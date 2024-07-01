using UnityEngine;

namespace Phantom {

    public static class GameGameDomain {

        public static void NewGame(GameBusinessContext ctx) {

            var config = ctx.templateInfraContext.Config_Get();

            // Game
            var game = ctx.gameEntity;
            game.fsmComponent.Gaming_Enter();

            // Map
            var mapTypeID = config.originalMapTypeID;
            var map = GameMapDomain.Spawn(ctx, mapTypeID);
            var has = ctx.templateInfraContext.Map_TryGet(mapTypeID, out var mapTM);
            if (!has) {
                GLog.LogError($"MapTM Not Found {mapTypeID}");
            }

            // Role
            var player = ctx.playerEntity;

            // - Owner
            var spawnPoint = mapTM.ownerSpawnPoint;
            var owner = GameRoleDomain.Spawn(ctx,
                                             config.ownerRoleTypeID,
                                             spawnPoint);
            player.ownerRoleEntityID = owner.entityID;
            ctx.ownerSpawnPoint = spawnPoint;

            // - Enemy
            var enemyTMArr = mapTM.enemyArray;
            var enemyPosArr = mapTM.enemyPosArray;
            for (int i = 0; i < enemyTMArr.Length; i++) {
                var enemyTM = enemyTMArr[i];
                var enemyPos = enemyPosArr[i];
                var enemy = GameRoleDomain.Spawn(ctx,
                                                 enemyTM.typeID,
                                                 enemyPos);
            }

            // Camera
            var mainCamera = ctx.mainCamera;
            var cameraID = CameraApp.CreateMainCamera(ctx.cameraContext,
                                                          mainCamera.transform.rotation.eulerAngles.z,
                                                          mainCamera.orthographicSize,
                                                          mainCamera.aspect,
                                                          mainCamera.transform.position,
                                                          mapTM.cameraConfinerWorldMax, mapTM.cameraConfinerWorldMax,
                                                          new Vector2(0, 0));
            CameraApp.SetCurrentCamera(ctx.cameraContext, cameraID);

            // UI
            UIApp.GameInfo_Open(ctx.uiContext, owner.hpMax);

            // BGM
            var soundTable = ctx.templateInfraContext.SoundTable_Get();
            SoundApp.BGM_PlayLoop(ctx.soundContext, soundTable.bgmLoop, 1, soundTable.bgmVolume, false);

        }

        public static void ApplyGameTime(GameBusinessContext ctx, float dt) {
            var game = ctx.gameEntity;
            var fsm = game.fsmComponent;

            fsm.Gaming_IncTimer(dt);
        }

        public static void ApplyGameOver(GameBusinessContext ctx, float dt) {
            var game = ctx.gameEntity;
            var fsm = game.fsmComponent;

            fsm.GameOver_DecTimer(dt);

            var enterTime = fsm.gameOver_enterTime;
            if (enterTime <= 0) {
                UIApp.GameOver_Open(ctx.uiContext, fsm.gameOver_result);
            }
        }

        public static void RestartGame(GameBusinessContext ctx) {
            var game = ctx.gameEntity;
            var fsm = game.fsmComponent;
            ExitGame(ctx);
            NewGame(ctx);
        }

        public static void ApplyGameResult(GameBusinessContext ctx) {
            var owner = ctx.Role_GetOwner();
            var game = ctx.gameEntity;
            var config = ctx.templateInfraContext.Config_Get();
            if (owner == null || owner.needTearDown) {
                // game.fsmComponent.GameOver_Enter(config.gameResetEnterTime, GameResult.Lose);
                RestartGame(ctx);
            }
        }

        public static void ExitGame(GameBusinessContext ctx) {
            // Game
            var game = ctx.gameEntity;
            game.fsmComponent.NotInGame_Enter();

            // Map
            GameMapDomain.UnSpawn(ctx);

            // Role
            int roleLen = ctx.roleRepo.TakeAll(out var roleArr);
            for (int i = 0; i < roleLen; i++) {
                var role = roleArr[i];
                GameRoleDomain.UnSpawn(ctx, role);
            }

            // UI
            UIApp.GameOver_Close(ctx.uiContext);
            UIApp.GameInfo_Close(ctx.uiContext);

        }

    }
}