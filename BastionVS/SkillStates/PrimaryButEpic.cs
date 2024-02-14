using RoR2;
using UnityEngine;

namespace Bastion
{
    public class PrimaryButEpic : BaseMeleeAttack
    {
        private float baseSwingDelay = 0.12f;

        private float baseMaxSwingTime = 0.25f;

        private float baseEarlyExitTime = 0.38f;

        private float baseDamageCoefficient = 2;

        public override void OnEnter()
        {
            hitboxGroupName = "SwordGroup";

            damageType = DamageType.Generic;
            damageCoefficient = baseDamageCoefficient;
            procCoefficient = 1f;
            pushForce = 100f;
            bonusForce = Vector3.zero;
            baseDuration = 0.5f;

            attackStartPercentTime = baseSwingDelay / baseDuration;// 0.2f;
            attackEndPercentTime = baseEarlyExitTime / baseDuration;// 0.4f;

            earlyExitPercentTime = baseEarlyExitTime / baseDuration;// 0.6f;

            hitStopDuration = 0.012f;
            attackRecoil = 0.5f;
            hitHopVelocity = 4f;

            swingSoundString = "HenrySwordSwing";
            hitSoundString = "";
            muzzleString = swingIndex % 2 == 0 ? "SwingLeft" : "SwingRight";
            playbackRateParam = "Slash.playbackRate";
            swingEffectPrefab = null;// Prefabs.swingEffect;
            hitEffectPrefab = Prefabs.swingImpact;

            //impactSound = HenryAssets.swordHitSoundEvent.index;

            base.OnEnter();
        }

        protected override void PlaySwingEffect()
        {
            base.PlaySwingEffect();

            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Prefabs.swingEffect, transform);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.Euler(90f, 240f, 0f);
            gameObject.GetComponent<ScaleParticleSystemDuration>().newDuration = 0.3f;
        }

        protected override void PlayAttackAnimation()
        {
            base.PlayAttackAnimation();

            if (base.isGrounded & !base.GetModelAnimator().GetBool("isMoving"))
            {
                base.PlayAnimation("FullBody, Override", swingIndex == 0 ? "SwingL" : "SwingR", "M1", this.duration);
            }
            else
            {
                base.PlayAnimation("Gesture, Override", swingIndex == 0 ? "SwingL" : "SwingR", "M1", this.duration);
            }
        }
    }
}
