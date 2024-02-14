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
    class SpecialStart : BaseSkillState
    {
        private float duration = 1f;
        private CameraTargetParams.AimRequest aimRequest;
        private bool success;

        public override void OnEnter()
        {
            base.OnEnter();
            duration /= attackSpeedStat;
            base.PlayAnimation("FullBody, Override", "Charge", "Special", duration);
            AkSoundEngine.PostEvent(Sounds.Play_Bastian_FullBurst, base.gameObject);
            Transform transform = base.FindModelChild("center");
            UnityEngine.Object.Instantiate<GameObject>(Prefabs.chargeSphere, transform.position, Quaternion.identity, transform);

            if (base.cameraTargetParams)
            {
                this.aimRequest = base.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Aura);
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.characterMotor)
            {
                base.characterMotor.velocity = Vector3.zero;
            }

            if (base.fixedAge >= duration && base.isAuthority)
            {
                success = true;
                this.outer.SetNextState(new Special());
            }
        }
        public override void OnExit()
        {
            base.OnExit();

            if (this.aimRequest != null)
            {
                this.aimRequest.Dispose();
            }
            if (!success)
            {
                PlayAnimation("FullBody, Override", "BufferEmpty");
            }
        }
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }
    }

    class Special : BaseSkillState
    {
        private float fireDelay = 0.16f;
        private float duration = 0.35f;
        private float blastDamageCoefficient = 15;
        private bool hasFired;
        
        public override void OnEnter()
        {
            base.OnEnter();
            duration /= attackSpeedStat;
            fireDelay = duration * 0.16f;
            PlayCrossfade("FullBody, Override", "Release", "Special", duration, duration * 0.1f);
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.fixedAge >= fireDelay && !hasFired)
            {
                hasFired = true;
                if (NetworkServer.active)
                {
                    DamageInfo damageInfo = new DamageInfo();
                    damageInfo.damage = base.healthComponent.health / 3;
                    damageInfo.position = base.characterBody.corePosition;
                    damageInfo.procCoefficient = 0;
                    damageInfo.procChainMask = default(ProcChainMask);
                    damageInfo.crit = false;
                    base.healthComponent.TakeDamage(damageInfo);

                    base.characterBody.AddTimedBuff(Prefabs.speed, 10);
                }

                if (base.isAuthority)
                {
                    new BlastAttack
                    {
                        attacker = base.gameObject,
                        baseDamage = this.damageStat * this.blastDamageCoefficient,
                        baseForce = 2000f,
                        crit = base.RollCrit(),
                        damageType = DamageType.Stun1s,
                        falloffModel = BlastAttack.FalloffModel.None,
                        radius = 30f,
                        position = base.characterBody.footPosition,
                        attackerFiltering = AttackerFiltering.NeverHitSelf,
                        teamIndex = base.teamComponent.teamIndex,
                    }.Fire();
                }
                EffectManager.SpawnEffect(Prefabs.explosionEffect, new EffectData() { origin = base.characterBody.corePosition, scale = 18}, false);

                SmallHop(characterMotor, characterBody.jumpPower);
            }
            if (base.fixedAge >= duration && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
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
