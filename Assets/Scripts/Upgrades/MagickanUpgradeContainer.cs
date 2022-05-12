using System;
using System.Collections;
using System.Collections.Generic;
using Units;
using UnityEngine;

namespace Upgrades
{
    public class MagickanUpgradeContainer : AbstractUpgradeContainer
    {
        private readonly Dictionary<string, IUpgrade> _treeOneDict = new Dictionary<string, IUpgrade>();
        private readonly Dictionary<string, IUpgrade> _treeTwoDict = new Dictionary<string, IUpgrade>();
        private IDictionaryEnumerator _treeOneEnum;
        private IDictionaryEnumerator _treeTwoEnum;
        private IUpgrade _lastUpgrade;

        private void Awake() {
            MagickanUpgrade upgrade11 = new MagickanUpgrade
                ("Intense Magic", 0, 1, 0, 0.8f, 255, 10);
            MagickanUpgrade upgrade12 = new MagickanUpgrade
                ("Arcane Blast", 1, 0, 0, 1, 720, 0);
            MagickanUpgrade upgrade13 = new MagickanUpgrade
                ("Arcane Mastery", 1, 2, 0, 1f, 1560, 0);
            MagickanUpgrade upgrade21 = new MagickanUpgrade
                ("Fireball", "Fireball", 480);
            MagickanUpgrade upgrade22 = new MagickanUpgrade
                ("Wall of Flame", "WoF", 970);
            MagickanUpgrade upgrade23 = new MagickanUpgrade
                ("Cosmic Insight", 0, 0, 1, 0.5f, 1000, 0);
            
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
            IUpgrade current = new MagickanUpgrade();
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
