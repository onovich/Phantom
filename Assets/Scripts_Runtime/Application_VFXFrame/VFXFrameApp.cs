using System;
using System.Threading.Tasks;
using Phantom.UI;
using UnityEngine;
using UnityEngine.AddressableAssets;
using MortiseFrame.Swing;

namespace Phantom {
    public static class VFXFrameApp {

        public static void Init(VFXFrameAppContext ctx) {

        }

        public static void LateTick(VFXFrameAppContext ctx, float dt) {
            ctx.vfxFrameCore.Tick(dt);
        }

        public static int AddVFXToWorld(VFXFrameAppContext ctx,
                                         string vfxName,
                                         Sprite[] frames,
                                         bool isLoop,
                                         float frameInterval,
                                         Vector2 pos,
                                         string sortingLayer) {
            return ctx.vfxFrameCore.TrySpawnAndPlayVFX_ToWorldPos(vfxName,
                                                             frames,
                                                             isLoop,
                                                             frameInterval,
                                                             pos,
                                                             sortingLayerName: sortingLayer);
        }

        public static int AddVFXToTarget(VFXFrameAppContext ctx,
                                          string vfxName,
                                          Sprite[] frames,
                                          bool isLoop,
                                          float frameInterval,
                                          Transform target,
                                          string sortingLayer) {
            return ctx.vfxFrameCore.TrySpawnAndPlayVFX_ToTarget(vfxName,
                                                          frames,
                                                          isLoop,
                                                          frameInterval,
                                                          target,
                                                          Vector3.zero,
                                                          sortingLayerName: sortingLayer);
        }

        public static void FlipX(VFXFrameAppContext ctx, int preSpawnVFXID, bool flipX) {
            ctx.vfxFrameCore.FlipX(preSpawnVFXID, flipX);
        }

        public static void SetDelayEndSec(VFXFrameAppContext ctx, int preSpawnVFXID, float delayEndSec) {
            ctx.vfxFrameCore.SetDelayEndSec(preSpawnVFXID, delayEndSec);
        }

        public static void SetFadingOut(VFXFrameAppContext ctx, int preSpawnVFXID, float fadingOutSec, EasingType easingType, EasingMode easingMode) {
            ctx.vfxFrameCore.SetFadingOut(preSpawnVFXID, fadingOutSec, easingType, easingMode);
        }

        public static void PlayVFXManualy(VFXFrameAppContext ctx, int preSpawnVFXID) {
            ctx.vfxFrameCore.TryPlayManualy(preSpawnVFXID);
        }

        public static void StopVFXManualy(VFXFrameAppContext ctx, int preSpawnVFXID) {
            ctx.vfxFrameCore.TryStopManualy(preSpawnVFXID);
        }

        public static void TearDown(VFXFrameAppContext ctx) {
            ctx.vfxFrameCore.TearDown();
        }

    }

}