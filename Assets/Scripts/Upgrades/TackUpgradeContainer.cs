using System.Collections;
using System.Collections.Generic;
using Helpers;
using Units;

namespace Upgrades
{
    public class TackUpgradeContainer : AbstractUpgradeContainer {
    
        private readonly Dictionary<string, IUpgrade> _treeOneDict = new Dictionary<string, IUpgrade>();
        private readonly Dictionary<string, IUpgrade> _treeTwoDict = new Dictionary<string, IUpgrade>();
        private IDictionaryEnumerator _treeOneEnum;
        private IDictionaryEnumerator _treeTwoEnum;
        private IUpgrade _lastUpgrade;

        void Awake() {
            TackUpgrade upgrade11 = new TackUpgrade
                ("Faster Shooting", 0, 0, 0, 0.75f, 150, 0, 0);
            TackUpgrade upgrade12 = new TackUpgrade
                ("Even Faster Shooting", 0, 0, 0, 0.60f, 250, 0, 0);
            TackUpgrade upgrade13 = new TackUpgrade
                ("Mega Turbo Speed", 0, 0, 0, 0.75f, 300, 0, 0);
            TackUpgrade upgrade21 = new TackUpgrade
                ("More Tacks", 0, 0, 0, 1, 125, 0, 2);
            TackUpgrade upgrade22 = new TackUpgrade
                ("Even More Tacks", 0, 0, 0, 1, 250, 0, 2);
            TackUpgrade upgrade23 = new TackUpgrade
                ("Tack Sprayer", 0, 0, 0, 1f, 485, 0, 2);
            TackUpgrade upgrade24 = new TackUpgrade
                ("Mega Doom Flame", 1, 0, 1f, 1f, 2500, 1, 1, 0,  DamageType.FIRE);
            Initialise(1, upgrade11);
            Initialise(1, upgrade12);
            Initialise(1, upgrade13);
            Initialise(2, upgrade21);
            Initialise(2, upgrade22);
            Initialise(2, upgrade23);
            Initialise(2, upgrade24);
            treeOneEnum = treeOneDict.GetEnumerator();
            treeTwoEnum = treeTwoDict.GetEnumerator();
            GetNextKey(1);
            GetNextKey(2);
        }
    

        public override bool TryApplyUpgrade(string upgradeName, AbstractUnit b, int tree, int money, out IUpgrade thisUpgrade) {
            IUpgrade current = new TackUpgrade();
            if(tree == 1) {
                if(treeOneDict.TryGetValue(upgradeName, out IUpgrade upgrade)) {
                    if(money < upgrade.getBuyValue()) {
                        thisUpgrade = null;
                        return false;
                    }
                    current = upgrade;
                    b.MakeUpgrade(upgrade);
                }
            } else {
                if(treeTwoDict.TryGetValue(upgradeName, out IUpgrade upgrade)) {
                    if(money < upgrade.getBuyValue()) {
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
