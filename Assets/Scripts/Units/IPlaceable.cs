using UnityEngine;

namespace Units
{
    public interface IPlaceable
    {
        public void BeforePlaceUnit();

        public bool TryPlaceUnit(Ray rayDown);

        public void OnCallDestroy();
    }
}
