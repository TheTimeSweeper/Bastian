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
using HG;
using System.Runtime.InteropServices;
using UnityEngine.Events;
using UnityEngine.AddressableAssets;

namespace Bastion
{
    class Utils
    {
        public static GameObject NewDisplayModel(GameObject model, string name)
        {
            GameObject characterDisplay = PrefabAPI.InstantiateClone(model, name, false);
            characterDisplay.GetComponentInChildren<MaskBehaviour>().model = null;
            UnityEngine.Object.Destroy(characterDisplay.GetComponentInChildren<AimAnimator>());
            characterDisplay.GetComponentInChildren<CharacterModel>().enabled = false;
            foreach (SkinnedMeshRenderer r in characterDisplay.GetComponentsInChildren<SkinnedMeshRenderer>())
            {
                r.material.shaderKeywords = null;
            }
            characterDisplay.GetComponentInChildren<Animator>().runtimeAnimatorController = Assets.MainAssetBundle.LoadAsset<RuntimeAnimatorController>("displayAnimator");
            return characterDisplay;
        }
        internal static LoadoutAPI.SkinDefInfo CreateNewSkinDefInfo(SkinnedMeshRenderer[] renderers, string skinName, string objectToEnable, CharacterModel.RendererInfo[] rendererInfos, Sprite icon)
        {
            LoadoutAPI.SkinDefInfo skinDefInfo = default(LoadoutAPI.SkinDefInfo);
            skinDefInfo.BaseSkins = Array.Empty<SkinDef>();
            skinDefInfo.MinionSkinReplacements = new SkinDef.MinionSkinReplacement[0];
            skinDefInfo.ProjectileGhostReplacements = new SkinDef.ProjectileGhostReplacement[0];
            List<SkinDef.GameObjectActivation> objectActivations = new List<SkinDef.GameObjectActivation>();
            foreach (SkinnedMeshRenderer r in renderers)
            {
                var name = r.name;
                if (name == "body" || name == "skin")
                {
                    SkinDef.GameObjectActivation activation = new SkinDef.GameObjectActivation()
                    {
                        gameObject = r.gameObject,
                        shouldActivate = r.gameObject.name == objectToEnable
                    };
                    objectActivations.Add(activation);
                }
            }
            skinDefInfo.GameObjectActivations = objectActivations.ToArray();
            skinDefInfo.Icon = icon;
            skinDefInfo.MeshReplacements = new SkinDef.MeshReplacement[0];
            skinDefInfo.Name = skinName;
            skinDefInfo.NameToken = skinName;
            skinDefInfo.RendererInfos = rendererInfos;
            skinDefInfo.RootObject = renderers[1].transform.parent.gameObject;
            skinDefInfo.UnlockableDef = null;
            return skinDefInfo;
        }
        public static GameObject CreateHitbox(string name, Transform parent, Vector3 scale)
        {
            var hitboxTransform1 = new GameObject(name);
            hitboxTransform1.transform.SetParent(parent);
            hitboxTransform1.transform.localPosition = Vector3.zero;
            hitboxTransform1.transform.localRotation = Quaternion.identity;
            hitboxTransform1.transform.localScale = scale;
            var hitBoxGroup1 = parent.gameObject.AddComponent<HitBoxGroup>();
            HitBox hitBox = hitboxTransform1.AddComponent<HitBox>();
            hitboxTransform1.layer = LayerIndex.projectile.intVal;
            hitBoxGroup1.hitBoxes = new HitBox[] { hitBox };
            hitBoxGroup1.groupName = name;
            return hitboxTransform1;
        }
        internal static void RegisterEffect(GameObject effect, string soundName = "", bool parentToReferencedTransform = true, bool positionAtReferencedTransform = true, bool applyScale = false)
        {
            var effectcomponent = effect.GetComponent<EffectComponent>();
            if (!effectcomponent)
            {
                effectcomponent = effect.AddComponent<EffectComponent>();
            }
            if (!effect.GetComponent<NetworkIdentity>())
            {
                effect.AddComponent<NetworkIdentity>();
            }
            if (!effect.GetComponent<VFXAttributes>())
            {
                effect.AddComponent<VFXAttributes>().vfxPriority = VFXAttributes.VFXPriority.Always;
            }
            effectcomponent.applyScale = applyScale;
            effectcomponent.effectIndex = EffectIndex.Invalid;
            effectcomponent.parentToReferencedTransform = parentToReferencedTransform;
            effectcomponent.positionAtReferencedTransform = positionAtReferencedTransform;
            effectcomponent.soundName = soundName;
            ContentAddition.AddEffect(effect);
        }
        public static Material InstantiateMaterial(Texture tex)
        {
            Material mat = new Material(Prefabs.Load<Material>("RoR2/Base/Commando/matCommandoDualies.mat"));
            if (mat)
            {
                mat.SetColor("_Color", Color.white);
                mat.SetTexture("_MainTex", tex);
                mat.SetColor("_EmColor", Color.black);
                mat.SetFloat("_EmPower", 0);
                mat.SetTexture("_EmTex", null);
                mat.SetFloat("_NormalStrength", 1f);
                mat.SetTexture("_NormalTex", null);
                return mat;
            }
            return mat;
        }
        public static Material InstantiateMaterial(Texture tex, Texture em)
        {
            Material mat = new Material(Prefabs.Load<Material>("RoR2/Base/Commando/matCommandoDualies.mat"));
            if (mat)
            {
                mat.SetColor("_Color", Color.white);
                mat.SetTexture("_MainTex", tex);
                mat.SetColor("_EmColor", Color.white);
                mat.SetFloat("_EmPower", 4);
                mat.SetTexture("_EmTex", em);
                mat.SetFloat("_NormalStrength", 1f);
                mat.SetTexture("_NormalTex", null);
                return mat;
            }
            return mat;
        }
    }
}
