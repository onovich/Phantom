using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Phantom {

    public class MapEntity : MonoBehaviour {

        public int typeID;
        Vector2Int mapSize;
        [SerializeField] SpriteRenderer bgSpr;

        public float timer;

        public void Ctor() {
            timer = 0;
        }

        public void SetSize(Vector2 size) {
            mapSize = size.RoundToVector2Int();
            bgSpr.size = new Vector2(mapSize.x, mapSize.y);
            bgSpr.transform.position = new Vector3(-mapSize.x / 2, -mapSize.y / 2, 0);
        }

        public void IncTimer(float dt) {
            timer += dt;
        }

        public void TearDown() {
            Destroy(gameObject);
        }

    }

}