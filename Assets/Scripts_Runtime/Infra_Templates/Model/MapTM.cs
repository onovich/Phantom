using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Phantom {

    [CreateAssetMenu(fileName = "TM_Map", menuName = "Phantom/MapTM")]
    public class MapTM : ScriptableObject {

        public int typeID;
        public Vector2Int mapSize;

        // Roles
        public Vector2 ownerSpawnPoint;

        // Camera
        public Vector2 cameraConfinerWorldMax;
        public Vector2 cameraConfinerWorldMin;

    }

}