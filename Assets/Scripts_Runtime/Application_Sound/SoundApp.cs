using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Phantom {

    public static class SoundApp {

        public static async Task LoadAssets(SoundAppContext ctx) {

            var handle = Addressables.LoadAssetAsync<GameObject>("Sound_AudioSource");
            var prefab = await handle.Task;
            ctx.audioSourcePrefab = prefab.GetComponent<AudioSource>();
            ctx.assetsHandle = handle;

            var bgmAudio = GameObject.Instantiate(ctx.audioSourcePrefab, ctx.root);
            bgmAudio.name = "BGMPlayer";
            ctx.bgmPlayer = bgmAudio;

            for (int i = 0; i < ctx.roleWalkPlayer.Length; i++) {
                var audio = GameObject.Instantiate(ctx.audioSourcePrefab, ctx.root);
                audio.name = "RoleWalkSE - " + i;
                ctx.roleWalkPlayer[i] = audio;
            }

            for (int i = 0; i < ctx.roleHurtPlayer.Length; i++) {
                var audio = GameObject.Instantiate(ctx.audioSourcePrefab, ctx.root);
                audio.name = "RoleHurtSE - " + i;
                ctx.roleHurtPlayer[i] = audio;
            }

            for (int i = 0; i < ctx.roleAttackPlayer.Length; i++) {
                var audio = GameObject.Instantiate(ctx.audioSourcePrefab, ctx.root);
                audio.name = "RoleAttackSE - " + i;
                ctx.roleAttackPlayer[i] = audio;
            }

            for (int i = 0; i < ctx.roleHitPlayer.Length; i++) {
                var audio = GameObject.Instantiate(ctx.audioSourcePrefab, ctx.root);
                audio.name = "RoleHitSE - " + i;
                ctx.roleHitPlayer[i] = audio;
            }

        }

        public static void ReleaseAssets(SoundAppContext ctx) {
            if (ctx.assetsHandle.IsValid()) {
                Addressables.Release(ctx.assetsHandle);
            }
        }

        public static void TearDown(SoundAppContext ctx) {
            foreach (var player in ctx.roleWalkPlayer) {
                player.Stop();
                GameObject.Destroy(player.gameObject);
            }
            foreach (var player in ctx.roleHurtPlayer) {
                player.Stop();
                GameObject.Destroy(player.gameObject);
            }
            foreach (var player in ctx.roleAttackPlayer) {
                player.Stop();
                GameObject.Destroy(player.gameObject);
            }
            foreach (var player in ctx.roleHitPlayer) {
                player.Stop();
                GameObject.Destroy(player.gameObject);
            }
            ctx.bgmPlayer.Stop();
            GameObject.Destroy(ctx.bgmPlayer.gameObject);
        }

        public static void BGM_PlayLoop(SoundAppContext ctx, AudioClip clip, int layer, float volume, bool replay) {
            var player = ctx.bgmPlayer;
            if (player.isPlaying && !replay) {
                return;
            }

            player.clip = clip;
            player.Play();
            player.loop = true;
            player.volume = volume;
        }

        public static void BGM_Stop(SoundAppContext ctx, int layer) {
            var player = ctx.bgmPlayer;
            player.Stop();
        }

        public static void Role_Walk(SoundAppContext ctx, AudioClip clip, float volume) {
            PlayWhenFree(ctx, ctx.roleWalkPlayer, clip, volume);
        }

        public static void Role_Hurt(SoundAppContext ctx, AudioClip clip, float volume) {
            PlayWhenFree(ctx, ctx.roleHurtPlayer, clip, volume);
        }

        public static void Role_Attack(SoundAppContext ctx, AudioClip clip, float volume) {
            PlayWhenFree(ctx, ctx.roleAttackPlayer, clip, volume);
        }

        public static void Role_Hit(SoundAppContext ctx, AudioClip clip, float volume) {
            PlayWhenFree(ctx, ctx.roleHitPlayer, clip, volume);
        }

        public static float GetVolume(Vector2 listenerPos, Vector2 hitPos, float thresholdDistance, float volume) {
            float dis = Vector2.Distance(listenerPos, hitPos);
            if (dis >= thresholdDistance) {
                return 0;
            }
            return (1 - dis / thresholdDistance) * volume;
        }

        public static void SetMuteAll(SoundAppContext ctx, bool isMute) {
            foreach (var player in ctx.roleWalkPlayer) {
                player.mute = isMute;
            }
            foreach (var player in ctx.roleHurtPlayer) {
                player.mute = isMute;
            }
            foreach (var player in ctx.roleAttackPlayer) {
                player.mute = isMute;
            }
            foreach (var player in ctx.roleHitPlayer) {
                player.mute = isMute;
            }
            ctx.bgmPlayer.mute = isMute;
        }

        static void PlayWhenFree(SoundAppContext ctx, AudioSource[] players, AudioClip clip, float volume) {
            if (clip == null || volume <= 0) {
                return;
            }
            foreach (var player in players) {
                if (!player.isPlaying) {
                    player.clip = clip;
                    player.Play();
                    player.volume = volume;
                    return;
                }
            }
        }

    }

}