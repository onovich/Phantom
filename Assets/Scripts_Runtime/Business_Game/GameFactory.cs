using UnityEngine;

namespace Phantom {

    public static class GameFactory {

        public static MapEntity Map_Spawn(TemplateInfraContext templateInfraContext,
                                 AssetsInfraContext assetsInfraContext,
                                 int typeID) {

            var has = templateInfraContext.Map_TryGet(typeID, out var mapTM);
            if (!has) {
                GLog.LogError($"Map {typeID} not found");
            }

            var prefab = assetsInfraContext.Entity_GetMap();
            var map = GameObject.Instantiate(prefab).GetComponent<MapEntity>();
            map.Ctor();
            map.typeID = typeID;
            map.SetSize(mapTM.mapSize);
            map.gridUnit = mapTM.gridUnit;
            return map;
        }

        public static RoleEntity Role_Spawn(TemplateInfraContext templateInfraContext,
                                 AssetsInfraContext assetsInfraContext,
                                 IDRecordService idRecordService,
                                 int typeID,
                                 Vector2 pos,
                                 Vector2 direction) {

            var has = templateInfraContext.Role_TryGet(typeID, out var roleTM);
            if (!has) {
                GLog.LogError($"Role {typeID} not found");
            }

            var prefab = assetsInfraContext.Entity_GetRole();
            var role = GameObject.Instantiate(prefab).GetComponent<RoleEntity>();
            role.Ctor();

            // Base Info
            role.entityID = idRecordService.PickRoleEntityID();
            role.typeID = typeID;
            role.allyStatus = roleTM.allyStatus;

            // Set Attr
            role.attackDistance = roleTM.attackDistance;
            role.hpMax = roleTM.hpMax;
            role.hp = role.hpMax;
            role.typeName = roleTM.typeName;

            // Rename
            role.gameObject.name = $"{role.typeName} - {role.entityID}";

            // Set Pos
            role.Pos_SetPos(pos);

            // Set Mod
            var modPrefab = roleTM.mod;
            if (modPrefab != null) {
                var mod = GameObject.Instantiate(modPrefab, role.body).GetComponent<RoleMod>();
                role.Mod_Set(mod);
            }

            // Set FSM
            role.FSM_EnterIdle();

            // Set VFX
            role.deadVFXName = roleTM.deadVFX.name;
            role.deadVFXDuration = roleTM.deadVFXDuration;

            return role;
        }

    }

}