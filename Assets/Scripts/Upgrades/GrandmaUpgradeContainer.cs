using System;
using System.Collections;
using System.Collections.Generic;
using Units;

namespace Upgrades
{
    public class GrandmaUpgradeContainer : AbstractUpgradeContainer
    {
        
        private readonly Dictionary<string, IUpgrade> _treeOneDict = new Dictionary<string, IUpgrade>();
        private readonly Dictionary<string, IUpgrade> _treeTwoDict = new Dictionary<string, IUpgrade>();
        private IDictionaryEnumerator _treeOneEnum;
        private IDictionaryEnumerator _treeTwoEnum;
        private IUpgrade _lastUpgrade;

        private void Awake() {
            //TODO fix these things
            GrandmaUpgrade upgrade11 = new GrandmaUpgrade
                ("Albin stash", 0, 300, 1, 1);
            GrandmaUpgrade upgrade12 = new GrandmaUpgrade
                ("Not Implemented1", 0, 1, 0, 1, 185, 0f, 0);
            GrandmaUpgrade upgrade13 = new GrandmaUpgrade
                ("Not Implemented2", 0, 0, 0, 1, 350, 0, 2);
            GrandmaUpgrade upgrade21 = new GrandmaUpgrade
                ("Not Implemented3", 0, 0, 0, (float)(1/1.15), 85, 0f, 0);
            GrandmaUpgrade upgrade22 = new GrandmaUpgrade
                ("Not Implemented4", 0, 0, 0, (float)(1/1.33), 140, 0f, 0);
            GrandmaUpgrade upgrade23 = new GrandmaUpgrade
                ("Not Implemented5", 0, 0, 1, 1, 120, 0f, 0);
            Initialise(1, upgrade11);
            Initialise(1, upgrade12);
            Initialise(1, upgrade13);
            Initialise(2, upgrade21);
            Initialise(2, upgrade22);
            Initialise(2, upgrade23);
            treeOneEnum = treeOneDict.GetEnumerator();
            treeTwoEnum = treeTwoDict.GetEnumerator();
            GetNextUpgrade(1);
            GetNextUpgrade(2);
        }

        public override bool TryApplyUpgrade(string upgradeName, AbstractUnit b, int tree, int money, out IUpgrade thisUpgrade) {
            IUpgrade current = new GrandmaUpgrade();
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
