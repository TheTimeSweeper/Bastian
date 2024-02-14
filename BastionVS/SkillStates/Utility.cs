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
    class UtilityStart : BaseState
    {
        private float duration = 0.2f;
        public override void OnEnter()
        {
            base.OnEnter();
            base.characterMotor.velocity = Vector3.zero;
            base.PlayAnimation("FullBody, Override", "Dash", "Utility", 0.7f);
            AkSoundEngine.PostEvent(Sounds.Play_Bastian_DashFast, base.gameObject);
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.fixedAge >= duration && base.isAuthority)
            {
                this.outer.SetNextState(new Utility());
            }
        }
    }
    class Utility : BaseState
    {
        private CameraTargetParams.AimRequest request;
        private Vector3 dashVector;
        private List<HealthComponent> hitList = new List<HealthComponent>();

        public override void OnEnter()
        {
            base.OnEnter();
            if (base.cameraTargetParams)
            {
                request = base.cameraTargetParams.RequestAimType(CameraTargetParams.AimType.Aura);
            }
            if (NetworkServer.active)
            {
                base.characterBody.AddBuff(RoR2Content.Buffs.HiddenInvincibility);
            }
            //base.gameObject.layer = LayerIndex.fakeActor.intVal;
            this.dashVector = base.inputBank.aimDirection;
            base.characterDirection.forward = this.dashVector;

            Transform transform = base.FindModelChild("center");
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Prefabs.dashEffect, transform.position + dashVector * 4f, Util.QuaternionSafeLookRotation(-dashVector)); 

        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            Stun();
            if (base.characterMotor && base.characterDirection)
            {
                base.characterMotor.rootMotion += this.dashVector * (this.moveSpeedStat * EntityStates.Merc.EvisDash.speedCoefficient * Time.fixedDeltaTime);
            }
            if (base.fixedAge >= 0.2f && base.isAuthority)
            {
                this.outer.SetNextStateToMain();
            }
        }
        void Stun()
        {
            if (NetworkServer.active)
            {
                Collider[] array = Physics.OverlapSphere(base.transform.position, 5, LayerIndex.entityPrecise.mask);
                foreach (Collider c in array)
                {
                    HurtBox component = c.GetComponent<HurtBox>();
                    if (component && component.healthComponent != base.healthComponent && component.healthComponent.alive && component.healthComponent.body && component.healthComponent.body.teamComponent &&
                        TeamMask.GetEnemyTeams(base.teamComponent.teamIndex).HasTeam(component.healthComponent.body.teamComponent.teamIndex) && !hitList.Contains(component.healthComponent))
                    {
                        hitList.Add(component.healthComponent);
                        SetStateOnHurt.SetStunOnObject(component.healthComponent.gameObject, 1);
                        AkSoundEngine.PostEvent(Sounds.Play_Bastian_Stun, component.healthComponent.gameObject);
                    }
                }
            }
        }
        public override void OnExit()
        {
            if (base.cameraTargetParams)
            {
                request.Dispose();
            }
            //base.gameObject.layer = LayerIndex.defaultLayer.intVal;
            if (NetworkServer.active)
            {
                base.characterBody.RemoveBuff(RoR2Content.Buffs.HiddenInvincibility);
            }
            if (base.isAuthority)
            {
                base.characterMotor.velocity *= 0.1f;
                base.SmallHop(base.characterMotor, EntityStates.Merc.Assaulter.smallHopVelocity);
            }
            base.OnExit();
        }
        public override InterruptPriority GetMinimumInterruptPriority()
        {
            return InterruptPriority.Skill;
        }
    }
}
