using UnityEngine.EventSystems;

namespace Helpers
{
    public interface ISelectable : IEventSystemHandler {
        void Select();
        void DeselectOthers();
        void SetSelected(bool selected);
        bool IsSelected();

        void Deselect();
    }
}
