using System;
using UnityEngine;

namespace Phantom {

    [CreateAssetMenu(fileName = "TM_Role", menuName = "Phantom/RoleTM")]
    public class RoleTM : ScriptableObject {

        [Header("Role Info")]
        public int typeID;
        public string typeName;
        public AllyStatus allyStatus;

        [Header("Role Attributes")]
        public float attackDistance;
        public int hpMax;

        [Header("Role Render")]
        public RoleMod mod;
        public GameObject deadVFX;
        public float deadVFXDuration;
    }

}