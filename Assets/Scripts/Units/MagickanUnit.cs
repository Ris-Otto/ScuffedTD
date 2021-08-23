using System;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using Units.Guns;
using UnityEngine;
using Upgrades;
using Random = UnityEngine.Random;

namespace Units
{
    public class MagickanUnit : AbstractUnit
    {
        
        #region fields
        private GameObject _target;
        [SerializeField]
        private GameObject _projectile;
        [SerializeField]
        private GameObject _secondaryProjectile;
        private AbstractUpgradeContainer _abstractUpgradeContainer;
        private bool _placed;
        private bool _isSelected;
        private int _price;
        private UIManager _uiManager;
        private MagickanUpgrade _currentUpgrade;
        private const float BASE_ATTACK_SPEED = 1f;
        private int _targetingStyle;
        private Animation _anim;
        [SerializeField]
        private GameObject _tertiaryProjectile;
        private GameObject[] pathTiles;
        #endregion

        protected override void Awake() {
            base.Awake();
            _anim = GetComponentInChildren<Animation>();
            GenerateGun<MagickanGun>(_projectile);
        }

        private void InitialisePathTargets() {
            List<GameObject> objList = GameObject.FindGameObjectsWithTag("Placeable").ToList();
            IEnumerable<GameObject> removalList = 
                objList.Where(tile => Vector3.Distance(transform.position, tile.transform.position) <= _currentUpgrade.range);
            /*foreach (GameObject tile in objList.Where(tile => Vector3.Distance(transform.position, tile.transform.position) > _currentUpgrade.range)) {
                removalList.Remove(tile);
            }*/
            pathTiles = removalList.ToArray();
        }
        
        public GameObject TargetPathTile() {
            return pathTiles[Random.Range(0, pathTiles.Length)];
        }

        public override void ComputeRotationFromChild() {
            _anim.Play();
            ComputeRotation();
        }

        private void OnDrawGizmos() {
            Gizmos.DrawWireSphere(transform.position, _currentUpgrade.range);
        }

        public override void MakeUpgrade(IUpgrade upgrade) {
            MagickanUpgrade up = (MagickanUpgrade) upgrade;
            currentUpgrade.CumulateUpgrades(upgrade);
            price += upgrade.price;
            switch (up.newProjectile) {
                case "Fireball":
                    GenerateGun<FireballGun>(_secondaryProjectile);
                    break;
                case "WoF":
                    GenerateGun<WoFGun>(_tertiaryProjectile);
                    break;
            }
        }
        
        public override void BeforePlaceUnit() {
            if (placed) {
                transform.position = cam.ScreenToWorldPoint(GetMousePos(5f));
                //This was probably stealing at least some processing power so made it initially more expensive
                //but invocation is cancelled at time of placement
                InitialisePathTargets();
                CancelInvoke(nameof(BeforePlaceUnit));
                return;
            }
            transform.position = cam.ScreenToWorldPoint(GetMousePos());
            placed = TryPlaceUnit(cam.ScreenPointToRay(GetMousePos()));
        }

        protected override void InitialiseUnitParameters() {
            _currentUpgrade = new MagickanUpgrade("default", 1, 2, 3, 1, 450, 25f, DamageType.FIRE, 1);
            _price = _currentUpgrade.price;
        }

        #region getters/setters

        protected override Camera cam => Camera.main;
        
        protected override GameObject target {
            get => _target;
            set => _target = value;
        }
    
        public override Animation Anim => _anim;
        
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
            set => _currentUpgrade = (MagickanUpgrade)value;
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
