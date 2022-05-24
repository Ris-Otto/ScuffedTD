using Managers;
using Units.Guns;
using UnityEngine;
using Upgrades;

namespace Units
{
    public class GooberUnit : AbstractUnit
    {
    
        #region fields
        [SerializeField]
        private GameObject projectile;
        private GooberUpgrade _currentUpgrade;
        private AbstractUpgradeContainer _abstractUpgradeContainer;
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

        protected override void InitialiseUnitParameters() {
            _currentUpgrade = new GooberUpgrade("default", 1, 2, 3f, 1, 250, 30f, 1);
            price = _currentUpgrade.price;
        }

        #region getters/setters

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

        protected override int sell => (int)(sellPercentage*price);

        protected override UIManager uiManager { 
            get => _uiManager;
            set => _uiManager = value;
        }

        public override IUpgrade currentUpgrade {
            get => _currentUpgrade;
            set => _currentUpgrade = (GooberUpgrade)value;
        }

        public override float baseAttackSpeed => 1f;

        public override Animation Anim => _anim;

        #endregion

    }
}
