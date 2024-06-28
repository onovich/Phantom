using UnityEngine;

namespace Phantom    {

    [CreateAssetMenu(fileName = "SoundTable", menuName = "Oshi/SoundTable")]
    public class SoundTable : ScriptableObject {

        [Header("Role SE")]
        public AudioClip roleMove;
        public float roleMoveVolume;

        public AudioClip roleDie;
        public float roleDieVolume;

        [Header("BGM")]
        public AudioClip bgmLoop;
        public float bgmVolume;

    }

}