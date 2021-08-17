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
            float tempradius = radius;
            radius = ConfigureRadius(parent.IsHangar);
            if (tempradius.Equals(radius)) return;
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
            _spriteRenderer.color = Color.white;
            _spriteRenderer.enabled = isActive;
        }

        public void ChangeDisplayColor(Color color) {
            _spriteRenderer.color = color;
            Debug.Log(_spriteRenderer.color);
        }

        private static float RadiusOfChild(Vector3 parentScale) {
            //Don't think this is very fun for the processor but it's such a rarely called method WH OMEGALUL cares
            //But basically this is needed, because I'm using a sprite to render range instead of a LineRenderer; can't be bothered with learning that shit
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
