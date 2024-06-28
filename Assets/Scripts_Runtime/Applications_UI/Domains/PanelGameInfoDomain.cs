using System;
using UnityEngine;
using UnityEngine.UI;

namespace Phantom.UI {

    public static class PanelGameInfoDomain {

        public static void Open(UIAppContext ctx, int hpMax) {
            var has = ctx.UniquePanel_TryGet<Panel_GameInfo>(out var panel);
            if (has) {
                return;
            }

            panel = ctx.uiCore.UniquePanel_Open<Panel_GameInfo>();
            panel.Ctor(hpMax);
        }

        public static void RefreshTime(UIAppContext ctx, float time) {
            var has = ctx.UniquePanel_TryGet<Panel_GameInfo>(out var panel);
            if (!has) {
                return;
            }
            panel.RefreshTime(time);
        }

        public static void Close(UIAppContext ctx) {
            var has = ctx.UniquePanel_TryGet<Panel_GameOver>(out var panel);
            if (!has) {
                return;
            }
            ctx.uiCore.UniquePanel_Close<Panel_GameOver>();
        }

    }

}