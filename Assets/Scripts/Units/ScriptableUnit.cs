using Managers;
using UnityEngine;
using Upgrades;

namespace Units
{
    [CreateAssetMenu(fileName = "New Unit", menuName = "ScriptableObject/Unit")]
    public class ScriptableUnit : ScriptableObject
    {
        public AbstractUpgradeContainer UpgradeContainer;
        public UIManager UIManager;
        public int price;
    }
}
