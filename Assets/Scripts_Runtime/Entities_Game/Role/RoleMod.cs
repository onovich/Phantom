using System;
using UnityEngine;

namespace Phantom {

    public class RoleMod : MonoBehaviour {

        [SerializeField] SpriteRenderer spr;
        [SerializeField] Animator anim;

        int attackIndex;

        public void SetSprite(Sprite sprite) {
            spr.sprite = sprite;
        }

        public void SetColorAlpha(float alpha) {
            spr.color = new Color(spr.color.r, spr.color.g, spr.color.b, alpha);
        }

        public void PlayIdle() {
            anim.Play("Idle");
        }

        public void PlayAttack() {
            if (attackIndex == 0) {
                PlayAttack1();
                attackIndex = 1;
            } else {
                PlayAttack2();
                attackIndex = 0;
            }
        }

        public void PlayAttack1() {
            anim.Play("Attack1");
        }

        public void PlayAttack2() {
            anim.Play("Attack2");
        }

        public void PlayBatFail() {
            anim.Play("Bat_Fail");
        }

        public void PlayBat() {
            anim.Play("Bat");
        }

        public void PlayHit() {
            anim.Play("Hit");
        }

        public void Anim_SetMovement(float speed) {
            anim.SetFloat("HorizontalMovement", speed);
        }

        public void TearDown() {
            Destroy(this.gameObject);
        }

    }

}