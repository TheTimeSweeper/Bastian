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
        private float duration = 0.29f;

        public Vector3 dashVector;

        public override void OnEnter()
        {
            base.OnEnter();
            duration /= attackSpeedStat;
            base.characterMotor.velocity = Vector3.zero;
            base.PlayAnimation("FullBody, Override", "Dash", "Utility", duration/0.29f);
            AkSoundEngine.PostEvent(Sounds.Play_Bastian_DashFast, base.gameObject);

            characterBody.SetAimTimer(0);
        }
        public override void FixedUpdate()
        {
            base.FixedUpdate();
            if (base.fixedAge >= duration && base.isAuthority)
            {
                this.outer.SetNextState(new Utility ());
            }
        }
    }
    class Utility : BaseState
    {
        public Vector3 dashVector;
        private CameraTargetParams.AimRequest request;
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

            GetDashVector();
            if (dashVector.sqrMagnitude < Mathf.Epsilon)
            {
                this.dashVector = inputBank.aimDirection;
            }
            //base.gameObject.layer = LayerIndex.fakeActor.intVal;
            base.characterDirection.forward = this.dashVector;

            Transform centerTransform = base.FindModelChild("center");
            GameObject gameObject = UnityEngine.Object.Instantiate<GameObject>(Prefabs.dashEffect, centerTransform.position + dashVector * 4f, Util.QuaternionSafeLookRotation(-dashVector));

        }

        private void GetDashVector()
        {
            Vector3 aimDirection = inputBank.aimDirection;
            aimDirection.y = 0;
            Vector3 rightDirection = -Vector3.Cross(Vector3.up, aimDirection);
            float angle = Vector3.Angle(inputBank.aimDirection, aimDirection);
            if (inputBank.aimDirection.y < 0) angle = -angle;
            this.dashVector = Quaternion.AngleAxis(angle, rightDirection) * inputBank.moveVector;
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


                        EffectManager.SpawnEffect(Prefabs.swingImpact, new EffectData
                        {
                            origin = component.transform.position,
                            rotation = Util.QuaternionSafeLookRotation(transform.forward),
                            //networkSoundEventIndex = this.impactSound
                        }, true);
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
