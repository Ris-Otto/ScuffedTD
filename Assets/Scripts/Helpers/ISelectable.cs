using UnityEngine.EventSystems;

namespace Helpers
{
    public interface ISelectable : IEventSystemHandler {

        public bool isPlaceable {
            get;
        }
        void Select();
        void DeselectOthers();
        void SetSelected(bool selected);
        bool IsSelected();

        void Deselect();
    }
}
