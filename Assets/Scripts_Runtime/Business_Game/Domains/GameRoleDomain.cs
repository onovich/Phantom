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
            var startGrid = PathFindingGridUtil.WorldToGrid(role.Pos, -map.mapSize / 2, map.gridUnit);
            var endGrid = PathFindingGridUtil.WorldToGrid(owner.Pos, -map.mapSize / 2, map.gridUnit);
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
            if (role.allyStatus != AllyStatus.Enemy) {
                return;
            }
            RoleEntity owner = ctx.Role_GetOwner();
            if (owner == null) {
                return;
            }

            var distSqr = (owner.Pos - role.Pos).sqrMagnitude;
            if (distSqr > role.attackDistance * role.attackDistance) {
                return;
            }

            owner.hp -= 1;
            GameCameraDomain.ShakeOnce(ctx);

            owner.RoleMod?.PlayHit();

            if (owner.hp <= 0) {
                owner.FSM_EnterDead();
            }
        }

        public static void UnSpawn(GameBusinessContext ctx, RoleEntity role) {
            ctx.roleRepo.Remove(role);
            role.TearDown();
        }

        public static void MoveByInput(GameBusinessContext ctx, RoleEntity role, float dt) {
            role.Move_ApplyMove(dt);
        }

        public static void MoveByPath(GameBusinessContext ctx, RoleEntity role, float dt) {
            var owner = ctx.Role_GetOwner();
            if (owner == null) {
                return;
            }
            if (owner.inputCom.moveAxis == Vector2.zero) {
                role.inputCom.moveAxis = Vector2.zero;
                return;
            }
            if (role.allyStatus != AllyStatus.Enemy) {
                return;
            }
            var path = role.path;
            var pathLen = role.pathLen;
            if (pathLen <= 1) {
                role.inputCom.moveAxis = Vector2.zero;
                return;
            }

            var targetGrid = path[1];
            var targetPos = PathFindingGridUtil.GridToWorld_LD(targetGrid, -ctx.currentMapEntity.mapSize / 2, ctx.currentMapEntity.gridUnit);
            var dir = (targetPos - role.Pos).normalized;
            role.inputCom.moveAxis = dir;
        }

    }

}