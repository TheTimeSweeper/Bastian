using RoR2;
using UnityEngine;

namespace Bastian
{
    public class PrimaryButEpic : BaseMeleeAttack
    {        
        private float baseDamageCoefficient => Configs.M1_Damage.Value;
        private BlastDamageBuildupController blastor;

        public override void OnEnter()
        {
            hitboxGroupName = "Swing";

            damageType = DamageType.Generic;
            damageCoefficient = baseDamageCoefficient;
            procCoefficient = 1f;
            pushForce = 100f;
            bonusForce = Vector3.zero;
            baseDuration = Configs.M1_Duration.Value;

            attackStartPercentTime = 0.12f;
            attackEndPercentTime = 0.34f;

            earlyExitPercentTime = 0.57f;

            hitStopDuration = 0.05f;
            attackRecoil = 0.5f;
            hitHopVelocity = 4f;
            
            swingSoundString = "";
            hitSoundString = "Play_Bastian_Punch_Impact";
            //muzzleString = swingIndex % 2 == 0 ? "SwingLeft" : "SwingRight";
            playbackRateParam = "M1";
            swingEffectPrefab = null;// Prefabs.swingEffect;
            hitEffectPrefab = Prefabs.swingImpact;
            impactSound = RoR2.Audio.NetworkSoundEventIndex.Invalid;
            
            AkSoundEngine.PostEvent(Sounds.Play_Bastian_Swing, base.gameObject);

            //impactSound = HenryAssets.swordHitSoundEvent.index;

            base.OnEnter();

            blastor = GetComponent<BlastDamageBuildupController>();

            //R2API.DamageAPI.AddModdedDamageType(attack, Prefabs.buildupDamage);
        }
        
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            StartAimMode(2);
        }

        protected override void PlaySwingEffect()
        {
            Transform transform = base.FindModelChild("swingMuzzle");
            float num = (this.swingIndex == 0) ? 1.5f : -1.5f;
            transform.localScale = new Vector3(num, 1.5f, 1.5f);
            transform.localRotation = Quaternion.Euler(0f, -30 * num, 15 * num);
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Prefabs.swingEffect, transform);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.Euler(90f, 240f, 0f);
            gameObject.GetComponent<ScaleParticleSystemDuration>().newDuration = 0.3f;
        }

        protected override void OnHitEnemyAuthority()
        {
            base.OnHitEnemyAuthority();
            if (blastor)
            {
                blastor.FillChargeAuthority(damageCoefficient);
            }
        }

        protected override void PlayAttackAnimation()
        {
            base.PlayAttackAnimation();

            base.PlayAnimation("Gesture, Override", swingIndex == 0 ? "SwingL" : "SwingR", "M1", this.duration);
            if (base.isGrounded & !base.GetModelAnimator().GetBool("isMoving"))
            {
                base.PlayAnimation("FullBody, Override", swingIndex == 0 ? "SwingL" : "SwingR", "M1", this.duration);
            }
        }
    }
}
