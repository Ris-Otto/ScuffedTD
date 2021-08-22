using Helpers;
using Units.Guns;
using UnityEngine;
using Upgrades;

namespace Units
{
    public class GooberUnit : AbstractUnit
    {
    
        #region fields
        private GameObject _target;
        [SerializeField]
        private GameObject projectile;
        private AbstractUpgradeContainer _abstractUpgradeContainer;
        private bool _placed;
        private bool _isSelected;
        private int _price;
        private UIManager _uiManager;
        private GooberUpgrade _currentUpgrade;
        private const float BASE_ATTACK_SPEED = 1f;
        private int _targetingStyle;
        private Animation _anim;
        #endregion
        
        protected override void Awake() {
            base.Awake();
            uiManager = GameObject.Find("GooberCanvas").GetComponent<UIManager>();
            target = null;
            isSelected = false;
            abstractUpgradeContainer = GetComponent<AbstractUpgradeContainer>();
            InitialiseUnitParameters();
            _anim = GetComponentInChildren<Animation>();
            GenerateGun<GooberGun>(projectile);
        }

        public override void ComputeRotationFromChild() {
            Anim.Play();
            ComputeRotation();
        }
        
        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(transform.position, _currentUpgrade.range);
        }

        public override void InitialiseUnitParameters() {
            _currentUpgrade = new GooberUpgrade("default", 1, 2, 3f, 1, 250, 30f, 1);
            price = _currentUpgrade.price;
        }

        public override void MakeUpgrade(IUpgrade upgrade) {
            currentUpgrade.CumulateUpgrades(upgrade);
            price += upgrade.price;
        }

        #region getters/setters
        protected override Camera cam => Camera.main;
        
        protected override GameObject target {
            get => _target;
            set => _target = value;
        }
        public override bool isSelected {
            get => _isSelected;
            protected set => _isSelected = value;
        }
        public override AbstractUpgradeContainer abstractUpgradeContainer {
            get => _abstractUpgradeContainer;
            set => _abstractUpgradeContainer = value;
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
            set => _currentUpgrade = (GooberUpgrade)value;
        }

        public override float baseAttackSpeed => BASE_ATTACK_SPEED;

        public override Animation Anim => _anim;

        public override int targetingStyle {
            get => _targetingStyle;
            set => _targetingStyle = value;
        }

        protected override CreateRange Range => GetComponentInChildren<CreateRange>();

        #endregion

    }
}
