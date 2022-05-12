using System.Collections.Generic;
using System.Linq;
using Enemies;
using Extension;
using Managers;
using Projectiles;
using Units.Guns;
using UnityEngine;
using Upgrades;

namespace Units
{
    public abstract class AbstractUnit : MonoBehaviour, IMoneyObject, ISelectable, IPlaceable, IMouseUser, ISource, ILoggable
    {
        
        private bool _canAccessCamo;
        private bool _isSelected;
        protected bool _placed;
        protected GameObject _target;
        protected Animation _anim;
        protected int _price;
        protected UIManager _uiManager;
        private int _pops;
        protected Log _log;
        private int _targetingStyle1;
        protected Vector3 placement;

        protected virtual void Awake() {
            InvokeRepeating(nameof(BeforePlaceUnit), 0f, Time.deltaTime);
            uiManager = GameObject.Find("GooberCanvas").GetComponent<UIManager>();
            target = null;
            isSelected = false;
            abstractUpgradeContainer = GetComponent<AbstractUpgradeContainer>();
            _log = FindObjectOfType<Log>();
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
            currentUpgrade.CumulateUpgrades(upgrade, currentUpgrade);
            price += upgrade.price;
            CanAccessCamo = currentUpgrade.hasAccessToCamo;
        }

        public void AddToKills(int kill) {
            Debug.Log(kill);
            _pops += kill;
        }

        public virtual void Log() {
            _log.Logger.Log(LogType.Log, $"; ; ; ; ; ; {name}:{_pops}");
            
        }

        #region Targeting
        
        public GameObject TargetEnemy(int style = 0) {
            
            IEnumerable<AbstractEnemy> eb = (CanAccessCamo ? et.AllEnemies : et.NonCamo)
                .Where(enemy =>
                Vector3.Distance(enemy.transform.position, transform.position) < currentUpgrade.range);

            GameObject ret = style switch {
                0 => TargetFirstEnemy(eb),
                1 => TargetLastEnemy(eb),
                2 => TargetStrongestEnemy(eb),
                3 => TargetClosestEnemy(eb),
                _ => null
            };

            return target = ret;
        }
        
        private GameObject TargetLastEnemy(IEnumerable<AbstractEnemy> eb) {
            float current = Mathf.Infinity;
            GameObject ret = null;
            foreach (AbstractEnemy e in eb) {
                float dist = e.distanceTravelled;
                if (dist > current) continue;
                current = dist;
                ret = e.gameObject;
            }
            return ret;
        }
        
        private GameObject TargetStrongestEnemy(IEnumerable<AbstractEnemy> eb) {
            
            float current = Mathf.NegativeInfinity;
            List<AbstractEnemy> ebn = eb.ToList();

            foreach (AbstractEnemy e in eb) {
                float health = e.Enemy.totalHealth;
                if (health < current) {
                    ebn.Remove(e);
                    continue;
                }
                current = health;
            }
            return TargetFirstEnemy(ebn);
        }
        
        private GameObject TargetClosestEnemy(IEnumerable<AbstractEnemy> eb) {
            float current = Mathf.Infinity;
            GameObject ret = null;
            

            foreach (AbstractEnemy e in eb) {
                float dist = Vector3.Distance(e.transform.position, placement);
                if (dist > current) continue;
                current = dist;
                ret = e.gameObject;
            }
            return ret;
        }
        
        private GameObject TargetFirstEnemy(IEnumerable<AbstractEnemy> eb) {
            float current = Mathf.NegativeInfinity;
            GameObject ret = null;
            
            foreach (AbstractEnemy e in eb) {
                float dist = e.distanceTravelled;
                if (dist < current) continue;
                current = dist;
                ret = e.gameObject;
            }
            return ret;
        }

        protected void ComputeRotation() {
            if (target == null) return;
            if(target.activeSelf & placed)
                transform.rotation = Quaternion.LookRotation
                    ( new Vector3(0f,0f,1f),target.transform.position - transform.position);
        }
        
        #endregion
    
        #region everything
        public virtual void BeforePlaceUnit() {
            if (placed) {
                Transform transform1;
                (transform1 = transform).position = cam.ScreenToWorldPoint(GetMousePos(5f));
                placement = transform1.position;
                _log.Logger.Log(LogType.Log, $"; ; ; ; ; {placement}");
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
            foreach (AbstractUnit t in et.Units) if (!Equals(t, this)) t.Deselect();
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


        public int targetingStyle {
            get => _targetingStyle1;
            set => _targetingStyle1 = value;
        }

        public virtual bool IsHangar => false;

        private CreateRange Range => GetComponentInChildren<CreateRange>();

        public bool CanAccessCamo {
            get => _canAccessCamo;
            set => _canAccessCamo = value;
        }

        #endregion
    }
}
