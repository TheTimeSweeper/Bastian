using System;
using System.Reflection;
using System.Collections.Generic;
using BepInEx;
using R2API;
using R2API.Utils;
using EntityStates;
using RoR2;
using RoR2.Projectile;
using RoR2.Skills;
using UnityEngine;
using UnityEngine.Networking;
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
    class Secondary : BaseSkillState
    {
        private float duration;
        private float baseDuration = Configs.M2_Duration.Value;
        private float fireDelay;
        private float baseFireDelay = 0.00f;
        private float interruptPercentTime = 0.46f;

        private bool hasFired;
        private float damageCoefficient = Configs.M2_Damage.Value;

        public override void OnEnter()
        {
            base.OnEnter();
            base.StartAimMode();
            duration = baseDuration / base.attackSpeedStat;
            fireDelay = baseFireDelay / base.attackSpeedStat;
            interruptPercentTime *= duration;

            base.PlayAnimation("Gesture, Override", "Shoot", "M2", this.duration);
            if (base.isGrounded & !base.GetModelAnimator().GetBool("isMoving"))
            {
                base.PlayAnimation("FullBody, Override", "Shoot", "M2", this.duration);
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.fixedAge >= fireDelay && !hasFired)
            {
                hasFired = true;
                Fire();
            }
            if (base.fixedAge >= this.duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
            }
        }
        void Fire()
        {
            AkSoundEngine.PostEvent(Sounds.Play_Bastian_Fire, base.gameObject);
            EffectManager.SimpleMuzzleFlash(Prefabs.muzzleflash, base.gameObject, "fingerMuzzle", false);
            Ray aimRay = base.GetAimRay();
            if (base.isAuthority)
            {
                new BulletAttack
                {
                    owner = base.gameObject,
                    weapon = base.gameObject,
                    origin = aimRay.origin,
                    falloffModel = BulletAttack.FalloffModel.None,
                    aimVector = aimRay.direction,
                    procCoefficient = 0.6f,
                    minSpread = 0,
                    maxSpread = base.characterBody.spreadBloomAngle,
                    bulletCount = 1U,
                    smartCollision = true,
                    damage = base.characterBody.damage * damageCoefficient,
                    force = 150,
                    tracerEffectPrefab = Prefabs.tracer,
                    muzzleName = "fingerMuzzle",
                    hitEffectPrefab = Prefabs.bulletImpact,
                    isCrit = base.RollCrit(),
                    radius = 1f,
                    maxDistance = 100,
                    damageType = DamageType.Stun1s
                }.Fire();
            }
        }
        public override void OnExit()
        {
            base.OnExit();
        }
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            if (base.fixedAge > interruptPercentTime)
            {
                return InterruptPriority.Any;
            }
            return InterruptPriority.PrioritySkill;
        }
    }
}
