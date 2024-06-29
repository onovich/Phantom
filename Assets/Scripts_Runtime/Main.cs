using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.U2D;

namespace Phantom {

    public class Main : MonoBehaviour {

        [SerializeField] bool drawCameraGizmos;

        InputEntity inputEntity;

        AssetsInfraContext assetsInfraContext;
        TemplateInfraContext templateInfraContext;

        LoginBusinessContext loginBusinessContext;
        GameBusinessContext gameBusinessContext;

        UIAppContext uiAppContext;
        VFXParticelAppContext vfxParticelAppContext;
        VFXFrameAppContext vfxFrameAppContext;
        CameraAppContext cameraAppContext;
        SoundAppContext soundAppContext;
        GLAppContext glAppContext;

        bool isLoadedAssets;
        bool isTearDown;

        void Start() {

            isLoadedAssets = false;
            isTearDown = false;

            Canvas mainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
            Transform hudFakeCanvas = GameObject.Find("HUDFakeCanvas").transform;
            Camera mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
            Transform vfxRoot = GameObject.Find("VFXRoot").transform;
            Transform soundRoot = GameObject.Find("SoundRoot").transform;

            inputEntity = new InputEntity();

            loginBusinessContext = new LoginBusinessContext();
            gameBusinessContext = new GameBusinessContext();

            uiAppContext = new UIAppContext("UI", mainCanvas, hudFakeCanvas, mainCamera);
            vfxParticelAppContext = new VFXParticelAppContext("VFX_Particel", vfxRoot);
            vfxFrameAppContext = new VFXFrameAppContext(vfxRoot);
            cameraAppContext = new CameraAppContext(new Vector2(Screen.width, Screen.height));
            soundAppContext = new SoundAppContext(soundRoot);
            glAppContext = new GLAppContext();

            assetsInfraContext = new AssetsInfraContext();
            templateInfraContext = new TemplateInfraContext();

            // Inject
            loginBusinessContext.uiContext = uiAppContext;
            loginBusinessContext.soundContext = soundAppContext;

            gameBusinessContext.inputEntity = inputEntity;
            gameBusinessContext.assetsInfraContext = assetsInfraContext;
            gameBusinessContext.templateInfraContext = templateInfraContext;
            gameBusinessContext.uiContext = uiAppContext;
            gameBusinessContext.vfxParticelContext = vfxParticelAppContext;
            gameBusinessContext.vfxFrameContext = vfxFrameAppContext;
            gameBusinessContext.cameraContext = cameraAppContext;
            gameBusinessContext.soundContext = soundAppContext;
            gameBusinessContext.glContext = glAppContext;
            gameBusinessContext.mainCamera = mainCamera;

            cameraAppContext.templateInfraContext = templateInfraContext;

            // TODO Camera

            // Binding
            Binding();

            Action action = async () => {
                try {
                    await LoadAssets();
                    Init();
                    Enter();
                    isLoadedAssets = true;
                } catch (Exception e) {
                    GLog.LogError(e.ToString());
                }
            };
            action.Invoke();

        }

        void Enter() {
            LoginBusiness.Enter(loginBusinessContext);
        }

        void Update() {

            if (!isLoadedAssets) {
                return;
            }

            var dt = Time.deltaTime;
            LoginBusiness.Tick(loginBusinessContext, dt);
            GameBusiness.Tick(gameBusinessContext, dt);

            UIApp.LateTick(uiAppContext, dt);
 
        }

        void Init() {

            Application.targetFrameRate = 120;

            var inputEntity = this.inputEntity;
            inputEntity.Ctor();
            inputEntity.Keybinding_Set(InputKeyEnum.MoveLeft, new KeyCode[] { KeyCode.A, KeyCode.LeftArrow });
            inputEntity.Keybinding_Set(InputKeyEnum.MoveRight, new KeyCode[] { KeyCode.D, KeyCode.RightArrow });
            inputEntity.Keybinding_Set(InputKeyEnum.MoveUp, new KeyCode[] { KeyCode.W, KeyCode.UpArrow });
            inputEntity.Keybinding_Set(InputKeyEnum.MoveDown, new KeyCode[] { KeyCode.S, KeyCode.DownArrow });
            inputEntity.Keybinding_Set(InputKeyEnum.Cast, new KeyCode[] { KeyCode.Space });

            GameBusiness.Init(gameBusinessContext);

            UIApp.Init(uiAppContext);
            VFXParticelApp.Init(vfxParticelAppContext);

        }

        void Binding() {
            var uiEvt = uiAppContext.evt;

            // UI
            // - Login
            uiEvt.Login_OnStartGameClickHandle += () => {
                LoginBusiness.Exit(loginBusinessContext);
                GameBusiness.StartGame(gameBusinessContext);
            };

            uiEvt.Login_OnExitGameClickHandle += () => {
                LoginBusiness.ExitApplication(loginBusinessContext);
            };

            // - GameOver
            uiEvt.GameOver_OnRestartGameClickHandle += () => {
                GameBusiness.UIGameOver_OnRestartGame(gameBusinessContext);
            };

            uiEvt.GameOver_OnExitGameClickHandle += () => {
                GameBusiness.UIGameOver_OnExitGameClick(gameBusinessContext);
            };

        }

        async Task LoadAssets() {
            await UIApp.LoadAssets(uiAppContext);
            await VFXParticelApp.LoadAssets(vfxParticelAppContext);
            await AssetsInfra.LoadAssets(assetsInfraContext);
            await TemplateInfra.LoadAssets(templateInfraContext);
            await SoundApp.LoadAssets(soundAppContext);
        }

        void OnApplicationQuit() {
            TearDown();
        }

        void OnDestroy() {
            TearDown();
        }

        void TearDown() {
            if (isTearDown) {
                return;
            }
            isTearDown = true;

            uiAppContext.evt.Clear();

            AssetsInfra.ReleaseAssets(assetsInfraContext);
            TemplateInfra.Release(templateInfraContext);
            SoundApp.ReleaseAssets(soundAppContext);
            VFXParticelApp.ReleaseAssets(vfxParticelAppContext);
            UIApp.ReleaseAssets(uiAppContext);

            GameBusiness.TearDown(gameBusinessContext);
            SoundApp.TearDown(soundAppContext);
            VFXParticelApp.TearDown(vfxParticelAppContext);
            VFXFrameApp.TearDown(vfxFrameAppContext);
            UIApp.TearDown(uiAppContext);
        }

        void OnDrawGizmos() {
            GameBusiness.OnDrawGizmos(gameBusinessContext, drawCameraGizmos);
        }

    }

}