using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Phantom {

    [CreateAssetMenu(fileName = "TM_Map", menuName = "Phantom/MapTM")]
    public class MapTM : ScriptableObject {

        public int typeID;
        public Vector2Int mapSize;
        public float gridUnit;
        public bool[] obstacleData;
        public int obstacleDataWidth;

        // Roles
        public Vector2 ownerSpawnPoint;
        public Vector2[] enemyPosArray;
        public RoleTM[] enemyArray;

        // Camera
        public Vector2 cameraConfinerWorldMax;
        public Vector2 cameraConfinerWorldMin;

    }

}