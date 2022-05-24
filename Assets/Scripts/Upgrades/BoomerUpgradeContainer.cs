using System.Collections;
using System.Collections.Generic;
using Units;

namespace Upgrades
{
    public class BoomerUpgradeContainer : AbstractUpgradeContainer
    {
        
        
        private readonly Dictionary<string, IUpgrade> _treeOneDict = new Dictionary<string, IUpgrade>();
        private readonly Dictionary<string, IUpgrade> _treeTwoDict = new Dictionary<string, IUpgrade>();
        private IDictionaryEnumerator _treeOneEnum;
        private IDictionaryEnumerator _treeTwoEnum;
        private IUpgrade _lastUpgrade;
        
        public override bool TryApplyUpgrade(string upgradeName, AbstractUnit b, int tree, int money, out IUpgrade thisUpgrade) {
            IUpgrade current = _lastUpgrade;
            if(tree == 1) {
                if(treeOneDict.TryGetValue(upgradeName, out IUpgrade upgrade)) {
                    if(money < upgrade.GetBuyValue()) {
                        thisUpgrade = null;
                        return false;
                    }
                    current = upgrade;
                    b.MakeUpgrade(upgrade);
                }
            } else {
                if(treeTwoDict.TryGetValue(upgradeName, out IUpgrade upgrade)) {
                    if(money < upgrade.GetBuyValue()) {
                        thisUpgrade = null;
                        return false;
                    }
                    current = upgrade;
                    b.MakeUpgrade(upgrade);
                }
            }
            thisUpgrade = current;
            return true;
        }

        #region getters/setters

        protected override Dictionary<string, IUpgrade> treeOneDict => _treeOneDict;
        protected override Dictionary<string, IUpgrade> treeTwoDict => _treeTwoDict;
        protected override IDictionaryEnumerator treeOneEnum {
            get => _treeOneEnum;
            set => _treeOneEnum = value;
        }
        protected override IDictionaryEnumerator treeTwoEnum {
            get => _treeTwoEnum;
            set => _treeTwoEnum = value;
        }
        public override IUpgrade lastUpgrade {
            get => _lastUpgrade;
            set => _lastUpgrade = value;
        }
    
        #endregion
    }
}