using Helpers;
using Projectiles;
using UnityEngine;

namespace Units.Guns
{
    public abstract class Gun : MonoBehaviour 
    {
        protected void Awake() {
            Time = 0.0f;
        }

        protected void Update() {
            ComputeShooting<Projectile>(ParentUnit.TargetEnemy(ParentUnit.targetingStyle));
        }

        public void GenerateGun(GameObject p) {
            Listener = gameObject.AddComponent<EnemyListener>();
            ConfigureGun(p);
            ConfigurePooler();
        }

        private void ConfigurePooler() {
            Pooler = gameObject.AddComponent<ProjectilePooler>();
            Pooler.CreatePool(Projectile, 30);
        }

        protected virtual void ConfigureGun(GameObject p) {
            ConfigureTransform(p);
            ParentUnit = Parent.GetComponent<AbstractUnit>();
            Upgrade = ParentUnit.currentUpgrade;
        }

        protected void ConfigureTransform(GameObject p) {
            //Apparently it's inefficient to call a GameObjects Transform component repeatedly. Sorta don't care cuz I'm cool but oh well.
            Transform transform1 = transform;
            transform1.parent = p.transform;
            transform1.position = p.transform.position;
            Parent = transform1.parent.gameObject;
        }

        protected virtual void ComputeShooting<T>(GameObject target) where T : Projectile {
            if (!ParentUnit.placed) return;
            Time += UnityEngine.Time.deltaTime;
            if (!(Time > AttackSpeed)) return;
            Target = target;
            Shoot<T>();
        }

        public void SetProjectile(GameObject p) {
            Projectile = p;
        }

        protected virtual void Shoot<T>() where T : Projectile {
            if (Target == null) return;
            ParentUnit.ComputeRotationFromChild();
            HandleProjectileSpawn<T>();
            Time = 0.0f;
        }

        protected virtual void HandleProjectileSpawn<T>() where T : Projectile {
            GameObject p = Pooler.SpawnFromPool(Name, transform.position, Quaternion.identity);
            Vector3 position = ConfigureProjectile<T>(p, out Vector2 direction, out Projectile projectile);
            ShootProjectile(projectile, direction, position);
        }

        protected virtual Vector3 ConfigureProjectileTransform(out Vector2 direction) {
            Vector3 position = Parent.transform.position;
            direction = Target.transform.position - position;
            return position;
        }
        
        protected virtual Vector3 ConfigureProjectile<T>(GameObject p, out Vector2 direction, out Projectile projectile) where T : Projectile {
            Vector3 position = ConfigureProjectileTransform(out direction);
            projectile = p.GetComponent<T>();
            return position;
        }

        protected void ShootProjectile(Projectile pr, Vector2 direction, Vector3 position) {
            pr.SendParams(Upgrade, Listener);
            pr.SeekTarget(Target, direction, position);
        }
        
        protected abstract float Time { get; set; }
        
        protected abstract float AttackSpeed { get; }

        protected abstract GameObject Target { get; set; }
        
        protected abstract AbstractUnit ParentUnit { get; set; }
        
        protected abstract GameObject Parent { get; set; }
        
        protected abstract IUpgrade Upgrade { get; set; }
        
        protected abstract bool UsesSecondary { get; }
        
        protected abstract EnemyListener Listener { get; set; }
        
        protected abstract ProjectilePooler Pooler { get; set; }
        
        protected abstract GameObject Projectile { get; set; }

        protected abstract string Name { get; }
    }
    
}
