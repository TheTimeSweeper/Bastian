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

namespace Bastion
{
    class Secondary : BaseSkillState
    {
        private float duration;
        private float baseDuration = 0.3f;
        private float fireDelay;
        private float baseFireDelay = 0.08f;
        private bool hasFired;
        private float damageCoefficient = 1.4f;
        public override void OnEnter()
        {
            base.OnEnter();
            base.StartAimMode();
            duration = baseDuration / base.attackSpeedStat;
            fireDelay = baseFireDelay / base.attackSpeedStat;
            if (base.isGrounded & !base.GetModelAnimator().GetBool("isMoving"))
            {
                base.PlayAnimation("FullBody, Override", "Shoot", "M2", this.duration);
            }
            else
            {
                base.PlayAnimation("Gesture, Override", "Shoot", "M2", this.duration);
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            base.characterBody.isSprinting = false;
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
                    radius = 0.2f,
                    maxDistance = 1000
                }.Fire();
            }
        }
        public override void OnExit()
        {
            base.OnExit();
        }
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }
    }
}
