using MortiseFrame.Compass;
using MortiseFrame.Compass.Extension;
using UnityEngine;

namespace Phantom {

    public static class GameMapDomain {

        public static MapEntity Spawn(GameBusinessContext ctx, int typeID) {
            var map = GameFactory.Map_Spawn(ctx.templateInfraContext, ctx.assetsInfraContext, typeID);
            ctx.currentMapEntity = map;
            return map;
        }

        public static void DrawMap(GameBusinessContext ctx) {
            DrawAllGrids(ctx);
            DrawAllObstacles(ctx);
            DrawAllPath(ctx);
        }

        static void DrawAllPath(GameBusinessContext ctx) {
            var enemyLen = ctx.roleRepo.TakeAll(out var enemyArr);
            for (int i = 0; i < enemyLen; i++) {
                var role = enemyArr[i];
                if (role.allyStatus != AllyStatus.Enemy) {
                    continue;
                }
                var path = role.path;
                var pathLen = role.pathLen;
                if (pathLen > 0) {
                    DrawPath(ctx, path);
                }
            }
        }

        static void DrawPath(GameBusinessContext ctx, Vector2[] path) {
            var config = ctx.templateInfraContext.Config_Get();
            var mat = config.pathMat;
            var color = config.pathColor;
            var thickness = config.pathThickness;
            var map = ctx.currentMapEntity;
            for (int i = 0; i < path.Length - 1; i++) {
                var startGrid = path[i];
                var endGrid = path[i + 1];
                var startPos = PathFindingGridUtil.GridToWorld_Center(startGrid, -map.mapSize / 2, map.gridUnit);
                var endPos = PathFindingGridUtil.GridToWorld_Center(endGrid, -map.mapSize / 2, map.gridUnit);
                GLApp.DrawLine(ctx.glContext, mat, startPos, endPos, color, thickness);
            }
        }

        static public bool IsWalkable(GameBusinessContext ctx, int gridX, int gridY) {
            var map = ctx.currentMapEntity;
            var walkable = PathFindingMapUtil.IsMapWalkable(map.obstacleData, map.obstacleDataWidth, gridX, gridY);
            return walkable;
        }

        static void DrawAllObstacles(GameBusinessContext ctx) {
            var map = ctx.currentMapEntity;
            var obstacles = map.obstacleData;
            var size = map.mapSize;
            var min = new Vector3(-size.x / 2, -size.y / 2, 0);
            var unit = map.gridUnit;
            for (int x = 0; x < size.x; x++) {
                for (int y = 0; y < size.y; y++) {
                    if (!obstacles[x + y * size.x]) {
                        DrawObstacle(ctx, new Vector3(min.x + x * unit, min.y + y * unit, 0));
                    }
                }
            }
        }

        static void DrawObstacle(GameBusinessContext ctx, Vector3 pos) {
            var config = ctx.templateInfraContext.Config_Get();
            var mat = config.obstacleMat;
            var color = config.obstacleColor;
            var map = ctx.currentMapEntity;
            var unit = map.gridUnit;
            var center = pos + new Vector3(unit / 2, unit / 2, 0);
            var size = new Vector2(unit, unit);
            GLApp.DrawRect(ctx.glContext, mat, center, size, color);
        }

        static void DrawAllGrids(GameBusinessContext ctx) {
            var map = ctx.currentMapEntity;
            var size = map.mapSize;
            var min = new Vector3(-size.x / 2, -size.y / 2, 0);
            var max = new Vector3(size.x / 2, size.y / 2, 0);
            var unit = map.gridUnit;
            for (float x = min.x; x <= max.x; x += unit) {
                DrawGrid(ctx, new Vector3(x, min.y, 0), new Vector3(x, max.y, 0));
            }
            for (float y = min.y; y <= max.y; y += unit) {
                DrawGrid(ctx, new Vector3(min.x, y, 0), new Vector3(max.x, y, 0));
            }
        }

        static void DrawGrid(GameBusinessContext ctx, Vector3 start, Vector2 end) {
            var config = ctx.templateInfraContext.Config_Get();
            var mat = config.gridMat;
            var color = config.gridColor;
            var thickness = config.gridThickness;
            GLApp.DrawLine(ctx.glContext, mat, start, end, color, thickness);
        }

        public static void UnSpawn(GameBusinessContext ctx) {
            var map = ctx.currentMapEntity;
            map.TearDown();
            ctx.currentMapEntity = null;
        }

    }

}