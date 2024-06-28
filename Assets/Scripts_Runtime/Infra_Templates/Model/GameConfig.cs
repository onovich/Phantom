using System;
using MortiseFrame.Swing;
using UnityEngine;

namespace Phantom {

    [CreateAssetMenu(fileName = "TM_GameConfig", menuName = "Phantom/GameConfig")]
    public class GameConfig : ScriptableObject {

        // Game
        [Header("Game Config")]
        public float gameResetEnterTime;

        // Role
        [Header("Role Config")]
        public int ownerRoleTypeID;

        // Map
        [Header("Map Config")]
        public int originalMapTypeID;

        // Camera
        [Header("DeadZone Config")]
        public Vector2 cameraDeadZoneNormalizedSize;

        [Header("Shake Config")]
        public float cameraShakeFrequency_roleDamage;
        public float cameraShakeAmplitude_roleDamage;
        public float cameraShakeDuration_roleDamage;
        public EasingType cameraShakeEasingType_roleDamage;
        public EasingMode cameraShakeEasingMode_roleDamage;

    }

}