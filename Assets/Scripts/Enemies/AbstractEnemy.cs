using System;
using Managers;
using Projectiles;
using UnityEngine;

namespace Enemies
{
    public abstract class AbstractEnemy : MonoBehaviour
    {
        [SerializeField]
        protected bool _lock;

        private bool spawned;

        private Collider2D _collider2D;
        
        protected void Awake() {
            _collider2D = GetComponent<Collider2D>();
            _collider2D.enabled = false;
            spawned = false;
            //OnSpawned();
            et = ActiveObjectsTracker.Instance;
            SendThisHasSpawnedToActiveObjectsTracker(et);
        }

        private void OnEnable() {
            if (waypointIdx >= 1) {
                _collider2D.enabled = true;
            }
        }

        #region SendMessages methods
        private void SendThisHasDiedToActiveObjectsTracker(ActiveObjectsTracker aot) {
            aot.RemoveEnemy(this);
        }

        private void SendThisHasSpawnedToActiveObjectsTracker(ActiveObjectsTracker aot) {
            aot.OnEnemySpawn(this);
        }

        public void SetCamo(ActiveObjectsTracker aot, bool camo) {
            IsCamo = camo;
            SendThisHasDiedToActiveObjectsTracker(aot);
            SendThisHasSpawnedToActiveObjectsTracker(aot);
        }
        
        #endregion
        
        #region Transform-manipulation methods
        
        private void FixedUpdate() {
            timeToSave += Time.deltaTime;
            ComputeMovement();
        }

        private void ComputeMovement() {
            Vector3 pos = transform.position;
            if(timeToSave > 0.15f) {
                SavePos(pos);
                timeToSave = 0f;
            }
            OnSpawned();
            Vector3 targetPos = Pathfinding.Waypoints[waypointIdx].position;
            Vector2 dir = targetPos - pos;
            transform.Translate(dir.normalized * (Enemy.speed * Time.deltaTime), Space.World);
            if(HasReachedTarget(targetPos)) GetNextWaypoint();
            distanceTravelled += Time.deltaTime*Enemy.speed;
        }

        private void SavePos(Vector3 pos) {
            savedPos = pos;
        }

        private bool HasReachedTarget(Vector3 targetPos) {
            return Vector3.Distance(transform.position, targetPos) <= Enemy.speed*0.02f;
        }

        private void GetNextWaypoint() {
            if (waypointIdx < Pathfinding.Waypoints.Length - 1) {
                waypointIdx++;
            }
            else {
                HealthManager.Instance.OnEnemyPassedThrough(this);
                ResetThis();
            }
                
        }

        private void OnSpawned() {
            if (spawned || waypointIdx < 1) return;
            spawned = true;
            _collider2D.enabled = true;
        }

        #endregion
        
        #region onHit-methods
        
        protected int PassOnDamageToChild(Projectile projectile, int remainingDamage, AbstractEnemy e) {
            return e.KillChild(projectile, remainingDamage);
        }
        
        protected virtual int ComputeOnHitBehaviour(Projectile projectile, int remainingDamage) {
            _lock = true;
            if (remainingDamage <= 0) {
                _lock = false;
                return 0;
            }
            if(remainingDamage >= Enemy.totalHealth) {
                ResetThis();
                return Enemy.totalHealth;
            }
            AbstractEnemy[] es = InstantiateChildren(Enemy.directChildren, projectile);
            ResetThis();
            return PassOnDamageToChild(projectile, remainingDamage-1, es[0]) + 1;
        }

        public int Die(Projectile projectile, int remainingDamage) {
            return ProjectileHasAppropriateParameters(projectile) ? ComputeOnHitBehaviour(projectile, remainingDamage) : 0;
        }

        private int KillChild(Projectile projectile, int remainingDamage) {
            return IsAppropriateDamageType(projectile) ? ComputeOnHitBehaviour(projectile, remainingDamage) : 0;
        }

        
        private bool CantBePoppedByProjectile(Projectile projectile) {
            bool ret = _lock;
            if (ret) {
                projectile.pierce++;
                return true;
            }
            ret = LastProjectile != null;
            if (ret)
                ret = LastProjectile.ID.Equals(projectile.ID);

            if (projectile.pierce > 0) return ret;
            projectile.ResetProjectileFromEnemy();
            return true;
        }

        public virtual bool IsAppropriateDamageType(Projectile projectile) {
            if (IsCamo && !projectile.Master.CanAccessCamo) {
                projectile.pierce++;
                return false;
            }
            bool toReturn = damageType.CompareTo(projectile.DamageType) <= 0;
            if(!toReturn) projectile.ResetProjectileFromEnemy();
            return toReturn;
        }
        
        private bool ProjectileHasAppropriateParameters(Projectile projectile) {
            //if (LastProjectile == null) return IsAppropriateDamageType(projectile);
            return !CantBePoppedByProjectile(projectile) && IsAppropriateDamageType(projectile);
        }

        protected void ResetThis() {
            SendThisHasDiedToActiveObjectsTracker(et);
            GameObject o;
            (o = gameObject).SetActive(false);
            Instantiate(Enemy.popObject, transform.position, Quaternion.identity);
            
            Destroy(o, 0.1f);
        }

        private AbstractEnemy InstantiateChild(GameObject childObject, Projectile projectile, bool hasOffset) {
            if (childObject.Equals(null)) return null;
            Vector3 offset = transform.position;
            if (hasOffset && savedPos != Vector3.zero) offset = savedPos;
            AbstractEnemy e = InstantiateChild(childObject, projectile, offset);
            return e;
        }
        
        private AbstractEnemy InstantiateChild(GameObject childObject, Projectile projectile, Vector3 spawnPos) {
            AbstractEnemy e = Instantiate
                (childObject, spawnPos, Quaternion.identity).GetComponent<AbstractEnemy>();
            e._lock = false;
            e.LastProjectile = projectile;
            e.waypointIdx = waypointIdx;
            e.distanceTravelled = distanceTravelled;
            
            return e;
        }

        protected AbstractEnemy[] InstantiateChildren(GameObject[] childObjects, Projectile projectile) {
            AbstractEnemy[] es = new AbstractEnemy[childObjects.Length];
            for (int i = 0; i < childObjects.Length; i++) 
                es[i] = InstantiateChild(childObjects[i], projectile, i != 0);
            return es;
        }

        #endregion
        
        #region getset
        public abstract float distanceTravelled {
            get;
            protected set;
        }

        protected abstract int waypointIdx {
            get;
            set;
        }

        protected abstract ActiveObjectsTracker et {
            get;
            set;
        }

        public abstract Enemy Enemy {
            get;
        }

        protected abstract Projectile LastProjectile {
            get;
            set;
        }

        protected abstract ScriptableDamageType damageType {
            get;
        }

        protected abstract float timeToSave {
            get;
            set;
        }

        protected abstract Vector3 savedPos {
            get;
            set;
        }

        public virtual bool IsCamo {
            get;
            set;
        }

        #endregion
    }
}
