using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
    public class PopBehaviour : MonoBehaviour
    {
        public Sprite[] Sprites;
        private SpriteRenderer _renderer;
        private Sprite _sprite;
        private float _timeAlive;

        private void Awake() {
            _renderer = GetComponent<SpriteRenderer>();
            _sprite = Sprites[Random.Range(0, Sprites.Length)];
            _renderer.sprite = _sprite;
        }

        private void Update() {
            _timeAlive += Time.deltaTime;
            if (_timeAlive > 0.1f) {
                Destroy(gameObject);
            }
        }
    }
}
