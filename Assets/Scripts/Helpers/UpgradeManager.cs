using Units;
using UnityEngine;
using UnityEngine.UI;
using Upgrades;

namespace Helpers
{
    public class UpgradeManager : MonoBehaviour {

    
        public int tree;
        private ActiveObjectsTracker _activeObjects;
        private UIManager _uiManager;
    
        private void Start() {
            _uiManager = GameObject.FindGameObjectWithTag("Canvas").GetComponent<UIManager>();
            _activeObjects = ActiveObjectsTracker.Instance;
        }

        public AbstractUpgradeContainer OnUpgradeClicked(int money, Button button) {
            AbstractUnit unit = GetSelectedUnit();
            Text currentButtonText = _uiManager.GetText(button);
            if (!unit.abstractUpgradeContainer.TryApplyUpgrade(currentButtonText.text, unit, tree, money, out IUpgrade upgrade)) return null;
            unit.abstractUpgradeContainer.lastUpgrade = upgrade;
            currentButtonText.text = unit.abstractUpgradeContainer.GetNextKey(tree);
            return unit.abstractUpgradeContainer;
        }
        private void FixedUpdate() {
            GetSelectedUnit();
        }

        private AbstractUnit GetSelectedUnit() {
            var objects = _activeObjects.units;
            foreach(var u in objects) {
                if(!u.isSelected) continue;
                _uiManager.DisplayStats(u, _uiManager.displayStats);
                return u;
            }
            return null;
        }
    
    
    
    }
}
