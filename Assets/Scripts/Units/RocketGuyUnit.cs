using System.Diagnostics.CodeAnalysis;
using Helpers;
using Units.Guns;
using UnityEngine;
using Upgrades;

namespace Units
{
    [SuppressMessage("ReSharper", "ConvertToAutoProperty")]
    public class RocketGuyUnit : AbstractUnit
    {
        
        #region fields
        private GameObject _target;
        [SerializeField]
        private GameObject projectile;
        private AbstractUpgradeContainer _abstractUpgradeContainer;
        private bool _placed;
        private bool _isSelected;
        private int _price = 200;
        private UIManager _uiManager;
        private RocketUpgrade _currentUpgrade;
        private const float BASE_ATTACK_SPEED = 1.25f;
        private int _targetingStyle;
        private Animation _anim;
        #endregion
        
        protected override void Awake() {
            base.Awake();
            uiManager = GameObject.Find("GooberCanvas").GetComponent<UIManager>();
            target = null;
            abstractUpgradeContainer = GetComponent<AbstractUpgradeContainer>();
            isSelected = false;
            InitialiseUnitParameters();
            _anim = GetComponentInChildren<Animation>();
            GenerateGun<RocketGun>(projectile);
        }

        public override void ComputeRotationFromChild() {
            Anim.Play();
            ComputeRotation();
        }
        
        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(transform.position, _currentUpgrade.range);
        }

        protected override void InitialiseUnitParameters() {
            _currentUpgrade = new RocketUpgrade
                ("default", 1, 1, 3, 1f, 650, 20f, 1f, 6, DamageType.EXPLOSIVE);
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
            set => _currentUpgrade = (RocketUpgrade)value;
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
