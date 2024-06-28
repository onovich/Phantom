using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Phantom {

    public class SoundAppContext {

        public Transform root;
        public AudioSource audioSourcePrefab;

        public AudioSource bgmPlayer;

        public AudioSource[] roleWalkPlayer; // Tap 
        public AudioSource[] roleHurtPlayer; // Hurt / Dead
        public AudioSource[] roleAttackPlayer; // Swoosh / Slash / Clang
        public AudioSource[] roleHitPlayer; // SwooshBreak / ClangBradk

        public AsyncOperationHandle assetsHandle;

        public SoundAppContext(Transform soundRoot) {
            roleWalkPlayer = new AudioSource[4];
            roleHurtPlayer = new AudioSource[4];
            roleAttackPlayer = new AudioSource[4];
            roleHitPlayer = new AudioSource[4];
            this.root = soundRoot;
        }

    }

}