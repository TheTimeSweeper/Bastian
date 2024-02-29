using System;
using System.Reflection;
using System.Collections.Generic;
using BepInEx;
using R2API;
using R2API.Utils;
using EntityStates;
using RoR2;
using RoR2.Projectile;
using UnityEngine;
using KinematicCharacterController;
using BepInEx.Configuration;
using RoR2.UI;
using UnityEngine.UI;
using System.Security;
using System.Security.Permissions;
using System.Linq;
using R2API.ContentManagement;
using UnityEngine.AddressableAssets;

namespace Bastian
{
    class Primary : BaseSkillState
    {
        private float duration = 0.5f;

        private float swingDelay = 0.12f;
        
        private float maxSwingTime = 0.25f;

        private float minDuration = 0.38f;

        private float damageCoefficient = 2;
        public int attackIndex = 0;
        private OverlapAttack attack;

        public override void OnEnter()
        {
            base.OnEnter();
            duration /= base.attackSpeedStat;
            swingDelay /= base.attackSpeedStat;
            minDuration /= base.attackSpeedStat;
            base.StartAimMode(2);
            if (base.isAuthority)
            {
                attack = base.InitMeleeOverlap(damageCoefficient, Prefabs.swingImpact, base.GetModelTransform(), "Swing");
            }
            if (base.isGrounded & !base.GetModelAnimator().GetBool("isMoving"))
            {
                base.PlayAnimation("FullBody, Override", attackIndex == 0 ? "SwingL" : "SwingR", "M1", this.duration);
            }
            else
            {
                base.PlayAnimation("Gesture, Override", attackIndex == 0 ? "SwingL" : "SwingR", "M1", this.duration);
            }
            AkSoundEngine.PostEvent(Sounds.Play_Bastian_Swing, base.gameObject);
            Transform transform = base.FindModelChild("swingMuzzle");
            int num = (this.attackIndex == 0) ? 1 : -1;
            transform.localScale = new Vector3(num, 1f, 1f);
            transform.localRotation = Quaternion.Euler(0f, -30 * num, 15 * num);
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Prefabs.swingEffect, transform);
            gameObject.transform.localPosition = Vector3.zero;
            gameObject.transform.localRotation = Quaternion.Euler(90f, 240f, 0f);
            gameObject.GetComponent<ScaleParticleSystemDuration>().newDuration = 0.3f;
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            base.characterBody.isSprinting = false;//what the fuck
            if (base.isAuthority)
            {
                if (base.fixedAge >= swingDelay && base.fixedAge <= maxSwingTime)
                {
                    if (attack.Fire() && !base.isGrounded)
                    {
                        base.SmallHop(base.characterMotor, 5f);
                    }
                }
                if (base.fixedAge >= minDuration && base.inputBank.skill1.down)
                {
                    this.outer.SetNextState(new Primary() { attackIndex = attackIndex == 0 ? 1 : 0 });
                    return;
                }
                if (base.fixedAge >= this.duration)
                {
                    this.outer.SetNextStateToMain();
                }
            }
        }
        public override void OnExit()
        {
            base.OnExit();
        }
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.PrioritySkill;
        }
    }
}
