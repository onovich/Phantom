using System;
using System.Collections.Generic;
using UnityEngine;

namespace Phantom {

    public class RoleVFXComponent {

        public List<int> vfxIDList;
        public RoleVFXComponent() {
            vfxIDList = new List<int>();
        }

        public void AddVFX(int id) {
            vfxIDList.Add(id);
        }

        public void ForEach(Action<int> action) {
            vfxIDList.ForEach(action);
        }

        public void Clear() {
            vfxIDList.Clear();
        }

    }

}