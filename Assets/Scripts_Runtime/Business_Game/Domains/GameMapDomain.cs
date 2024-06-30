using UnityEngine;

namespace Phantom {

    public static class GameMapDomain {

        public static MapEntity Spawn(GameBusinessContext ctx, int typeID) {
            var map = GameFactory.Map_Spawn(ctx.templateInfraContext, ctx.assetsInfraContext, typeID);
            ctx.currentMapEntity = map;
            DrawAllGrids(ctx);
            return map;
        }

        static void DrawAllGrids(GameBusinessContext ctx) {
            var map = ctx.currentMapEntity;
            var size = map.mapSize;
            var min = new Vector3(-size.x / 2, -size.y / 2, 0);
            var max = new Vector3(size.x / 2, size.y / 2, 0);
            var unit = map.gridUnit;
            for (float x = min.x; x < max.x; x += unit) {
                DrawGrid(ctx, new Vector3(x, min.y, 0), new Vector3(x, max.y, 0));
            }
            for (float y = min.y; y < max.y; y += unit) {
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