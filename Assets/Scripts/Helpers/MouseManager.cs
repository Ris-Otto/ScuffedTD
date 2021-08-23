using UnityEngine;
using UnityEngine.EventSystems;

namespace Helpers
{
    public class MouseManager : MonoBehaviour
    {

        public GameObject hitObject;
        private Camera _camera;

        private void Start() {
            _camera = Camera.main;
        }

        private void Update() {
            Ray ray = _camera.ScreenPointToRay(GetMousePos());
            if (EventSystem.current.IsPointerOverGameObject()) return;
            if (Physics.Raycast(ray, out RaycastHit hitInfo, LayerMask.GetMask("Selectable"))) {
                hitObject = hitInfo.transform.gameObject;
                if(Input.GetKeyDown(KeyCode.Mouse0))
                    SelectedObject(hitObject.GetComponent<ISelectable>());
            }
            else {
                hitObject = null;
            }
        }

        private static Vector3 GetMousePos() {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            return mousePos;
        }

        private static void SelectedObject(ISelectable thing) {
            thing.Select();
        }
    }
}
