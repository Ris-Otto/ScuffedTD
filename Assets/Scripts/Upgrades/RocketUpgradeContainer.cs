using System.Collections;
using System.Collections.Generic;
using Projectiles;
using Units;

namespace Upgrades
{
    public class RocketUpgradeContainer : AbstractUpgradeContainer {
    
        private Dictionary<string, IUpgrade> _treeOneDict = new Dictionary<string, IUpgrade>();
        private Dictionary<string, IUpgrade> _treeTwoDict = new Dictionary<string, IUpgrade>();
        private IDictionaryEnumerator _treeOneEnum;
        private IDictionaryEnumerator _treeTwoEnum;
        private IUpgrade _lastUpgrade;

        void Awake() {
            RocketUpgrade upgrade11 = new RocketUpgrade
                ("Bigger Bombs", 0, 0, 0, 1, 300, 0f, 2, 6, DamageType.EXPLOSIVE);
            RocketUpgrade upgrade12 = new RocketUpgrade
                ("Heavy Bombs", 1, 0, 0, 1, 555, 0f, 0, 10);
            RocketUpgrade upgrade21 = new RocketUpgrade
                ("Faster Reload", 0, 0, 0, 0.75f, 210, 0f, 0, 0);
            RocketUpgrade upgrade22 = new RocketUpgrade
                ("Missile Launcher", 0, 0, 1, 0.80f, 555, 10f, 0, 0);
            Initialise(1, upgrade11);
            Initialise(1, upgrade12);
            Initialise(2, upgrade21);
            Initialise(2, upgrade22);
            treeOneEnum = treeOneDict.GetEnumerator();
            treeTwoEnum = treeTwoDict.GetEnumerator();
            GetNextUpgrade(1);
            GetNextUpgrade(2);
        }

        public override bool TryApplyUpgrade(string upgradeName, AbstractUnit b, int tree, int money, out IUpgrade thisUpgrade) {
            IUpgrade current = new GooberUpgrade(); //TODO change
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
