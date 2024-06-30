using MortiseFrame.Silk;
using UnityEngine;

namespace Phantom {

    public static class GLApp {

        public static void Tick(GLAppContext ctx) {
            ctx.core.Tick();
        }

        public static void RecordCameraInfo(GLAppContext ctx, Camera camera) {
            ctx.core.RecordCameraInfo(camera);
        }

        public static void DrawLine(GLAppContext ctx, Material mat, Vector3 start, Vector3 end, Color color, float thickness) {
            ctx.core.DrawLine(mat, start, end, color, thickness);
        }

        public static void DrawRect(GLAppContext ctx, Material mat, Vector2 center, Vector2 size, Color color) {
            ctx.core.DrawRect(mat, center, size, color);
        }

        public static void DrawWiredRect(GLAppContext ctx, Material mat, Vector2 center, Vector2 size, Color color, float thickness) {
            ctx.core.DrawWiredRect(mat, center, size, color, thickness);
        }

        public static void DrawCircle(GLAppContext ctx, Material mat, Vector2 center, float radius, Color color) {
            ctx.core.DrawCircle(mat, center, radius, color);
        }

        public static void DrawWiredCircle(GLAppContext ctx, Material mat, Vector2 center, float radius, Color color, float thickness) {
            ctx.core.DrawWiredCircle(mat, center, radius, color, thickness);
        }

        public static void DrawRing(GLAppContext ctx, Material mat, Vector2 center, float radius, float thickness, Color color) {
            ctx.core.DrawRing(mat, center, radius, thickness, color);
        }

        public static void DrawWiredRing(GLAppContext ctx, Material mat, Vector2 center, float radius, float thickness, Color color) {
            ctx.core.DrawWiredRing(mat, center, radius, thickness, color);
        }

        public static void DrawTriangle(GLAppContext ctx, Material mat, Vector2 a, Vector2 b, Vector2 c, Color color) {
            ctx.core.DrawTriangle(mat, a, b, c, color);
        }

        public static void DrawWiredTriangle(GLAppContext ctx, Material mat, Vector2 a, Vector2 b, Vector2 c, Color color, float thickness) {
            ctx.core.DrawWiredTriangle(mat, a, b, c, color, thickness);
        }

        public static void DrawStar(GLAppContext ctx, Material mat, Vector2 center, int points, float innerRadius, float outerRadius, Color color) {
            ctx.core.DrawStar(mat, center, points, innerRadius, outerRadius, color);
        }

        public static void DrawWiredStar(GLAppContext ctx, Material mat, Vector2 center, int points, float innerRadius, float outerRadius, Color color, float thickness) {
            ctx.core.DrawWiredStar(mat, center, points, innerRadius, outerRadius, color, thickness);
        }

        public static void TearDown(GLAppContext ctx) {
            ctx.core.TearDown();
        }

    }

}