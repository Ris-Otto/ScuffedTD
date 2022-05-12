using System;
using Enemies;
using Managers;
using Units;
using UnityEngine;
using Upgrades;

namespace Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {

        private ISource _master;
        //private GameObject _target;
        protected int kills;
        protected EnemyListener _listener;

        protected virtual void Awake() {
            kills = 0;
        }

        public void SeekTarget(GameObject obj, Vector2 shootDirection, Vector2 pos) {
            if(obj == null) return;
            spawnedAt = pos;
            //target = obj;
            dir = shootDirection;
        }

        protected abstract void ComputeMovement(); 
        
        protected virtual void CheckIfOutsideRange() {
            if (Vector3.Distance(transform.position, spawnedAt) > (range*2)) 
                ResetThis();
        }

        public virtual void ComputeMovementFromOther() {
            
        }
        
        public virtual void ResetProjectileFromEnemy() {
            ResetThis();
        }

        protected virtual void Hit(Collider2D col) {
            kills += _listener.Income(col.gameObject.GetComponent<AbstractEnemy>().Die(this, damage));
            hasCollided = true;
            if (pierce-- <= 0 && hasCollided) ResetThis();
        }

        protected virtual void ResetThis() {
            gameObject.SetActive(false);
            Master.AddToKills(kills);
            kills = 0;
            hasCollided = false;
        }

        public abstract void SendParams(IUpgrade upgrade, EnemyListener listener);

        #region getset
        protected abstract float projectileSpeed {
            get;
            set;
        }

        

        public ISource Master {
            get => _master;
            set => _master = value;
        }

        protected abstract Vector2 dir {
            get;
            set;
        }

        protected abstract int damage {
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

        protected virtual EnemyListener Listener {
            get;
            set;
        }

        protected abstract bool hasCollided {
            get;
            set;
        }

        public virtual long ID {
            get;
            set;
        }

        public abstract ScriptableDamageType DamageType {
            get;
        }
        #endregion

    }
}
