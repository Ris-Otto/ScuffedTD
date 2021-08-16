using Helpers;
using Units.Guns;
using UnityEngine;
using Upgrades;

namespace Units
{
    public class TackUnit : AbstractUnit
    {
        
        #region fields
        private GameObject _target;
        [SerializeField]
        private GameObject _projectile;
        private bool _isSelected;
        private AbstractUpgradeContainer _abstractUpgradeContainer;
        private bool _placed;
        private int _price = 350;
        private UIManager _uiManager;
        private TackUpgrade _currentUpgrade;
        private const float BASE_ATTACK_SPEED = 1.25f;
        [HideInInspector]
        public float _rotationAmount;
        private int _targetingStyle;
        private Animation _anim;
        #endregion
        
        protected override void Awake() {
            base.Awake();
            _projectile = Resources.Load<GameObject>("Prefabs/Projectiles/TackProjectile");
            uiManager = GameObject.Find("GooberCanvas").GetComponent<UIManager>();
            target = null;
            abstractUpgradeContainer = GetComponent<AbstractUpgradeContainer>();
            InitialiseUnitParameters();
            _anim = GetComponentInChildren<Animation>();
            _rotationAmount = 2*(Mathf.PI / _currentUpgrade.Shot_count);
            GenerateGun<TackGun>(_projectile);
        }
    
        public override void MakeUpgrade(IUpgrade upgrade) {
            currentUpgrade.CumulateUpgrades(upgrade);
            price += upgrade.price;
            _rotationAmount = 2*(Mathf.PI / _currentUpgrade.Shot_count);
        }
        
        public override void ComputeRotationFromChild() {
            Anim.Play();
        }
        
        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(transform.position, _currentUpgrade.range);
        }

        public override void InitialiseUnitParameters() {
            _currentUpgrade = new TackUpgrade
                ("default", 1, 1, 1.5f, 1f, 350, 20f, 0, 8,  DamageType.SHARP);
            price = _currentUpgrade.price;
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
        
        public override Animation Anim => _anim;

        protected override int sell => (int)(sellPercentage*price);

        protected override UIManager uiManager { 
            get => _uiManager;
            set => _uiManager = value;
        }

        public override IUpgrade currentUpgrade {
            get => _currentUpgrade;
            set => _currentUpgrade = (TackUpgrade)value;
        }

        public override float baseAttackSpeed => BASE_ATTACK_SPEED;

        public override int targetingStyle {
            get => _targetingStyle;
            set => _targetingStyle = value;
        }
        
        protected override CreateRange Range => GetComponentInChildren<CreateRange>();

        #endregion
    }
}
