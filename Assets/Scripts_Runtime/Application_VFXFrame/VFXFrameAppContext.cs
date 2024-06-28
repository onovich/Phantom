using System;
using System.Threading.Tasks;
using TenonKit.Prism;
using UnityEngine;

namespace Phantom {

    public class VFXFrameAppContext {

        // Core
        public VFXFrameCore vfxFrameCore;

        public VFXFrameAppContext(Transform root) {
            vfxFrameCore = new VFXFrameCore(root);
        }

    }

}