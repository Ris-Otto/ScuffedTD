using System.Collections.Generic;
using System.Linq;
using Enemies;
using Extension;
using Helpers;
using Projectiles;
using Units.Guns;
using UnityEngine;
using Upgrades;

namespace Units
{
    public abstract class AbstractUnit : MonoBehaviour, IMoneyObject, ISelectable, IPlaceable, IMouseUser
    {
        protected bool _canAccessCamo;
        private bool _isSelected;
        protected bool _placed;
        protected GameObject _target;
        protected int _targetingStyle;
        protected Animation _anim;
        protected int _price;
        protected UIManager _uiManager;

        protected virtual void Awake() {
            InvokeRepeating(nameof(BeforePlaceUnit), 0f, Time.deltaTime);
            uiManager = GameObject.Find("GooberCanvas").GetComponent<UIManager>();
            target = null;
            isSelected = false;
            abstractUpgradeContainer = GetComponent<AbstractUpgradeContainer>();
            InitialiseUnitParameters();
        }
        
        protected void GenerateGun<T>(GameObject projectile) where T : Gun {
            GameObject gameObj = new GameObject {name = projectile.name + "Gun"};
            Gun thisGun = gameObj.AddComponent<T>();
            thisGun.SetProjectile(projectile);
            thisGun.GenerateGun(gameObject);
        }

        public abstract void ComputeRotationFromChild();

        protected abstract void InitialiseUnitParameters();
        
        public virtual void MakeUpgrade(IUpgrade upgrade) {
            currentUpgrade.CumulateUpgrades(upgrade);
            price += upgrade.price;
            CanAccessCamo = upgrade.hasAccessToCamo;
        }

        #region Targeting

        public GameObject TargetEnemy(int style = 0) {
            target = null;
            AbstractEnemy[] eb = CanAccessCamo ? et.AllEnemies : et.NonCamo;

            switch (style) {
                default:
                    TargetFirstEnemy(eb);
                    break;
                case 0:
                    TargetFirstEnemy(eb);
                    break;
                case 1:
                    TargetLastEnemy(eb);
                    break;
                case 2:
                    TargetStrongestEnemy(eb);
                    break;
                case 3:
                    TargetClosestEnemy(eb);
                    break;
            }
            return target;
        }

        private void TargetFirstEnemy(IEnumerable<AbstractEnemy> eb) {
            eb.Aggregate(Mathf.NegativeInfinity, (current, t) => 
                GetTargetUsingCorrespondingTargetingStyle(t, t.distanceTravelled, current, 1));
        }

        private void TargetLastEnemy(IEnumerable<AbstractEnemy> eb) {
            eb.Aggregate(Mathf.Infinity, (current, t) => 
                GetTargetUsingCorrespondingTargetingStyle(t, t.distanceTravelled, current, -1));
        }
        private void TargetStrongestEnemy(IEnumerable<AbstractEnemy> eb) {
            eb.Aggregate(Mathf.NegativeInfinity, (current, t) => 
                GetTargetUsingCorrespondingTargetingStyle(t, t.Enemy.totalHealth, current, 1));
        }
        private void TargetClosestEnemy(IEnumerable<AbstractEnemy> eb) {
            eb.Aggregate(Mathf.Infinity, (current, t) => 
                GetTargetUsingCorrespondingTargetingStyle(t, Vector3.Distance(t.transform.position, transform.position), 
                    current, -1));
        }

        private float GetTargetUsingCorrespondingTargetingStyle(AbstractEnemy t, float style, 
            float toCompare, int expected) {
            if (t.Equals(null)) return style;
            if (style.CompareTo(toCompare) != expected || 
                !(Vector3.Distance(t.transform.position, transform.position) < currentUpgrade.range)) return toCompare;
            target = t.gameObject;
            return style;
        }
    
        protected void ComputeRotation() {
            target = TargetEnemy(targetingStyle);
            if (target == null) return;
            if(target.activeSelf & placed)
                transform.rotation = Quaternion.LookRotation
                    ( new Vector3(0f,0f,1f),target.transform.position - transform.position);
        }
        
        #endregion
    
        #region everything
        public virtual void BeforePlaceUnit() {
            if (placed) {
                transform.position = cam.ScreenToWorldPoint(GetMousePos(5f));
                CancelInvoke(nameof(BeforePlaceUnit));
                return;
            }
            transform.position = cam.ScreenToWorldPoint(GetMousePos());
            placed = TryPlaceUnit(cam.ScreenPointToRay(GetMousePos()));
        }

        public Vector3 GetMousePos() {
            Vector3 pos = Input.mousePosition;
            pos.z = 2f;
            return pos;
        }

        public Vector3 GetMousePos(float zValue) {
            Vector3 pos = Input.mousePosition;
            pos.z = zValue;
            return pos;
        }

        public bool TryPlaceUnit(Ray rayDown) {
            if (!Physics.Raycast(transform.position, Vector3.forward, out RaycastHit hitInfo, 5f, finalMask)) 
                return false;
            
            bool placeableObject = hitInfo.transform.gameObject.GetInterface<ISelectable>().isPlaceable;
            DeselectOthers();
            Range.ChangeDisplayColor(!placeableObject ? Color.red : Color.white);
            //This could probably be better
            return Input.GetKeyDown(KeyCode.Mouse0) && placeableObject;
        }
        
        public void OnCallDestroy() {
            if (transform.GetChild(transform.childCount-1).TryGetComponent(out ProjectilePooler pooler)) 
                foreach (GameObject p in pooler.Get().SelectMany(keyValuePair => keyValuePair.Value))
                    Destroy(p);
            et.RemoveUnit(this);
            Destroy(gameObject);
        }

        

        public void Select() {
            //line below should make this less expensive since || only evaluates the first one if true
            placed = placed || TryPlaceUnit(cam.ScreenPointToRay(GetMousePos()));
            if(!placed) {
                Range.DisplayRange(true);
                return;
            }
            SetSelected(!isSelected && placed);
            if(isSelected) {
                DeselectOthers();
                uiManager.ShowMenu(this);
                Range.DisplayRange(true);
            } else {
                uiManager.HideMenu();
                Range.DisplayRange(false);
            }
            
        }

        public void Deselect() {
            SetSelected(false);
            Range.DisplayRange(false);
            uiManager.HideMenu();
        }

        public void DeselectOthers() {
            AbstractUnit[] units = et.Units;
            foreach (AbstractUnit t in units) 
                if (t != this) t.Deselect();
        }
        
        public void SetSelected(bool selected) {
            isSelected = selected;
        }

        public bool IsSelected() {
            return isSelected;
        }

        public int GetBuyValue() {
            return price;
        }
        public int GetSellValue() {
            return sell;
        }
        #endregion
        
        
        #region getset
        
        protected abstract Camera cam {
            get;
        }

        protected abstract GameObject target {
            get;
            set;
        }

        public abstract Animation Anim {
            get;
        }

        public bool isSelected {
            get => _isSelected;
            protected set => _isSelected = value;
        }

        public abstract AbstractUpgradeContainer abstractUpgradeContainer {
            get;
            protected set;
        }

        public abstract bool placed {
            get;
            protected set;
        }
        
        public bool isPlaceable => false;
        
        protected abstract int price {
            get;
            set;
        }

        private static int finalMask => 1 << 8 | 1 << 9;

        protected abstract int sell {
            get;
        }
        protected static float sellPercentage => 0.5f;

        protected abstract UIManager uiManager {
            get;
            set;
        }
        
        public abstract IUpgrade currentUpgrade {
            get;
            set;
        }

        public abstract float baseAttackSpeed {
            get;
        }

        private static ActiveObjectsTracker et 
            => GameObject.FindGameObjectWithTag("Pooler").GetComponent<ActiveObjectsTracker>();
        
        
        public abstract int targetingStyle {
            get;
            set;
        }

        public virtual bool IsHangar => false;

        protected abstract CreateRange Range { get; }

        public virtual bool CanAccessCamo {
            get => _canAccessCamo;
            protected set => _canAccessCamo = value;
        }

        #endregion
        
    }
}
