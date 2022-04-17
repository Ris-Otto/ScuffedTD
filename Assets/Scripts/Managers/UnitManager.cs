using System;
using System.Collections;
using Units;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class UnitManager : MonoBehaviour, IUndoableAction<AbstractUnit>
    {
        public GameObject unit;
        public Camera cam;
        private ActiveObjectsTracker _activeObjects;
        private Economics _eco;
        private bool _canUndo;
        private GameObject currentObject;
        private Log log;

        private void FixedUpdate() {
            if (!EventSystem.current.IsPointerOverGameObject()) return;
            if (_canUndo) {
                Undo();
            }
        }
        
        private void Start() {
            log = FindObjectOfType<Log>();
            _eco = Economics.Instance;
            _activeObjects = GameObject.FindGameObjectWithTag("Pooler").GetComponent<ActiveObjectsTracker>();
        }

        private AbstractUnit SpawnUnit(){
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 2f;
            Vector3 objectPos = cam.ScreenToWorldPoint(mousePos);
            GameObject obj = Instantiate(unit, objectPos, Quaternion.identity);
            AbstractUnit toReturn = obj.GetComponent<AbstractUnit>();
            if(_eco.Money < toReturn.GetBuyValue()) {
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
            foreach (AbstractUnit u in _activeObjects.Units) {
                if (!u.IsSelected()) continue;
                toReturn = u;
            }
            return toReturn;
        }

        private void SetCanUndo(bool canUndo) {
            _canUndo = canUndo;
        }

        public AbstractUnit Execute() {
            return SpawnUnit();
        }

        private IEnumerator WaitALittle(Func<bool> func) {
            yield return new WaitUntil(func);
            SetCanUndo(true);
        }
        

        private static bool IsPointerOverUIFunc() => !EventSystem.current.IsPointerOverGameObject();
        
        public void Undo() {
            AbstractUnit u = currentObject.GetComponent<AbstractUnit>();
            if (u.placed) {
                currentObject = null;
                SetCanUndo(false);
                return;
            }
            _eco.UpdateMoney(u.GetBuyValue());
            log.Logger.Log(LogType.Log, $"0: {u.name}: Purchase undone.");
            SetCanUndo(false);
            u.OnCallDestroy();
            currentObject = null;
        }
    }
}
