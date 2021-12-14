using Units;
using UnityEngine;

namespace Helpers
{
    public class CreateRange : MonoBehaviour
    {

        #region fields
        private float radius;
        private SpriteRenderer _spriteRenderer;
        private AbstractUnit parent;
        private Transform myTransform;
        private Transform parentTransform;
        private Vector3 InitialScale;
        private float initialRadius;
        #endregion
        
        private void Awake() {
            ConfigureTransform();
            parent = GetComponentInParent<AbstractUnit>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }
        
        private void FixedUpdate() {
            if (ConfigureRadius(parent.IsHangar).Equals(radius)) return;
            radius = ConfigureRadius(parent.IsHangar);
            ChangeRadius();
        }
        
        private void ConfigureTransform() {
            Transform transform1 = transform;
            myTransform = transform1;
            parentTransform = transform1.parent;
            ConfigureLocalScale();
        }

        private void ConfigureLocalScale() {
            initialRadius = RadiusOfChild(parentTransform.localScale);
            InitialScale = new Vector2(initialRadius, initialRadius);
            myTransform.localScale = InitialScale;
        }
        
        public void DisplayRange(bool isActive) {
            _spriteRenderer.enabled = isActive;
        }

        public void ChangeDisplayColor(Color color) {
            _spriteRenderer.color = color;
        }

        private static float RadiusOfChild(Vector3 parentScale) {
            if (parentScale.x >= 0.5f)
                return parentScale.x / 3;
            return 1 + parentScale.x / 3;
        }

        private void ChangeRadius() {
            Vector3 localScale = InitialScale;
            localScale *= radius;
            myTransform.localScale = localScale;
        }

        private float ConfigureRadius(bool isHangar) {
            //Hangars don't have range(or rather, they have infinite/map-wide range), so it is simply displayed as a small circle barely encompassing it
            return isHangar ? 0.5f : parent.currentUpgrade.range;
        }
        
    }
}
