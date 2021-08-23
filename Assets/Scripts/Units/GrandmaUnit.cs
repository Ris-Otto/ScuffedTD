using Helpers;
using Projectiles;
using UnityEngine;
using Upgrades;

namespace Units
{
    public class GrandmaUnit : AbstractUnit
    {
        
        #region fields
        [SerializeField]
        private GameObject projectileUnit;
        private AbstractUpgradeContainer _abstractUpgradeContainer;
        private bool _placed;
        private bool _isSelected;
        private EnemyListener _listener;
        private int _price;
        private UIManager _uiManager;
        private GrandmaUpgrade _currentUpgrade;
        private const float BASE_ATTACK_SPEED = 1f;
        private int _targetingStyle;
        #endregion
        

        protected override void Awake() {
            base.Awake();
            _listener = gameObject.AddComponent<EnemyListener>();
            MakeChild();
        }
        
        private void MakeChild() {
            projectileUnit = Instantiate(projectileUnit, gameObject.transform);
            projectileUnit.GetComponent<GrandmaProjectile>().SendParams(_currentUpgrade, _listener);
        }

        public override void MakeUpgrade(IUpgrade upgrade) {
            currentUpgrade.CumulateUpgrades(upgrade);
            price += upgrade.price;
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

        public override bool isSelected {
            get => _isSelected;
            protected set => _isSelected = value;
        }
        public override AbstractUpgradeContainer abstractUpgradeContainer {
            get => _abstractUpgradeContainer;
            set => _abstractUpgradeContainer = value;
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

        public override float baseAttackSpeed => BASE_ATTACK_SPEED;


        public override int targetingStyle {
            get => _targetingStyle;
            set => _targetingStyle = value;
        }

        public override bool IsHangar => true;

        protected override CreateRange Range => GetComponentInChildren<CreateRange>();

        #endregion
    }
}
