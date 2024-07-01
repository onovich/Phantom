namespace Phantom {

    public static class GameInputDomain {

        public static void Player_BakeInput(GameBusinessContext ctx, float dt) {
            InputEntity inputEntity = ctx.inputEntity;
            inputEntity.ProcessInput(ctx.mainCamera, dt);
        }

        public static void Role_BakeInput(GameBusinessContext ctx, RoleEntity owner) {
            InputEntity inputEntity = ctx.inputEntity;
            ref RoleInputComponent inputCom = ref owner.inputCom;
            inputCom.moveAxis = inputEntity.moveAxis;
        }

        public static void Player_ResetInput(GameBusinessContext ctx) {
            InputEntity inputEntity = ctx.inputEntity;
            inputEntity.Reset();
        }

        public static void Role_ResetInput(GameBusinessContext ctx, RoleEntity role) {
            ref RoleInputComponent inputCom = ref role.inputCom;
            inputCom.Reset();
        }

    }

}