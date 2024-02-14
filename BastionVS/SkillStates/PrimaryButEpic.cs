using RoR2;
using UnityEngine;

namespace Bastion
{
    public class PrimaryButEpic : BaseMeleeAttack
    {
        private float baseSwingDelay = 0.12f;

        private float baseMaxSwingTime = 0.25f;

        private float baseEarlyExitTime = 0.38f;
        
        private float baseDamageCoefficient = 2.5f;

        public override void OnEnter()
        {
            hitboxGroupName = "Swing";

            damageType = DamageType.Generic;
            damageCoefficient = baseDamageCoefficient;
            procCoefficient = 1f;
            pushForce = 100f;
            bonusForce = Vector3.zero;
            baseDuration = 0.5f;

            attackStartPercentTime = baseSwingDelay / baseDuration;// 0.2f;
            attackEndPercentTime = baseMaxSwingTime / baseDuration;// 0.4f;

            earlyExitPercentTime = baseEarlyExitTime / baseDuration;// 0.6f;

            hitStopDuration = 0.05f;
            attackRecoil = 0.5f;
            hitHopVelocity = 4f;
            
            swingSoundString = "";
            hitSoundString = "";
            //muzzleString = swingIndex % 2 == 0 ? "SwingLeft" : "SwingRight";
            playbackRateParam = "M1";
            swingEffectPrefab = null;// Prefabs.swingEffect;
            hitEffectPrefab = Prefabs.swingImpact;

            AkSoundEngine.PostEvent(Sounds.Play_Bastian_Swing, base.gameObject);

            //impactSound = HenryAssets.swordHitSoundEvent.index;

            base.OnEnter();
        }

        protected override void PlaySwingEffect()
        {
            Transform transform = base.FindModelChild("swingMuzzle");
            int num = (this.swingIndex == 0) ? 1 : -1;
            transform.localScale = new Vector3(num, 1f, 1f);
            transform.localRotation = Quaternion.Euler(0f, -30 * num, 15 * num);
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
