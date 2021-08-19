using System;
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
            ComputeMovement();
        }

        private void ComputeMovement() {
            Vector3 targetPos = Pathfinding.Waypoints[waypointIdx].position;
            Vector2 dir = targetPos - transform.position;
            transform.Translate(dir.normalized * (Enemy.speed * Time.deltaTime), Space.World);
            if(HasReachedTarget(targetPos)) GetNextWaypoint();
            distanceTravelled += 0.01f*Enemy.speed;
        }

        private Vector2 GetDir() {
            return Pathfinding.Waypoints[waypointIdx].position - transform.position;
        }
        
        protected virtual void SetOffset(Vector2 dir, float size) {
            SpawnOffset = -dir.normalized * size;
        }

        private bool HasReachedTarget(Vector3 targetPos) {
            return Vector3.Distance(transform.position, targetPos) <= Enemy.speed*0.05f;
        }

        private void GetNextWaypoint() {
            if (waypointIdx < Pathfinding.Waypoints.Length - 1) 
                waypointIdx++;
            else 
                ResetThis();
        }
        
        #endregion
        
        #region onHit-methods

        private bool CantBePoppedByProjectile(Projectile projectile) {
            return LastProjectile != null && LastProjectile.ID.Equals(projectile.ID);
        }

        protected virtual bool IsAppropriateDamageType(ScriptableDamageType dmgType) {
            return DamageType.CompareTo(dmgType) <= 0;
        }
        
        public virtual int Die(Projectile projectile) {
            if (ProjectileHasAppropriateParameters(projectile)) return ComputeOnHitBehaviour(projectile);
            projectile.pierce++;
            return 0;
        }

        protected virtual int ComputeOnHitBehaviour(Projectile projectile) {
            int pop = projectile.damage;
            if(pop >= Enemy.totalHealth) {
                ResetThis();
                return Enemy.totalHealth;
            }
            InstantiateChildOnConditionsMet(Enemy.children[Enemy.children.Length-(pop)], projectile);
            ResetThis();
            return pop;
        }

        private bool ProjectileHasAppropriateParameters(Projectile projectile) {
            if (LastProjectile == null) return IsAppropriateDamageType(projectile.DamageType);
            return !CantBePoppedByProjectile(projectile) && IsAppropriateDamageType(projectile.DamageType);
        }

        protected void ResetThis() {
            Instantiate(Enemy.popObject, transform.position, Quaternion.identity);
            SendThisHasDiedToActiveObjectsTracker(et);
            GameObject o;
            (o = gameObject).SetActive(false);
            Destroy(o, 0.1f);
        }

        private void InstantiateChildOnConditionsMet(GameObject childObject, Projectile projectile, bool hasOffset,
            int offsetMagnitude) {
            if (childObject.Equals(null)) return;
            SetOffset(GetDir(), childObject.transform.localScale.magnitude*0.5f*offsetMagnitude);
            if (!hasOffset) SpawnOffset = Vector3.zero;
            GameObject obj = Instantiate(childObject, transform.position - SpawnOffset, Quaternion.identity); 
            AbstractEnemy e = obj.GetComponent<AbstractEnemy>();
            e.LastProjectile = projectile;
            e.waypointIdx = waypointIdx;
            e.distanceTravelled = distanceTravelled;
        }
        
        protected void InstantiateChildOnConditionsMet(GameObject childObject, Projectile projectile) {
            if (childObject.Equals(null)) return;
            GameObject obj = Instantiate(childObject, transform.position - SpawnOffset, Quaternion.identity);
            AbstractEnemy e = obj.GetComponent<AbstractEnemy>();
            e.LastProjectile = projectile;
            e.waypointIdx = waypointIdx;
            e.distanceTravelled = distanceTravelled;
        }
        
        protected void InstantiateMultipleChildrenOnConditionsMet(GameObject[] childObjects, Projectile projectile) {
            for (int i = 0; i < childObjects.Length; i++) 
                InstantiateChildOnConditionsMet(childObjects[i], projectile, i != 0, i);
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

        protected abstract bool dead {
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

        protected abstract Vector3 SpawnOffset {
            get;
            set;
        }
        
        #endregion
    }
}
