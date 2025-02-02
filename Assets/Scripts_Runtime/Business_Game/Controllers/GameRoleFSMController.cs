using MortiseFrame.Swing;
using UnityEngine;

namespace Phantom {

    public static class GameRoleFSMController {

        public static void FixedTickFSM(GameBusinessContext ctx, RoleEntity role, float dt) {

            FixedTickFSM_Any(ctx, role, dt);

            RoleFSMStatus status = role.FSM_GetStatus();
            if (status == RoleFSMStatus.Idle) {
                FixedTickFSM_Idle(ctx, role, dt);
            } else if (status == RoleFSMStatus.Dead) {
                FixedTickFSM_Dead(ctx, role, dt);
            } else {
                GLog.LogError($"GameRoleFSMController.FixedTickFSM: unknown status: {status}");
            }

        }

        static void FixedTickFSM_Any(GameBusinessContext ctx, RoleEntity role, float dt) {

        }

        static void FixedTickFSM_Idle(GameBusinessContext ctx, RoleEntity role, float dt) {
            RoleFSMComponent fsm = role.FSM_GetComponent();
            if (fsm.idle_isEntering) {
                fsm.idle_isEntering = false;
            }
            var input = ctx.inputEntity;
            if (input.moveAxis == Vector2.zero) {
                return;
            }

            // Calculate Path
            GameRoleDomain.CalculatePathToOwner(ctx, role);

            // Move
            GameRoleDomain.MoveByPath(ctx, role, dt);
            GameRoleDomain.MoveByInput(ctx, role, dt);
            GameRoleDomain.ApplyDamage(ctx, role);
            GameRoleDomain.ApplyConstraint(ctx, role);
        }

        static void FixedTickFSM_Dead(GameBusinessContext ctx, RoleEntity role, float dt) {
            RoleFSMComponent fsm = role.FSM_GetComponent();
            if (fsm.dead_isEntering) {
                fsm.dead_isEntering = false;
            }

            // VFX
            VFXParticelApp.AddVFXToWorld(ctx.vfxParticelContext, role.deadVFXName, role.deadVFXDuration, role.Pos);

            // Camera
            GameCameraDomain.ShakeOnce(ctx);
            role.needTearDown = true;
        }

    }

}