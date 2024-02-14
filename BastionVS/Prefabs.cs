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
    class Prefabs
    {
        internal static GameObject swingEffect;
        internal static GameObject swingImpact;
        internal static GameObject bulletImpact;
        internal static GameObject muzzleflash;
        internal static GameObject tracer;
        internal static GameObject dashEffect;
        internal static GameObject explosionEffect;
        internal static GameObject chargeSphere;
        internal static BuffDef speed { get; set; }

        internal static void CreatePrefabs()
        {
			speed = ScriptableObject.CreateInstance<BuffDef>();
			speed.buffColor = MainPlugin.characterColor;
			speed.canStack = false;
			speed.iconSprite = Load<Sprite>("RoR2/Base/Common/texMovespeedBuffIcon.tif");
			ContentAddition.AddBuffDef(speed);

			muzzleflash = PrefabAPI.InstantiateClone(Load<GameObject>("RoR2/Base/Common/VFX/Muzzleflash1.prefab"), "Muzzleflash", false);
			foreach (ParticleSystemRenderer particleSystemRenderer in muzzleflash.GetComponentsInChildren<ParticleSystemRenderer>(true))
			{
				particleSystemRenderer.gameObject.SetActive(true);
				string name = particleSystemRenderer.name;
				bool flag = name == "Starburst";
				if (flag)
				{
					particleSystemRenderer.material = new Material(Load<Material>("RoR2/DLC1/Railgunner/matRailgunImpactSpikes.mat"));
					particleSystemRenderer.material.DisableKeyword("VERTEXCOLOR");
					particleSystemRenderer.material.SetFloat("_ZTest", 2f);
				}
				bool flag2 = name == "ForwardFlames";
				if (flag2)
				{
					particleSystemRenderer.material = Load<Material>("RoR2/DLC1/Railgunner/matRailgunImpact.mat");
				}
			}
			muzzleflash.GetComponentInChildren<Light>().color = new Color(1f, 0f, 1f);
			ContentAddition.AddEffect(muzzleflash);

			tracer = PrefabAPI.InstantiateClone(Load<GameObject>("RoR2/Base/Drones/TracerCommandoBoost.prefab"), "Tracer", false);
			tracer.GetComponent<Tracer>().speed = 500f;
			LineRenderer component = tracer.GetComponent<LineRenderer>();
			component.startColor = new Color(1f, 0.21176f, 0.72549f);
			component.endColor = new Color(1f, 0f, 1f);
			Utils.RegisterEffect(tracer, "", true, true, false);

			bulletImpact = PrefabAPI.InstantiateClone(Load<GameObject>("RoR2/Base/Captain/HitsparkCaptainShotgun.prefab"), "BulletImpact", false);
			Material material = new Material(Load<Material>("RoR2/Base/Common/VFX/matOmniHitspark1.mat"));
			material.SetTexture("_RemapTex", Load<Texture2D>("RoR2/Base/Common/ColorRamps/texHippoRezRamp.png"));
			material.DisableKeyword("VERTEXCOLOR");
			material.SetColor("_TintColor", new Color(1f, 0f, 1f));
			Material material2 = new Material(Load<Material>("RoR2/DLC1/Railgunner/matRailgunRings.mat"));
			material2.DisableKeyword("VERTEXCOLOR");
			material2.SetColor("_TintColor", new Color(1f, 0f, 1f));
			foreach (ParticleSystemRenderer particleSystemRenderer2 in bulletImpact.GetComponentsInChildren<ParticleSystemRenderer>(true))
			{
				particleSystemRenderer2.gameObject.SetActive(true);
				string name2 = particleSystemRenderer2.name;
				bool flag3 = name2 == "TriangleSparks" || name2 == "TriangleSparksLarge";
				if (flag3)
				{
					particleSystemRenderer2.material = material;
				}
				bool flag4 = name2 == "ForwardFlames";
				if (flag4)
				{
					particleSystemRenderer2.material = Load<Material>("RoR2/DLC1/Railgunner/matRailgunnerBackpackFlash.mat");
				}
				bool flag5 = name2 == "HitRing";
				if (flag5)
				{
					particleSystemRenderer2.material = material2;
				}
			}
			Utils.RegisterEffect(bulletImpact, "Play_Bastian_Bullet_Impact", true, true, false);

			swingImpact = PrefabAPI.InstantiateClone(Load<GameObject>("RoR2/Base/Loader/OmniImpactVFXLoaderLightning.prefab"), "SwingImpact", false);
			UnityEngine.Object.Destroy(swingImpact.GetComponent<OmniEffect>());
			ParticleSystemRenderer[] swingParticles = swingImpact.GetComponentsInChildren<ParticleSystemRenderer>(true);
			foreach (ParticleSystemRenderer r in swingParticles)
			{
				r.gameObject.SetActive(true);
				Material material3 = new Material(r.material);
				r.material = material3;
				material3.DisableKeyword("VERTEXCOLOR");
				material3.SetColor("_TintColor", new Color(1f, 0.00339f, 1f));
				r.transform.localScale = Vector3.one * 2;
			}
			Texture2D value = Load<Texture2D>("RoR2/Base/Common/ColorRamps/texRampBrotherPillar.png");
			swingParticles[0].material.SetTexture("_RemapTex", value);
			swingParticles[1].material.SetTexture("_RemapTex", value);
			swingParticles[4].material = material2;
			swingParticles[5].material.SetTexture("_RemapTex", Load<Texture2D>("RoR2/Base/Common/ColorRamps/texRampDefault.png"));
			Utils.RegisterEffect(swingImpact, "Play_Bastian_Punch_Impact", true, true, false);

			swingEffect = PrefabAPI.InstantiateClone(Load<GameObject>("RoR2/Base/Croco/CrocoSlash.prefab"), "SwingEffect", false);
			Texture2D value2 = Load<Texture2D>("RoR2/Base/Common/ColorRamps/texRampAncientWisp.png");
			ParticleSystemRenderer[] componentsInChildren4 = swingEffect.GetComponentsInChildren<ParticleSystemRenderer>();
			componentsInChildren4[0].material = new Material(Load<Material>("RoR2/Base/Brother/matBrotherSwing.mat"));
			componentsInChildren4[0].material.SetTexture("_RemapTex", value2);
			componentsInChildren4[0].material.SetTexture("_Cloud1Tex", Load<Texture2D>("RoR2/Base/Common/texMagmaCloud.png"));
			componentsInChildren4[0].material.SetFloat("_Boost", 5f);
			componentsInChildren4[0].material.SetFloat("_DepthOffset", -4.666f);
			componentsInChildren4[0].material.SetFloat("_InvFade", 0.128f);
			componentsInChildren4[0].material.SetFloat("_AlphaBoost", 1.7f);
			componentsInChildren4[0].transform.localScale = Vector3.one * 2;
			componentsInChildren4[1].transform.localScale = Vector3.one * 2;
			componentsInChildren4[1].transform.localRotation = Quaternion.Euler(0f, 0f, 140f);
			swingEffect.AddComponent<DestroyOnTimer>().duration = 1f;
			swingEffect.GetComponent<ScaleParticleSystemDuration>().newDuration = 0.15f;

			dashEffect = PrefabAPI.InstantiateClone(Load<GameObject>("RoR2/Base/Brother/BrotherDashEffect.prefab"), "DashEffect", false);
			foreach (ParticleSystemRenderer particleSystemRenderer4 in dashEffect.GetComponentsInChildren<ParticleSystemRenderer>(true))
			{
				particleSystemRenderer4.transform.localScale = Vector3.one * 0.3f;
				string name3 = particleSystemRenderer4.name;
				bool flag6 = particleSystemRenderer4.name == "Dash";
				if (flag6)
				{
					particleSystemRenderer4.material = componentsInChildren4[0].material;
				}
				bool flag7 = particleSystemRenderer4.name == "Donut";
				if (flag7)
				{
					Material material4 = particleSystemRenderer4.material;
					particleSystemRenderer4.material = new Material(material4);
					particleSystemRenderer4.material.SetColor("_TintColor", new Color(1f, 0f, 1f));
					particleSystemRenderer4.material.SetFloat("_NormalStrength", 5f);
					particleSystemRenderer4.material.SetFloat("_AlphaBoost", 4f);
					particleSystemRenderer4.material.SetFloat("_SpecularStrength", 0.05394607f);
					particleSystemRenderer4.material.SetFloat("_SpecularExponent", 0.1f);
					particleSystemRenderer4.material.SetTextureScale("_Cloud1Tex", Vector2.one * 6f);
				}
			}
			dashEffect.GetComponentInChildren<Light>().color = new Color(1f, 0f, 1f);
			UnityEngine.Object.Destroy(dashEffect.GetComponent<EffectComponent>());
			UnityEngine.Object.Destroy(dashEffect.GetComponent<VFXAttributes>());

			Material material5 = new Material(Load<Material>("RoR2/DLC1/Railgunner/matRailgunnerMineAreaIndicator.mat"));
			material5.SetFloat("_InvFade", 4.8f);
			material5.SetFloat("_Boost", 0.2f);
			material5.SetFloat("_RimPower", 4.4f);
			material5.SetFloat("_AlphaBoost", 2.64f);
			material5.SetFloat("_IntersectionStrength", 20f);

			chargeSphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			UnityEngine.Object.Destroy(chargeSphere.GetComponent<SphereCollider>());
			chargeSphere.GetComponent<Renderer>().material = material5;
			chargeSphere.transform.localScale = Vector3.one * 18f;
			ObjectScaleCurve objectScaleCurve = chargeSphere.AddComponent<ObjectScaleCurve>();
			objectScaleCurve.useOverallCurveOnly = true;
			objectScaleCurve.overallCurve = AnimationCurve.Linear(0f, 1f, 1f, 0f);
			objectScaleCurve.timeMax = 1f;
			chargeSphere.transform.parent = dashEffect.transform.parent;
			chargeSphere.AddComponent<DestroyOnTimer>().duration = 1f;

			explosionEffect = PrefabAPI.InstantiateClone(Load<GameObject>("RoR2/Base/Loader/LoaderGroundSlam.prefab"), "ExplosionEffect", false);
			explosionEffect.GetComponentInChildren<Light>().color = new Color(1f, 0f, 1f);
			foreach (ParticleSystemRenderer r in explosionEffect.GetComponentsInChildren<ParticleSystemRenderer>(true))
			{
				r.transform.localScale = Vector3.one * 0.3f;
				string name = r.name;
				if (name == "Flash Center")
				{
					r.material = material2;
				}
				bool flag9 = name == "Sparks, Single";
				if (flag9)
				{
					Material material6 = new Material(Load<Material>("RoR2/DLC1/Railgunner/matRailgunnerLED.mat"));
					r.material.SetColor("_TintColor", new Color(1f, 0f, 1f));
				}
				bool flag10 = name == "Ring";
				if (flag10)
				{
					r.material = new Material(Load<Material>("RoR2/DLC1/Railgunner/matRailgunCryoSphere.mat"));
					r.material.SetTexture("_RemapTex", value2);
				}
				bool flag11 = name == "Sphere, Expanding";
				if (flag11)
				{
					r.material = new Material(material5);
					material5.SetTexture("_RemapTex", Load<Texture2D>("RoR2/DLC1/Common/ColorRamps/texRampVoidRaidCrabEye.png"));
				}
				bool flag12 = name == "Omni, Directional";
				if (flag12)
				{
					Material material7 = new Material(r.material);
					r.material = material7;
					material7.DisableKeyword("VERTEXCOLOR");
					material7.SetColor("_TintColor", new Color(1f, 0.00339f, 1f));
					material7.SetTexture("_RemapTex", value);

					r.transform.localScale -= Vector3.right * 0.25f;
					r.transform.localPosition = new Vector3(0, 0.06f, 0);
				}
			}
			Utils.RegisterEffect(explosionEffect, "", false, false, true);
		}
        internal static T Load<T>(string path)
        {
            return Addressables.LoadAssetAsync<T>(path).WaitForCompletion();
        }
    }
}
