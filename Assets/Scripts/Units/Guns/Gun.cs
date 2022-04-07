using Managers;
using Projectiles;
using UnityEngine;
using Upgrades;

namespace Units.Guns
{
    public abstract class Gun : MonoBehaviour 
    {
        
        //TODO optimise, optimise
        
        protected GameObject _projectile;
        protected ProjectilePooler _pooler;
        protected float _time;
        protected IUpgrade _upgrade;
        protected GameObject _parent;
        protected EnemyListener _listener;
        
        protected void Awake() {
            _time = 0.0f;
        }

        protected void FixedUpdate() {
            ComputeShooting<Projectile>(ParentUnit.TargetEnemy(ParentUnit.targetingStyle));
        }

        public void GenerateGun(GameObject p) {
            _listener = gameObject.AddComponent<EnemyListener>();
            ConfigureGun(p);
            ConfigurePooler();
        }

        private void ConfigurePooler() {
            _pooler = gameObject.AddComponent<ProjectilePooler>();
            _pooler.CreatePool(Projectile, 30);
        }

        protected virtual void ConfigureGun(GameObject p) {
            ConfigureTransform(p);
            ParentUnit = _parent.GetComponent<AbstractUnit>();
            _upgrade = ParentUnit.currentUpgrade;
        }

        protected void ConfigureTransform(GameObject p) {
            Transform transform1 = transform;
            transform1.parent = p.transform;
            transform1.position = p.transform.position;
            _parent = transform1.parent.gameObject;
        }

        protected virtual void ComputeShooting<T>(GameObject target) where T : Projectile {
            if (!ParentUnit.placed) return;
            _time += Time.deltaTime;
            if (!(_time > AttackSpeed)) return;
            Shoot<T>(target);
        }

        public void SetProjectile(GameObject p) {
            Projectile = p;
        }

        protected virtual void Shoot<T>(GameObject target) where T : Projectile {
            if (target == null) return;
            ParentUnit.ComputeRotationFromChild();
            HandleProjectileSpawn<T>(target);
            _time = 0.0f;
        }

        protected virtual void HandleProjectileSpawn<T>(GameObject target) where T : Projectile {
            GameObject p = _pooler.SpawnFromPool(Name, transform.position, Quaternion.identity);
            Vector3 position = ConfigureProjectile<T>(p, target, out Vector2 direction, out Projectile projectile);
            ShootProjectile(projectile, direction, position, target);
        }

        protected virtual Vector3 ConfigureProjectileTransform(GameObject target, out Vector2 direction) {
            Vector3 position = _parent.transform.position;
            direction = target.transform.position - position;
            return position;
        }
        
        protected virtual Vector3 ConfigureProjectile<T>(GameObject p, GameObject target, out Vector2 direction,
            out Projectile projectile) where T : Projectile {
            Vector3 position = ConfigureProjectileTransform(target, out direction);
            projectile = p.GetComponent<T>();
            projectile.Master = ParentUnit;
            return position;
        }

        protected void ShootProjectile(Projectile pr, Vector2 direction, Vector3 position, GameObject target) {
            pr.SendParams(_upgrade, _listener);
            pr.SeekTarget(target, direction, position);
        }
        
        
        protected abstract float AttackSpeed { get; }
        
        protected abstract AbstractUnit ParentUnit { get; set; }

        protected abstract GameObject Projectile { get; set; }

        protected abstract string Name { get; }
    }
    
}
