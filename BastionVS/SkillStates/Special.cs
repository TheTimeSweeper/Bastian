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
using Bastian.SkillDefs;

namespace Bastian
{
    class SpecialStart : BaseSkillState, IHasAuthoritySkillDefComponent<BlastDamageBuildupController>
    {
        private float duration = 1f;
        private CameraTargetParams.AimRequest aimRequest;
        private bool success;

        public BlastDamageBuildupController componentFromSkillDef { get; set; }

        private float charge;

        public override void OnEnter()
        {
            base.OnEnter();
            duration /= attackSpeedStat;
            base.PlayAnimation("FullBody, Override", "Charge", "Special", duration);
            AkSoundEngine.PostEvent(Sounds.Play_Bastian_FullBurst, base.gameObject);
            Transform transform = base.FindModelChild("center");
            UnityEngine.Object.Instantiate<GameObject>(Prefabs.chargeSphere, transform.position, Quaternion.identity, transform);
            
            if (componentFromSkillDef)
            {
                charge = componentFromSkillDef.charge;
                componentFromSkillDef.ResetChargeAuthority();
            }

            if (base.cameraTargetParams)
            {
                this.aimRequest = base.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Aura);
            }
            if (characterMotor)
            {
                characterMotor.Motor.ForceUnground();
                SmallHop(characterMotor, 16); 
            }
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();

            if (base.characterMotor)
            {
                if (isAuthority)
                {
                    ref float ySpeed = ref characterMotor.velocity.y;
                    ySpeed += 11 * Time.deltaTime;
                }
            }

            if (base.fixedAge >= duration)
            {
                success = true;
                if (isAuthority)
                {
                    this.outer.SetNextState(new Special { charge = charge });
                }
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

        public override void OnSerialize(NetworkWriter writer)
        {
            base.OnSerialize(writer);
            writer.Write(charge);
        }

        public override void OnDeserialize(NetworkReader reader)
        {
            base.OnDeserialize(reader);
            charge = reader.ReadSingle();
        }
    }

    class Special : BaseSkillState
    {
        public float charge;

        private float fireDelay = 0.16f;
        private float duration = 0.35f;
        private float blastDamageCoefficient => Configs.M4_Min_Damage.Value + charge;
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
            if(base.fixedAge < fireDelay && characterMotor)
            {
                base.characterMotor.velocity = Vector3.zero;
            }
            if (base.fixedAge >= fireDelay && !hasFired)
            {
                hasFired = true;
                if (isAuthority)
                {
                    BlastAttack blastAttack = new BlastAttack
                    {
                        attacker = base.gameObject,
                        baseDamage = this.damageStat * this.blastDamageCoefficient,
                        baseForce = 2000f,
                        crit = base.RollCrit(),
                        damageType = DamageType.Stun1s,
                        falloffModel = BlastAttack.FalloffModel.None,
                        radius = Configs.M4_Blast_Radius.Value,
                        position = characterBody.corePosition,
                        attackerFiltering = AttackerFiltering.NeverHitSelf,
                        teamIndex = base.teamComponent.teamIndex,
                    };
                    blastAttack.damageType.damageSource = DamageSource.Special;
                    blastAttack.Fire();
                }
                if (NetworkServer.active)
                {
                    DamageInfo damageInfo = new DamageInfo();
                    damageInfo.damage = base.healthComponent.health * Configs.M4_Health_Cost.Value;
                    damageInfo.position = base.characterBody.corePosition;
                    damageInfo.procCoefficient = 0;
                    damageInfo.procChainMask = default(ProcChainMask);
                    damageInfo.crit = false;
                    base.healthComponent.TakeDamage(damageInfo);

                    base.characterBody.AddTimedBuff(Prefabs.speed, 10);
                }
                EffectManager.SpawnEffect(Prefabs.explosionEffect, new EffectData() { origin = characterBody.corePosition, scale = Configs.M4_Blast_Radius.Value }, false);

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
