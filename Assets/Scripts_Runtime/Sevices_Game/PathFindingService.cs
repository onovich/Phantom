using System;
using MortiseFrame.Compass;
using UnityEngine;

namespace Phantom {

    public class PathFindingService {

        public static int FindPath(Vector2 startGrid, Vector2 endGrid, Func<int, int, bool> walkable, int mapWidth, int mapHeight, PathFindingDirection directionMode, bool cornerWalkable, Vector2[] path) {
            return PathFindingCore.FindPath(startGrid, endGrid, walkable, mapWidth, mapHeight, directionMode, cornerWalkable, path);
        }

    }

}