using System.Collections;
using System.Collections.Generic;
using Helpers;
using Units;
using UnityEngine;

namespace Upgrades
{
    
    public abstract class AbstractUpgradeContainer : MonoBehaviour, IMoneyObject {

        protected void Initialise(int tree, IUpgrade upgrade) {
            AddUpgrade(upgrade.upgradeName, upgrade, tree);
        }

        private void AddUpgrade(string upgradeName, IUpgrade upgrade, int tree) { //adds upgrade to corresponding upgradeTree
            if(tree == 1)
                treeOneDict.Add(upgradeName, upgrade);
            else
                treeTwoDict.Add(upgradeName, upgrade);
        }

        public abstract bool TryApplyUpgrade(string upgradeName, AbstractUnit b, int tree, int money, out IUpgrade thisUpgrade);
        
        public string GetKey(int tree) { //used for getting upgradeName from button
            string toReturn;
            try {
                toReturn = tree == 1 ? treeOneEnum.Key?.ToString() : treeTwoEnum.Key?.ToString();
            } catch (System.InvalidOperationException){
                toReturn = "Max Upgrades";
            }
            return toReturn;
        }

        public string GetNextKey(int tree) { //used for displaying upgradeName on button
            if (tree == 1) {
                return treeOneEnum.MoveNext() ? treeOneEnum.Key?.ToString() : "Max Upgrades";
            }
            return treeTwoEnum.MoveNext() ? treeTwoEnum.Key?.ToString() : "Max Upgrades";
        }
        
        public int getBuyValue() {
            try {
                if(treeOneDict.TryGetValue(lastUpgrade.ToString(), out IUpgrade up)) 
                    return up.getBuyValue();
                treeTwoDict.TryGetValue(lastUpgrade.ToString(), out IUpgrade up1);
                return up1.getBuyValue();
            } catch (System.NullReferenceException ) {
                return 0;
            }
        }
    
        public int getSellValue() {
            return 0;
        }
        
        protected abstract Dictionary<string, IUpgrade> treeOneDict {
            get;
        }

        protected abstract Dictionary<string, IUpgrade> treeTwoDict {
            get;
        }

        protected abstract IDictionaryEnumerator treeOneEnum {
            get;
            set;
        }

        protected abstract IDictionaryEnumerator treeTwoEnum {
            get;
            set;
        }
        public abstract IUpgrade lastUpgrade {
            get;
            set;
        }


    }
}
