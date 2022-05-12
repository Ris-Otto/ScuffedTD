using System.Diagnostics.CodeAnalysis;
using Managers;
using Projectiles;
using Units.Guns;
using UnityEngine;
using Upgrades;

namespace Units
{
    [SuppressMessage("ReSharper", "ConvertToAutoProperty")]
    public class RocketGuyUnit : AbstractUnit
    {
        
        #region fields
        [SerializeField]
        private GameObject projectile;
        private RocketUpgrade _currentUpgrade;
        protected AbstractUpgradeContainer _abstractUpgradeContainer;
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
        

        protected override void InitialiseUnitParameters() {
            _currentUpgrade = new RocketUpgrade
                ("default", 1, 1, 3, 1f, 480, 20f, 1f, 6, DamageType.EXPLOSIVE);
            price = _currentUpgrade.price;
        }

        
        
        #region getters/setters
        protected override Camera cam => Camera.main;

        protected override GameObject target {
            get => _target;
            set => _target = value;
        }
        
        public override AbstractUpgradeContainer abstractUpgradeContainer {
            get => _abstractUpgradeContainer;
            protected set => _abstractUpgradeContainer = value;
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

        public override float baseAttackSpeed => 1.25f;

        

        #endregion
    }
}
