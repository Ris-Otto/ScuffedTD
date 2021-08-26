using Helpers;
using Projectiles;
using UnityEngine;

namespace Enemies
{
    public abstract class AbstractEnemy : MonoBehaviour
    {
        protected void Awake() {
            waypointIdx = 0;
            distanceTravelled = 0;
            et = ActiveObjectsTracker.Instance;
            SendThisHasSpawnedToActiveObjectsTracker(et);
        }

        #region SendMessages methods
        private void SendThisHasDiedToActiveObjectsTracker(ActiveObjectsTracker aot) {
            aot.RemoveEnemy(this);
        }

        private void SendThisHasSpawnedToActiveObjectsTracker(ActiveObjectsTracker aot) {
            aot.OnEnemySpawn(this);
        }
        
        #endregion
        
        #region Transform-manipulation methods
        
        private void FixedUpdate() {
            timeToSave += Time.deltaTime;
            ComputeMovement();
        }

        private void ComputeMovement() {
            Vector3 pos = transform.position;
            if(timeToSave > 0.2f*Enemy.speed) {
                SavePos(pos);
                timeToSave = 0f;
            }
            Vector3 targetPos = Pathfinding.Waypoints[waypointIdx].position;
            Vector2 dir = targetPos - pos;
            transform.Translate(dir.normalized * (Enemy.speed * Time.deltaTime), Space.World);
            if(HasReachedTarget(targetPos)) GetNextWaypoint();
            distanceTravelled += 0.01f*Enemy.speed;
            
        }

        private Vector2 GetDir() {
            return Pathfinding.Waypoints[waypointIdx].position - transform.position;
        }

        private void SavePos(Vector3 pos) {
            savedPos = pos;
        }

        private bool HasReachedTarget(Vector3 targetPos) {
            return Vector3.Distance(transform.position, targetPos) <= Enemy.speed*0.02f;
        }

        private void GetNextWaypoint() {
            if (waypointIdx < Pathfinding.Waypoints.Length - 1) 
                waypointIdx++;
            else 
                ResetThis();
        }
        
        #endregion
        
        #region onHit-methods
        
        protected int PassOnDamageToChild(Projectile projectile, int remainingDamage, AbstractEnemy e) {
            return e.KillChild(projectile, remainingDamage);
        }
        
        protected virtual int ComputeOnHitBehaviourOverload(Projectile projectile, int remainingDamage) {
            if (remainingDamage <= 0) return 0;
            if(remainingDamage >= Enemy.totalHealth) {
                ResetThis();
                return Enemy.totalHealth;
            }
            AbstractEnemy[] es = InstantiateMultipleChildrenOnConditionsMet(Enemy.directChildren, projectile);
            ResetThis();
            return PassOnDamageToChild(projectile, remainingDamage-1, es[0]) + 1;
        }

        public int DieOverload(Projectile projectile, int remainingDamage) {
            return ProjectileHasAppropriateParameters(projectile) ? ComputeOnHitBehaviourOverload(projectile, remainingDamage) : 0;
        }

        private int KillChild(Projectile projectile, int remainingDamage) {
            return IsAppropriateDamageType(projectile) ? ComputeOnHitBehaviourOverload(projectile, remainingDamage) : 0;
        }

        private bool CantBePoppedByProjectile(Projectile projectile) {
            bool toReturn = LastProjectile != null && LastProjectile.ID.Equals(projectile.ID);
            if (!toReturn) projectile.pierce++;
            return toReturn;
        }

        public virtual bool IsAppropriateDamageType(Projectile projectile) {
            bool toReturn = DamageType.CompareTo(projectile.DamageType) <= 0;
            if(!toReturn) projectile.ResetProjectileFromEnemy();
            return toReturn;
        }
        
        private bool ProjectileHasAppropriateParameters(Projectile projectile) {
            if (IsCamo) {
                projectile.pierce++;
                return false;
            }
            if (LastProjectile == null) return IsAppropriateDamageType(projectile);
            return !CantBePoppedByProjectile(projectile) && IsAppropriateDamageType(projectile);
        }

        protected void ResetThis() {
            Instantiate(Enemy.popObject, transform.position, Quaternion.identity);
            SendThisHasDiedToActiveObjectsTracker(et);
            GameObject o;
            (o = gameObject).SetActive(false);
            Destroy(o, 0.1f);
        }

        private AbstractEnemy InstantiateChildOnConditionsMet(GameObject childObject, Projectile projectile, bool hasOffset,
            int offsetMagnitude) {
            if (childObject.Equals(null)) return null;
            //SetOffset(GetDir(), childObject.transform.localScale.magnitude*0.5f*offsetMagnitude);
            AbstractEnemy e = InstantiateChildOverload(childObject, projectile, hasOffset ? savedPos*offsetMagnitude : transform.position);
            return e;
        }
        
        private AbstractEnemy InstantiateChildOverload(GameObject childObject, Projectile projectile, Vector3 spawnPos) {
            AbstractEnemy e = Instantiate
                (childObject, spawnPos, Quaternion.identity).GetComponent<AbstractEnemy>();
            e.LastProjectile = projectile;
            e.waypointIdx = waypointIdx;
            e.distanceTravelled = distanceTravelled;
            return e;
        }

        protected AbstractEnemy[] InstantiateMultipleChildrenOnConditionsMet(GameObject[] childObjects, Projectile projectile) {
            AbstractEnemy[] es = new AbstractEnemy[childObjects.Length];
            for (int i = 0; i < childObjects.Length; i++) 
                es[i] = InstantiateChildOnConditionsMet(childObjects[i], projectile, i != 0, i);
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

        protected abstract ScriptableDamageType DamageType {
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

        public virtual bool IsCamo => TryGetComponent(out Camo camo);

        #endregion
    }
}
