using Enemies;
using Helpers;
using UnityEngine;

namespace Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        public void SeekTarget(GameObject obj, Vector2 shootDirection, Vector2 pos) {
            if(obj == null) return;
            spawnedAt = pos;
            target = obj;
            dir = shootDirection;
        }

        protected abstract void ComputeMovement(); 
        
        protected virtual void CheckIfOutsideRange() {
            if (Vector3.Distance(transform.position, spawnedAt) > (range + 2)) 
                ResetThis();
        }

        protected void CalculateProjectilePropertiesOnNonNormalReturnValue(int dieValue) {

        }

        public virtual void ComputeMovementFromOther() {
            
        }
        
        public virtual void ResetProjectileFromEnemy() {
            ResetThis();
        }
        
        protected bool IsTargetActive() {
            return target != null;
        }

        protected virtual void Hit(Collider2D col) {
            if (pierce <= 0 && hasCollided) {
                ResetThis();
            }
            else {
                Listener.Income(col.gameObject.GetComponent<AbstractEnemy>().DieOverload(this, damage));
                hasCollided = true;
                pierce--;
            }
        }

        protected virtual void ResetThis() {
            hasCollided = false;
            gameObject.SetActive(false);
        }

        public abstract void SendParams(IUpgrade upgrade, EnemyListener listener);

        #region getset
        protected abstract float projectileSpeed {
            get;
            set;
        }

        protected abstract GameObject target {
            get;
            set;
        }

        protected abstract Vector2 dir {
            get;
            set;
        }

        public abstract int damage {
            get;
            set;
        }

        public abstract int pierce {
            get;
            set;
        }

        protected abstract float range {
            get;
            set;
        }

        protected abstract Vector2 spawnedAt {
            get;
            set;
        }

        private EnemyListener Listener => GetComponent<EnemyListener>();

        protected abstract bool hasCollided {
            get;
            set;
        }

        public abstract long ID {
            get;
        }

        public abstract ScriptableDamageType DamageType {
            get;
        }
        #endregion

    }
}
