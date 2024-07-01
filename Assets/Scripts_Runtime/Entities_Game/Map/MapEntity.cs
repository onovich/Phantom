using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Phantom {

    public class MapEntity : MonoBehaviour {

        public int typeID;
        public Vector2Int mapSize;
        [SerializeField] SpriteRenderer bgSpr;
        public float gridUnit;

        public float timer;

        public void Ctor() {
            timer = 0;
        }

        public void SetSize(Vector2 size) {
            mapSize = size.RoundToVector2Int();
        }

        public void IncTimer(float dt) {
            timer += dt;
        }

        public void TearDown() {
            Destroy(gameObject);
        }

    }

}