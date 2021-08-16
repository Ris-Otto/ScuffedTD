using System;
using Units;
using UnityEngine;

namespace Helpers
{
    public class GridThing : MonoBehaviour, ISelectable
    {
        private bool _isSelected;
        private ActiveObjectsTracker _et;
        public bool isPlaceable;

        private void Start() {
            _et = ActiveObjectsTracker.Instance;
        }

        public void Select() {
            SetSelected(!_isSelected);
            if (_isSelected) {
                DeselectOthers();
            }
        }

        public void DeselectOthers() {
            var units = _et.units;
            foreach (var t in units) {
                t.Deselect();
            }
            Deselect();
        }

        public void SetSelected(bool selected) {
            _isSelected = selected;
        }

        public bool IsSelected() {
            return _isSelected;
        }

        public void Deselect() {
            SetSelected(false);
        }
    }
}
