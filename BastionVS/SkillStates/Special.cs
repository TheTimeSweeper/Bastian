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
        public override void OnEnter()
        {
            base.OnEnter();
            base.PlayAnimation("FullBody, Override", "Charge", "Special", 1);
            AkSoundEngine.PostEvent(Sounds.Play_Bastian_FullBurst, base.gameObject);
            Transform transform = base.FindModelChild("center");
            UnityEngine.Object.Instantiate<GameObject>(Prefabs.chargeSphere, transform.position, Quaternion.identity, transform);
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.fixedAge >= duration && base.isAuthority)
            {
                this.outer.SetNextState(new Special());
            }
        }
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }
    }
    class Special : BaseSkillState
    {
        private float fireDelay = 0.07f;
        private float duration = 0.35f;
        private float blastDamageCoefficient = 15;
        private bool hasFired;

        public override void OnEnter()
        {
            base.OnEnter();
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
                else if (base.isAuthority)
                {
                    new BlastAttack
                    {
                        attacker = base.gameObject,
                        baseDamage = this.damageStat * this.blastDamageCoefficient,
                        baseForce = 12000f,
                        crit = base.RollCrit(),
                        damageType = DamageType.SlowOnHit,
                        falloffModel = BlastAttack.FalloffModel.None,
                        radius = 18f,
                        position = base.characterBody.footPosition,
                        attackerFiltering = AttackerFiltering.NeverHitSelf,
                        teamIndex = base.teamComponent.teamIndex
                    }.Fire();
                }
                EffectManager.SpawnEffect(Prefabs.explosionEffect, new EffectData() { origin = base.characterBody.corePosition, scale = 18}, false); 
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
