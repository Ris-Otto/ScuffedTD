using System;
using System.Collections;
using Units;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Helpers
{
    public class UnitManager : MonoBehaviour, IUndoableAction<AbstractUnit>
    {
        public GameObject unit;
        public Camera cam;
        private ActiveObjectsTracker _activeObjects;
        private Economics _eco;
        private bool _canUndo;
        private GameObject currentObject;

        private void FixedUpdate() {
            if (!EventSystem.current.IsPointerOverGameObject()) return;
            if (_canUndo) {
                Undo();
            }
        }
        
        private void Start() {
            _eco = Economics.Instance;
            _activeObjects = GameObject.FindGameObjectWithTag("Pooler").GetComponent<ActiveObjectsTracker>();
        }

        private AbstractUnit SpawnUnit(){
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 2f;
            Vector3 objectPos = cam.ScreenToWorldPoint(mousePos);
            GameObject obj = Instantiate(unit, objectPos, Quaternion.identity);
            AbstractUnit toReturn = obj.GetComponent<AbstractUnit>();
            if(_eco.Money < toReturn.getBuyValue()) {
                toReturn.OnCallDestroy();
                return null;
            }
            _activeObjects.OnUnitSpawn(toReturn);
            StartCoroutine(WaitALittle(IsPointerOverUIFunc));
            currentObject = toReturn.gameObject;
            return toReturn;
        }

        public AbstractUnit DestroyUnit() {
            AbstractUnit toReturn = null;
            foreach (AbstractUnit u in _activeObjects.units) {
                if (!u.IsSelected()) continue;
                toReturn = u;
            }
            return toReturn;
        }

        private void SetCanUndo(bool canUndo) {
            _canUndo = canUndo;
        }

        public AbstractUnit Execute() {
            //Borde i princip adda ti nån shitty list/stack haista paska juttu
            //men man kan oberoende int undo mer än ett inköp åt gången å sen kan de int undoas så w/e fuck you h8rz get off my case 
            /*
                          /´¯/) 
                        /¯../ 
                      /..../ 
                 /´¯/'...'/´¯¯`·¸ 
             /'/.../..../......./¨¯\ 
            ('(...´...´.... ¯~/'...') 
             \.................'...../ 
              ''...\.......... _.·´ 
                \..............( 
                 \.............\
             */
            
            //Dehä e btw helt vitun onödit ja behöver ba en Undo() metod men helt samma kolla ett par rader uppåt ba. bitch.
            return SpawnUnit();
        }

        private IEnumerator WaitALittle(Func<bool> func) {
            yield return new WaitUntil(func);
            SetCanUndo(true);
        }
        

        private static bool IsPointerOverUIFunc() => !EventSystem.current.IsPointerOverGameObject();
        
        public void Undo() {
            //idk maby dogshit sluta läsa koden
            AbstractUnit u = currentObject.GetComponent<AbstractUnit>();
            if (u.placed) {
                currentObject = null;
                SetCanUndo(false);
                return;
            }
            _eco.UpdateMoney(u.getBuyValue());
            SetCanUndo(false);
            u.OnCallDestroy();
            currentObject = null;
        }
    }
}
