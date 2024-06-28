using System;
using UnityEngine;
using UnityEngine.UI;
using TenonKit.Loom;
using System.Collections.Generic;

namespace Phantom.UI {

    public class Panel_GameInfo : MonoBehaviour, IPanel {

        [SerializeField] Text timeText;
        [SerializeField] RectTransform hpRoot;

        public void Ctor(int hpMax) {
        }

        public void RefreshTime(float time) {
            timeText.text = time.ToString("F0");
        }

        void OnDestroy() {
        }

    }

}