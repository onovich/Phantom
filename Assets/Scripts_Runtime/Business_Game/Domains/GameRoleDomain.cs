using System;
using MortiseFrame.Compass;
using MortiseFrame.Compass.Extension;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Phantom {

    public static class GameRoleDomain {

        public static RoleEntity Spawn(GameBusinessContext ctx, int typeID, Vector2 pos) {
            var role = GameFactory.Role_Spawn(ctx.templateInfraContext,
                                              ctx.assetsInfraContext,
                                              ctx.idRecordService,
                                              typeID,
                                              pos);

            ctx.roleRepo.Add(role);
            return role;
        }

        public static void CheckAndUnSpawn(GameBusinessContext ctx, RoleEntity role) {
            if (role.needTearDown) {
                UnSpawn(ctx, role);
            }
        }

        public static void CalculatePathToOwner(GameBusinessContext ctx, RoleEntity role) {
            var owner = ctx.Role_GetOwner();
            if (owner == null) {
                return;
            }

            if (role.allyStatus == AllyStatus.Player) {
                return;
            }
            var path = role.path;
            Array.Clear(path, 0, path.Length);

            var map = ctx.currentMapEntity;
            var startGrid = PathFindingGridUtil.WorldToGrid(role.Pos, -map.mapSize, map.gridUnit);
            var endGrid = PathFindingGridUtil.WorldToGrid(owner.Pos, -map.mapSize, map.gridUnit);
            var gridUnit = map.gridUnit;
            int mapWidth = map.obstacleDataWidth;
            int mapHeight = PathFindingMapUtil.GetMapHeight(map.obstacleData, mapWidth);

            var pathLen = PathFindingService.FindPath(startGrid,
                                                      endGrid,
                                                      (x, y) => GameMapDomain.IsWalkable(ctx, x, y),
                                                      mapWidth,
                                                      mapHeight,
                                                      PathFindingDirection.FourDirections,
                                                      true,
                                                      path);
            role.pathLen = pathLen;
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