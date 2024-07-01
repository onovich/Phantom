using System;
using UnityEngine;

namespace Phantom {

    public class RoleEntity : MonoBehaviour {

        // Base Info
        public int entityID;
        public int typeID;
        public string typeName;
        public AllyStatus allyStatus;

        // Attr
        public Vector2 faceDir;
        public float attackDistance;
        public int hpMax;
        public int hp;

        // State
        public bool needTearDown;

        // FSM
        public RoleFSMComponent fsmCom;

        // Input
        public RoleInputComponent inputCom;

        // Render
        [SerializeField] public Transform body;
        RoleMod roleMod;
        public RoleMod RoleMod => roleMod;

        // VFX
        public string deadVFXName;
        public float deadVFXDuration;

        public RoleVFXComponent vfxCom;

        // Pos
        public Vector2 Pos => Pos_GetPos();

        // Path
        public Vector2[] path;
        public int pathLen;

        public void Ctor() {
            fsmCom = new RoleFSMComponent();
            inputCom = new RoleInputComponent();
            vfxCom = new RoleVFXComponent();
            path = new Vector2[100];
        }

        // Pos
        public void Pos_SetPos(Vector2 pos) {
            transform.position = pos;
        }

        Vector2 Pos_GetPos() {
            return transform.position;
        }

        // Move
        public void Move_ApplyMove(Func<Vector2, bool> walkable) {
            var axis = inputCom.moveAxis;
            if (axis == Vector2.zero) {
                return;
            }
            axis.Normalize();
            var target = Pos + axis;
            if (!walkable(target)) {
                return;
            }
            transform.position += new Vector3(axis.x, axis.y, 0);
        }

        // Color
        public void Color_SetAlpha(float alpha) {
            roleMod?.SetColorAlpha(alpha);
        }

        // FSM
        public RoleFSMStatus FSM_GetStatus() {
            return fsmCom.status;
        }

        public RoleFSMComponent FSM_GetComponent() {
            return fsmCom;
        }

        public void FSM_EnterIdle() {
            fsmCom.EnterIdle();
        }

        public void FSM_EnterDead() {
            fsmCom.EnterDead();
        }

        // Mod
        public void Mod_Set(RoleMod mod) {
            roleMod = mod;
        }

        // VFX
        public void TearDown() {
            vfxCom.Clear();
            roleMod?.TearDown();
            Destroy(this.gameObject);
        }

    }

}