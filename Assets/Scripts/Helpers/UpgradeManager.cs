using System;
using TMPro;
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
            return TryApplyUpgradeFromContainer(money, button, unit);
        }

        private AbstractUpgradeContainer TryApplyUpgradeFromContainer(int money, Button button, AbstractUnit unit) {
            AbstractUpgradeContainer container = unit.abstractUpgradeContainer;
            return CanApplyUpgrade(money, button, unit, container) ? null : container;
        }

        private bool CanApplyUpgrade(int money, Button button, AbstractUnit unit, AbstractUpgradeContainer container) {
            if (!container.TryApplyUpgrade(_uiManager.GetText(button), unit, tree, money, out IUpgrade upgrade)) return true;
            Text buttonTextComponent = _uiManager.GetTextComponent(button);
            container.lastUpgrade = upgrade;
            ApplyTextToButton(container, buttonTextComponent);
            return false;
        }

        private void ApplyTextToButton(AbstractUpgradeContainer container, Text buttonTextComponent) {
            IUpgrade nextUpgrade = container.GetNextUpgrade(tree);
            string bandaid = container.GetKey(tree);
            buttonTextComponent.text = nextUpgrade == null ? bandaid : nextUpgrade.upgradeName + " " + nextUpgrade.price;
        }

        private void FixedUpdate() {
            GetSelectedUnit();
        }

        private AbstractUnit GetSelectedUnit() {
            AbstractUnit[] objects = _activeObjects.units;
            foreach(AbstractUnit u in objects) {
                if(!u.isSelected) continue;
                _uiManager.DisplayStats(u, _uiManager.displayStats);
                return u;
            }
            return null;
        }
    
    
    
    }
}
