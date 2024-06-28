using UnityEngine;

namespace Phantom {

    public static class GameRoleDomain {

        public static RoleEntity Spawn(GameBusinessContext ctx, int typeID, Vector2 pos, Vector2 dir) {
            var map = ctx.currentMapEntity;
            var role = GameFactory.Role_Spawn(ctx.templateInfraContext,
                                              ctx.assetsInfraContext,
                                              ctx.idRecordService,
                                              typeID,
                                              pos,
                                              dir);

            ctx.roleRepo.Add(role);
            return role;
        }

        public static void CheckAndUnSpawn(GameBusinessContext ctx, RoleEntity role) {
            if (role.needTearDown) {
                UnSpawn(ctx, role);
            }
        }

        public static void ApplyDamage(GameBusinessContext ctx, RoleEntity role) {
            RoleEntity target;
            if (role.allyStatus == AllyStatus.Enemy) {
                target = ctx.Role_GetOwner();
            } else {
                target = ctx.Role_GetNearestEnemy(role);
            }

            if (target == null) {
                return;
            }

            var distSqr = (target.Pos - role.Pos).sqrMagnitude;
            if (distSqr > role.attackDistance * role.attackDistance) {
                return;
            }

            target.hp -= 1;
            GameCameraDomain.ShakeOnce(ctx);

            if (target.allyStatus == AllyStatus.Player) {
                target.RoleMod?.PlayHit();
                target.Move_SetFace(-role.faceDir);
            }

            if (target.hp <= 0) {
                target.FSM_EnterDead();
            }
        }

        public static void UnSpawn(GameBusinessContext ctx, RoleEntity role) {
            ctx.roleRepo.Remove(role);
            role.TearDown();
        }

        public static void ApplyMove(GameBusinessContext ctx, RoleEntity role, float dt) {
            role.Move_ApplyMove(dt);
        }

    }

}