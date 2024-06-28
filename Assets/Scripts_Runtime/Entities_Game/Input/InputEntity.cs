using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Phantom {

    public class InputEntity {

        public Vector2 moveAxis;

        InputKeybindingComponent keybindingCom;

        public void Ctor() {
            keybindingCom.Ctor();
        }

        public void ProcessInput(Camera camera, float dt) {

            if (keybindingCom.IsKeyDown(InputKeyEnum.MoveLeft)) {
                moveAxis.x = -1;
            }
            if (keybindingCom.IsKeyDown(InputKeyEnum.MoveRight)) {
                moveAxis.x = 1;
            }
            if (keybindingCom.IsKeyDown(InputKeyEnum.MoveUp)) {
                moveAxis.y = 1;
            }
            if (keybindingCom.IsKeyDown(InputKeyEnum.MoveDown)) {
                moveAxis.y = -1;
            }
        }

        public void Keybinding_Set(InputKeyEnum key, KeyCode[] keyCodes) {
            keybindingCom.Bind(key, keyCodes);
        }

        public void Reset() {
            moveAxis = Vector2.zero;
        }

    }

}