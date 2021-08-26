using System.Collections;
using System.Collections.Generic;
using Helpers;
using Units;

namespace Upgrades
{
    public class GooberUpgradeContainer : AbstractUpgradeContainer
    {
        private readonly Dictionary<string, IUpgrade> _treeOneDict = new Dictionary<string, IUpgrade>();
        private readonly Dictionary<string, IUpgrade> _treeTwoDict = new Dictionary<string, IUpgrade>();
        private IDictionaryEnumerator _treeOneEnum;
        private IDictionaryEnumerator _treeTwoEnum;
        private IUpgrade _lastUpgrade;

        private void Awake() {
            GooberUpgrade upgrade11 = new GooberUpgrade
                ("Sharpened Albins", 0, 1, 0, 1, 120, 0f, 0);
            GooberUpgrade upgrade12 = new GooberUpgrade
                ("Perfectly Honed Albins", 0, 1, 0, 1, 185, 0f, 0);
            GooberUpgrade upgrade13 = new GooberUpgrade
                ("Thrice The Fun", 0, 0, 0, 1, 350, 0, 2);
            GooberUpgrade upgrade14 = new GooberUpgrade
                ("Overclocked", 1, 1, 0, 1, 1000, 0, 0);
            GooberUpgrade upgrade21 = new GooberUpgrade
                ("Sleight of Hand", 0, 0, 0, (float)(1/1.15), 85, 0f, 0);
            GooberUpgrade upgrade22 = new GooberUpgrade
                ("Kossu Buff", 0, 0, 0, (float)(1/1.33), 140, 0f, 0);
            GooberUpgrade upgrade23 = new GooberUpgrade
                ("20/20", 0, 0, 0, 1, 120, 0f, 0, true);
            GooberUpgrade upgrade24 = new GooberUpgrade
                ("Piribrillor", 0, 0, 1, 1, 150, 0f, 0);
            Initialise(1, upgrade11);
            Initialise(1, upgrade12);
            Initialise(1, upgrade13);
            Initialise(1, upgrade14);
            Initialise(2, upgrade21);
            Initialise(2, upgrade22);
            Initialise(2, upgrade24);
            Initialise(2, upgrade23);
            treeOneEnum = treeOneDict.GetEnumerator();
            treeTwoEnum = treeTwoDict.GetEnumerator();
            GetNextUpgrade(1);
            GetNextUpgrade(2);
        }

        public override bool TryApplyUpgrade(string upgradeName, AbstractUnit b, int tree, int money, out IUpgrade thisUpgrade) {
            IUpgrade current = new GooberUpgrade();
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
