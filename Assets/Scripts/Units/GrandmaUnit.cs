using Helpers;
using Projectiles;
using UnityEngine;
using Upgrades;

namespace Units
{
    public class GrandmaUnit : AbstractHangar
    {
        
        #region fields
        [SerializeField]
        private GameObject projectileUnit;
        private EnemyListener _listener;
        private GrandmaUpgrade _currentUpgrade;
        private AbstractUpgradeContainer _abstractUpgradeContainer;
        #endregion
        
        protected override void Awake() {
            base.Awake();
            _listener = gameObject.AddComponent<EnemyListener>();
            MakeChild();
        }
        
        private void MakeChild() {
            projectileUnit = Instantiate(projectileUnit, gameObject.transform);
            projectileUnit.GetComponent<GrandmaProjectile>().SendParams(_currentUpgrade, _listener);
            projectileUnit.GetComponent<GrandmaProjectile>().Master = this;
        }

        public override void MakeUpgrade(IUpgrade upgrade) {
            currentUpgrade.CumulateUpgrades(upgrade, currentUpgrade);
            price += upgrade.price;
            CanAccessCamo = currentUpgrade.hasAccessToCamo;
            projectileUnit.GetComponent<GrandmaProjectile>().SendParams(_currentUpgrade, _listener);
        }

        public override void ComputeRotationFromChild() {
            
        }

        protected override void InitialiseUnitParameters() {
            _currentUpgrade = new GrandmaUpgrade("default", 1, 3, 20f, 1, 1250, 20f, 8);
            price = _currentUpgrade.price;
        }

        #region getters/setters

        protected override Camera cam => Camera.main;

        protected override GameObject target {
            get; 
            set;
        }

        
        public override AbstractUpgradeContainer abstractUpgradeContainer {
            get => _abstractUpgradeContainer;
            protected set => _abstractUpgradeContainer = value;
        }

        public override Animation Anim {
            get;
        }

        public override bool placed {
            get => _placed;
            protected set => _placed = value;
        }
        protected override int price {
            get => _price;
            set => _price = value;
        }

        protected override int sell => (int)(sellPercentage*price);

        protected override UIManager uiManager { 
            get => _uiManager;
            set => _uiManager = value;
        }

        public override IUpgrade currentUpgrade {
            get => _currentUpgrade;
            set => _currentUpgrade = (GrandmaUpgrade)value;
        }

        public override float baseAttackSpeed => 1f;

        #endregion
    }
}
