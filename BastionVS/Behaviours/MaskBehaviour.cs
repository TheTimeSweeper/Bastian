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
    class MaskBehaviour : MonoBehaviour
    {
        public float minDuration = 0.2f;
        public float maxDuration = 1;
        public float minCooldown = 0.6f;
        public float maxCooldown = 5;
        public float duration;
        public float cooldown;
        public ChildLocator childLocator;
        public float stopwatch;
        public ParticleSystem effect;
        public Material maskOff;
        public Material maskOn;
        public CharacterModel model;
        public Renderer mesh;
        private void Start()
        {
            if (model)
            {
                maskOn = model.baseRendererInfos[3].defaultMaterial;
            }
            else
            {
                mesh = base.GetComponentsInChildren<SkinnedMeshRenderer>()[3];
                maskOn = mesh.material;
            }

            childLocator = base.GetComponent<ChildLocator>();
            effect = childLocator.FindChild("maskEffect").GetComponent<ParticleSystem>();

            duration = RoR2Application.rng.RangeFloat(minDuration, maxDuration);
            cooldown = RoR2Application.rng.RangeFloat(minCooldown, maxCooldown);
        }
        private void FixedUpdate()
        {
            if (!effect)
                return;
            stopwatch += Time.fixedDeltaTime;
            if (stopwatch >= duration && effect.isPlaying)
            {
                effect.Stop();
                duration = RoR2Application.rng.RangeFloat(minDuration, maxDuration);
                if (model)
                {
                    model.baseRendererInfos[3].defaultMaterial = maskOn;
                }
                else
                {
                    if (mesh)
                    {
                        mesh.sharedMaterial = maskOn;
                    }
                }
            }
            if (stopwatch >= cooldown + duration)
            {
                stopwatch = 0;
                effect.Play();
                cooldown = RoR2Application.rng.RangeFloat(minCooldown, maxCooldown);
                if (model)
                {
                    model.baseRendererInfos[3].defaultMaterial = maskOff;
                }
                else
                {
                    if (mesh)
                    {
                        mesh.sharedMaterial = maskOff;
                    }
                }
            }
        }
    }
}