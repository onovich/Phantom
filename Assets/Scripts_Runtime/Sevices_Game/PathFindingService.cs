using System;
using MortiseFrame.Compass;
using MortiseFrame.Compass.Extension;
using UnityEngine;

namespace Phantom {

    public class PathFindingService {

        public Vector2[] path;
        public int pathLen;
        public int obstacleDataWidth;
        public bool[] obstacleData;
        public float gridUnit;
        public Vector2 gridGridCornerLD;
        public Vector2 gridGridConderRT;
        public bool showGizmos;

        public static int FindPath(Vector2 startGrid, Vector2 endGrid, Func<int, int, bool> walkable, int mapWidth, int mapHeight, PathFindingDirection directionMode, bool cornerWalkable, Vector2[] path) {
            return PathFindingCore.FindPath(startGrid, endGrid, walkable, mapWidth, mapHeight, directionMode, cornerWalkable, path);
        }

        void OnDrawPath() {
            PathFindingGizmosHelper.OnDrawPath(pathLen, path, gridGridCornerLD, gridUnit);
        }

        void OnDrawGrid() {
            PathFindingGizmosHelper.OnDrawGrid(gridUnit, gridGridCornerLD, gridGridConderRT);
        }

        void OnDrawObstacle() {
            PathFindingGizmosHelper.OnDrawObstacle(obstacleData, obstacleDataWidth, gridGridCornerLD, gridUnit);
        }

        void OnDrawGizmos() {
            if (!showGizmos) return;
            OnDrawGrid();
            OnDrawPath();
            OnDrawObstacle();
        }

    }

}