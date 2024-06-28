using System;
using System.Threading.Tasks;
using Phantom.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Phantom {
    public static class VFXParticelApp {

        public static void Init(VFXParticelAppContext ctx) {

        }

        public static async Task LoadAssets(VFXParticelAppContext ctx) {
            try {
                await ctx.vfxParticelCore.LoadAssets();
            } catch (Exception e) {
                GLog.LogError(e.ToString());
            }
        }

         public static void ReleaseAssets(VFXParticelAppContext ctx) {
            ctx.vfxParticelCore.ReleaseAssets();
        }

        public static void LateTick(VFXParticelAppContext ctx, float dt) {
            ctx.vfxParticelCore.Tick(dt);
        }

        public static void AddVFXToWorld(VFXParticelAppContext ctx, string vfxName, float duration, Vector2 pos) {
            ctx.vfxParticelCore.TrySpawnAndPlayVFX_ToWorldPos(vfxName, duration, pos);
        }

        public static void AddVFXToTarget(VFXParticelAppContext ctx, string vfxName, float duration, Transform target) {
            ctx.vfxParticelCore.TrySpawnAndPlayVFX_ToTarget(vfxName, duration, target, Vector3.zero);
        }

        public static void PlayVFXManualy(VFXParticelAppContext ctx, int preSpawnVFXID) {
            ctx.vfxParticelCore.TryPlayManualy(preSpawnVFXID);
        }

        public static void StopVFXManualy(VFXParticelAppContext ctx, int preSpawnVFXID) {
            ctx.vfxParticelCore.TryStopManualy(preSpawnVFXID);
        }

        public static void TearDown(VFXParticelAppContext ctx) {
            ctx.vfxParticelCore.TearDown();
        }

    }

}