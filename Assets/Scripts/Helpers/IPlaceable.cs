using UnityEngine;

namespace Helpers
{
    public interface IPlaceable
    {
        public void BeforePlaceUnit();

        public Vector3 GetMousePos();

        public bool TryPlaceUnit(Ray rayDown);

        public void OnCallDestroy();
    }
}
