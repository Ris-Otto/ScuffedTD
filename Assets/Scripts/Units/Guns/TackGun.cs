using Managers;
using Projectiles;
using UnityEngine;
using Upgrades;

namespace Units.Guns
{
    public class TackGun : Gun
    {
        
        private TackUnit _parentUnit;
        private const float BASE_ATTACK_SPEED = 1;
        

        protected override void HandleProjectileSpawn<T>(GameObject target) {
            if (!(_upgrade is TackUpgrade up)) return;
            //Specific to multi-shot units
            //int _ID = Random.Range(0, 100000);
            
            for (int i = 0; i < up.Shot_count; i++) {
                GameObject p = _pooler.SpawnFromPool(Name, transform.position, Quaternion.identity);
                Vector3 position = ConfigureProjectile<T>(p, target, out Vector2 direction, out Projectile projectile);
                if(i != 0) direction = RotateVector(direction, i);
                //Specific to multi-shot units, bit slower but oh well
                //projectile.GetComponent<TackProjectile>().ID = _ID;
                ShootProjectile(projectile, direction, position, target);
            }
        }

        protected override Vector3 ConfigureProjectileTransform(GameObject target, out Vector2 direction) {
            Vector3 position = _parent.transform.position;
            direction = Vector2.up;
            return position;
        }
        
        private Vector2 RotateVector(Vector2 vector, int amount) {
            float x = Mathf.Cos(_parentUnit._rotationAmount*amount) * vector.x - Mathf.Sin(_parentUnit._rotationAmount*amount) * vector.y;
            float y = Mathf.Sin(_parentUnit._rotationAmount*amount) * vector.x + Mathf.Cos(_parentUnit._rotationAmount*amount) * vector.y;
            return new Vector2(x, y);
        }

        #region getset

        protected override float AttackSpeed => BASE_ATTACK_SPEED * _upgrade.secondsPerAttackModifier;

        protected override AbstractUnit ParentUnit {
            get => _parentUnit;
            set => _parentUnit = (TackUnit)value;
        }

        protected override GameObject Projectile {
            get => _projectile;
            set => _projectile = value;
        }
        
        protected override string Name => "TackProjectile";

        #endregion
    }
}
