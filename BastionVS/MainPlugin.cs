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

[module: UnverifiableCode]
[assembly: SecurityPermission(SecurityAction.RequestMinimum, SkipVerification = true)]
namespace Bastion
{
    [BepInDependency("com.weliveinasociety.CustomEmotesAPI", BepInDependency.DependencyFlags.SoftDependency)]
    [BepInDependency(R2API.ContentManagement.R2APIContentManager.PluginGUID)]
    [BepInDependency(R2API.LanguageAPI.PluginGUID)]
    [BepInDependency(R2API.LoadoutAPI.PluginGUID)]
    //[BepInDependency(R2API.Networking.NetworkingAPI.PluginGUID)]
    [BepInDependency(R2API.PrefabAPI.PluginGUID)]
    [BepInDependency(R2API.SoundAPI.PluginGUID)]
    [BepInDependency(R2API.RecalculateStatsAPI.PluginGUID)]
    [BepInPlugin(MODUID, MODNAME, VERSION)]
    [NetworkCompatibility(CompatibilityLevel.EveryoneMustHaveMod, VersionStrictness.EveryoneNeedSameModVersion)]
    public class MainPlugin : BaseUnityPlugin
    {
        public const string MODUID = "com.DragonyckKKing.Bastian";
        public const string MODNAME = "Bastian";
        public const string VERSION = "1.0.0";
        public const string SURVIVORNAME = "Bastian: Model 4.1.N.K";
        public const string SURVIVORNAMEKEY = "BASTIAN";

        private const string configSection = "hello";

        public static GameObject characterPrefab;
        public static readonly Color characterColor = new Color(0.9176471f, 0.6862745f, 0.9137255f);

        public static MainPlugin instance;

        private void Awake()
        {
            instance = this;
            Assets.PopulateAssets();
            Prefabs.CreatePrefabs();
            CreatePrefab();
            RegisterStates();
            RegisterCharacter();
            Hook.Hooks();

            if (ModCompat.EmoteAPIEnabled)
            {
                ModCompat.AddEmoteSupport();
            }
        }
        internal static void CreatePrefab()
        {
            var commandoBody = Addressables.LoadAssetAsync<GameObject>("RoR2/Base/Commando/CommandoBody.prefab").WaitForCompletion();

            characterPrefab = PrefabAPI.InstantiateClone(commandoBody, SURVIVORNAME + "Body", true);
            characterPrefab.GetComponent<NetworkIdentity>().localPlayerAuthority = true;
            Destroy(characterPrefab.transform.Find("ModelBase").gameObject);
            Destroy(characterPrefab.transform.Find("CameraPivot").gameObject);
            Destroy(characterPrefab.transform.Find("AimOrigin").gameObject);

            GameObject model = Assets.MainAssetBundle.LoadAsset<GameObject>("Bastian");
            model.AddComponent<AnimationEvents>().soundCenter = model;

            GameObject ModelBase = new GameObject("ModelBase");
            ModelBase.transform.parent = characterPrefab.transform;
            ModelBase.transform.localPosition = new Vector3(0f, -0.94f, 0f);
            ModelBase.transform.localRotation = Quaternion.identity;
            ModelBase.transform.localScale = new Vector3(1f, 1f, 1f);

            GameObject gameObject3 = new GameObject("AimOrigin");
            gameObject3.transform.parent = ModelBase.transform;
            gameObject3.transform.localPosition = new Vector3(0f, 1.3f, 0f);
            gameObject3.transform.localRotation = Quaternion.identity;
            gameObject3.transform.localScale = Vector3.one;

            Transform transform = model.transform;
            transform.parent = ModelBase.transform;
            transform.localPosition = Vector3.zero;
            transform.localScale = Vector3.one * 0.55f;
            transform.localRotation = Quaternion.identity;

            CharacterDirection characterDirection = characterPrefab.GetComponent<CharacterDirection>();
            characterDirection.targetTransform = ModelBase.transform;
            characterDirection.modelAnimator = model.GetComponentInChildren<Animator>();

            CharacterBody bodyComponent = characterPrefab.GetComponent<CharacterBody>();
            bodyComponent.name = "BastianBody";
            bodyComponent.baseNameToken = SURVIVORNAMEKEY + "_NAME";
            bodyComponent.subtitleNameToken = SURVIVORNAMEKEY + "_SUBTITLE";
            bodyComponent.bodyFlags = CharacterBody.BodyFlags.ImmuneToExecutes;
            bodyComponent.rootMotionInMainState = false;
            bodyComponent.mainRootSpeed = 0;
            bodyComponent.baseMaxHealth = 110;
            bodyComponent.levelMaxHealth = 33;
            bodyComponent.baseRegen = 1.5f;
            bodyComponent.levelRegen = 0.2f;
            bodyComponent.baseMaxShield = 0;
            bodyComponent.levelMaxShield = 0;
            bodyComponent.baseMoveSpeed = 7;
            bodyComponent.levelMoveSpeed = 0;
            bodyComponent.baseAcceleration = 110;
            bodyComponent.baseJumpPower = 15;
            bodyComponent.levelJumpPower = 0;
            bodyComponent.baseDamage = 12;
            bodyComponent.levelDamage = 2.4f;
            bodyComponent.baseAttackSpeed = 1;
            bodyComponent.levelAttackSpeed = 0;
            bodyComponent.baseCrit = 1;
            bodyComponent.levelCrit = 0;
            bodyComponent.baseArmor = 0;
            bodyComponent.levelArmor = 0;
            bodyComponent.baseJumpCount = 1;
            bodyComponent.sprintingSpeedMultiplier = 1.45f;
            bodyComponent.wasLucky = false;
            bodyComponent.hideCrosshair = false;
            bodyComponent.aimOriginTransform = gameObject3.transform;
            bodyComponent.hullClassification = HullClassification.Human;
            bodyComponent.portraitIcon = Assets.MainAssetBundle.LoadAsset<Sprite>("portrait").texture;
            bodyComponent.isChampion = false;
            bodyComponent.currentVehicle = null;
            bodyComponent.skinIndex = 0U;
            bodyComponent.bodyColor = characterColor;

            Modules.Config.ConfigureBody(bodyComponent, configSection);

            HealthComponent healthComponent = characterPrefab.GetComponent<HealthComponent>();
            healthComponent.health = bodyComponent.maxHealth;
            healthComponent.shield = 0f;
            healthComponent.barrier = 0f;
            healthComponent.magnetiCharge = 0f;
            healthComponent.body = null;
            healthComponent.dontShowHealthbar = false;
            healthComponent.globalDeathEventChanceCoefficient = 1f;

            ModelLocator modelLocator = characterPrefab.GetComponent<ModelLocator>();
            modelLocator.modelTransform = transform;
            modelLocator.modelBaseTransform = ModelBase.transform;
            modelLocator.dontReleaseModelOnDeath = false;
            modelLocator.autoUpdateModelTransform = true;
            modelLocator.dontDetatchFromParent = false;
            modelLocator.noCorpse = false;
            modelLocator.normalizeToFloor = false;
            modelLocator.preserveModel = false;

            ChildLocator childLocator = model.GetComponent<ChildLocator>();

            CharacterModel characterModel = model.AddComponent<CharacterModel>();


            SkinnedMeshRenderer[] renderers = model.GetComponentsInChildren<SkinnedMeshRenderer>();
            List<CharacterModel.RendererInfo> rendererInfoList = new List<CharacterModel.RendererInfo>();
            for (int i = 0; i < renderers.Length; i++)
            {
                var name = renderers[i].name;
                var mat = Utils.InstantiateMaterial(renderers[i].material.mainTexture, renderers[i].material.GetTexture("_EmissionMap"));
                renderers[i].material = mat;
                rendererInfoList.Add(new CharacterModel.RendererInfo()
                {
                    renderer = renderers[i],
                    defaultMaterial = mat,
                    defaultShadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On,
                    ignoreOverlays = name != "body" || name != "skin"
                });
            }
            CharacterModel.RendererInfo[] rendererInfos = rendererInfoList.ToArray();
            characterModel.body = bodyComponent;
            characterModel.baseRendererInfos = rendererInfos;
            characterModel.autoPopulateLightInfos = true;
            characterModel.invisibilityCount = 0;
            characterModel.temporaryOverlays = new List<TemporaryOverlay>();
            characterModel.mainSkinnedMeshRenderer = model.GetComponentInChildren<SkinnedMeshRenderer>();

            ModelSkinController modelSkinController = model.AddComponent<ModelSkinController>();
            LanguageAPI.Add(SURVIVORNAMEKEY + "BODY_DEFAULT_SKIN_NAME", "Default");
            LanguageAPI.Add(SURVIVORNAMEKEY + "BODY_MASTERY_SKIN_NAME", "Mastery");

            var meshes = model.GetComponentsInChildren<SkinnedMeshRenderer>(); 
            modelSkinController.skins = new SkinDef[]
            {
                LoadoutAPI.CreateNewSkinDef(Utils.CreateNewSkinDefInfo(meshes, SURVIVORNAMEKEY + "BODY_DEFAULT_SKIN_NAME", "body", rendererInfos, LoadoutAPI.CreateSkinIcon(new Color(0.86275f, 0.50980f, 0.80784f), new Color(0.47059f, 0.46275f, 0.48235f), new Color(0.20392f, 0.18824f, 0.21961f), new Color(0.76471f, 0.70196f, 0.72549f)))),
                LoadoutAPI.CreateNewSkinDef(Utils.CreateNewSkinDefInfo(meshes, SURVIVORNAMEKEY + "BODY_MASTERY_SKIN_NAME", "skin", rendererInfos, LoadoutAPI.CreateSkinIcon(new Color(0.81176f, 0.22745f, 0.25882f), new Color(0.93333f, 0.87843f, 0.79608f), new Color(0.22745f, 0.2078f, 0.17647f), new Color(0.67451f, 0.51373f, 0.27451f))))
            };

            childLocator.FindChild("skin").gameObject.SetActive(false);

            var maskB = model.AddComponent<MaskBehaviour>();
            maskB.maskOff = Utils.InstantiateMaterial(Assets.MainAssetBundle.LoadAsset<Material>("maskOff").mainTexture);
            maskB.model = characterModel;

            Collider[] colliders = model.GetComponentsInChildren<Collider>();
            HurtBoxGroup hurtBoxGroup = model.AddComponent<HurtBoxGroup>();
            List<HurtBox> hurtboxes = new List<HurtBox>();
            foreach (Collider c in colliders)
            {
                HurtBox hurtbox = c.gameObject.AddComponent<HurtBox>();
                hurtbox.gameObject.layer = LayerIndex.entityPrecise.intVal;
                hurtbox.healthComponent = healthComponent;
                hurtbox.isBullseye = true;
                hurtbox.damageModifier = HurtBox.DamageModifier.Normal;
                hurtbox.hurtBoxGroup = hurtBoxGroup;
                hurtbox.indexInGroup = 0;

                hurtBoxGroup.mainHurtBox = hurtbox;
                hurtBoxGroup.bullseyeCount = 1;

                hurtboxes.Add(hurtbox);
            }
            hurtBoxGroup.hurtBoxes = hurtboxes.ToArray();

            Utils.CreateHitbox("Swing", model.transform, new Vector3(13, 11, 12)).transform.localPosition = new Vector3(0, 2.64f, 4f);

            var ragdollMaterial = Addressables.LoadAssetAsync<PhysicMaterial>("RoR2/Base/Common/physmatRagdoll.physicMaterial").WaitForCompletion();
            List<Transform> transforms = new List<Transform>();
            List<string> boneNames = new List<string>()
            {
                "BASE",
                "pelvis",
                "stomach",
                "spine",
                "chest",
                "thigh.L",
                "shin.L",
                "foot.L",
                "thigh.R",
                "shin.R",
                "foot.R",
                "neck",
                "head",
                "shoulder.L",
                "upper_arm.L",
                "forearm.L",
                "hand.L",
                "shoulder.R",
                "upper_arm.R",
                "forearm.R",
                "hand.R"
            };
            foreach (Transform t in model.GetComponentsInChildren<Transform>(true))
            {
                string name = t.name;
                if (boneNames.Contains(name))
                {
                    transforms.Add(t);
                    GameObject g = t.gameObject;
                    g.layer = LayerIndex.ragdoll.intVal;
                    if (!g.GetComponent<Rigidbody>())
                    {
                        g.AddComponent<Rigidbody>();
                    }
                    var bonecollider = g.AddComponent<CapsuleCollider>();
                    bonecollider.radius = 0.4f;
                    bonecollider.height = 3.5f;
                    bonecollider.material = ragdollMaterial;
                    bonecollider.sharedMaterial = ragdollMaterial;
                    Rigidbody parentRigidBody = t.parent.GetComponent<Rigidbody>();
                    if (parentRigidBody)
                    {
                        var joint = g.AddComponent<CharacterJoint>();
                        joint.autoConfigureConnectedAnchor = true;
                        joint.enablePreprocessing = true;
                        joint.connectedBody = parentRigidBody;
                    }
                }
            }
            var ragdoll = model.AddComponent<RagdollController>();
            ragdoll.bones = transforms.ToArray();

            AimAnimator aimAnimator = model.AddComponent<AimAnimator>();
            aimAnimator.inputBank = characterPrefab.GetComponent<InputBankTest>();
            aimAnimator.directionComponent = characterPrefab.GetComponent<CharacterDirection>();
            aimAnimator.pitchRangeMin = -60;
            aimAnimator.pitchRangeMax = 60;
            aimAnimator.yawRangeMin = -90;
            aimAnimator.yawRangeMax = 90;
            aimAnimator.pitchGiveupRange = 30f;
            aimAnimator.yawGiveupRange = 10f;
            aimAnimator.giveupDuration = 3;

            FootstepHandler footstepHandler = model.AddComponent<FootstepHandler>();
            footstepHandler.baseFootstepString = "Play_player_footstep";
            footstepHandler.sprintFootstepOverrideString = "";
            footstepHandler.enableFootstepDust = true;
            footstepHandler.footstepDustPrefab = Prefabs.Load<GameObject>("RoR2/Base/Common/VFX/GenericFootstepDust.prefab");

            EntityStateMachine mainStateMachine = bodyComponent.GetComponent<EntityStateMachine>();
            mainStateMachine.mainStateType = new SerializableEntityStateType(typeof(CharacterMain));
            
            CharacterDeathBehavior characterDeathBehavior = characterPrefab.GetComponent<CharacterDeathBehavior>();
            characterDeathBehavior.deathStateMachine = characterPrefab.GetComponent<EntityStateMachine>();
            characterDeathBehavior.deathState = new SerializableEntityStateType(typeof(EntityStates.Commando.DeathState));

            NetworkStateMachine networkStateMachine = bodyComponent.GetComponent<NetworkStateMachine>();
            var list = networkStateMachine.stateMachines.ToList();
            networkStateMachine.stateMachines = list.ToArray();

            ContentAddition.AddBody(characterPrefab);
        }
        private void RegisterCharacter()
        {
            string desc = "A translator gone rogue, this new and improved model seeks to improve on his older counterparts, by any means necessary." +
                "<style=cSub>\r\n\r\n< ! > Shocking swing is an attack that's meant to be consistent, keeping the damage up and allowing him to melt down boss health."
                + Environment.NewLine +
                "<style=cSub>\r\n\r\n< ! > Nano-Spring allows Bastian to find his way out of a packed crowd, stunning enemies to allow for a few seconds of relief."
                + Environment.NewLine +
                "<style=cSub>\r\n\r\n< ! > For long-ranged enemies, Micro-Bullet is your best friend. Combined with backup mags, this is a lethal combo that can deal damage as fast as you can click."
                + Environment.NewLine +
                "<style=cSub>\r\n\r\n< ! > FOR EMERGENCIES ONLY! All Bastian Models are equipped with a \"Self - Utilized Ion Charge/ Internal Detonation for Emergencies\", dubbed the \"Architect Burst\" which will detonate the model and anything in it's surrounding area. Survival is possible, but only if action is taken IMMEDIATELY after usage.";

            string outro = "..and so he left, his faith in those he spoke for dwindling, and his sense of self blooming.";
            string fail = "..and so he vanished, a single spark among the current of change.";
            string lore = "A local robot, built to help negotiate deals with foreign planetary bodies. His original purpose was to function as a middle-man, designed to survive on planets with air and temps that a normal human couldn't withstand. " +
                "He was brought aboard the UES Contact Light as part of a backup plan, in case any negotiations for the survival of the other crew members came into play. " +
                "However, he ended up being reprogrammed for combat, despite this being a simple grab-and-go mission.";

            LanguageAPI.Add(SURVIVORNAMEKEY + "_NAME", SURVIVORNAME);
            LanguageAPI.Add(SURVIVORNAMEKEY + "_DESCRIPTION", desc);
            LanguageAPI.Add(SURVIVORNAMEKEY + "_SUBTITLE", "");
            LanguageAPI.Add(SURVIVORNAMEKEY + "_OUTRO", outro);
            LanguageAPI.Add(SURVIVORNAMEKEY + "_FAIL", fail);
            LanguageAPI.Add(SURVIVORNAMEKEY + "_LORE", lore);

            var survivorDef = ScriptableObject.CreateInstance<SurvivorDef>();
            {
                survivorDef.cachedName = SURVIVORNAMEKEY + "_NAME";
                survivorDef.unlockableDef = null;
                survivorDef.descriptionToken = SURVIVORNAMEKEY + "_DESCRIPTION";
                survivorDef.primaryColor = characterColor;
                survivorDef.bodyPrefab = characterPrefab;
                survivorDef.displayPrefab = Utils.NewDisplayModel(characterPrefab.GetComponent<ModelLocator>().modelBaseTransform.gameObject, SURVIVORNAME + "Display");
                survivorDef.outroFlavorToken = SURVIVORNAMEKEY + "_OUTRO";
                survivorDef.desiredSortPosition = 21;
                survivorDef.mainEndingEscapeFailureFlavorToken = SURVIVORNAMEKEY + "_FAIL";
            };

            ContentAddition.AddSurvivorDef(survivorDef);

            SkillSetup();

            var characterMaster = PrefabAPI.InstantiateClone(Prefabs.Load<GameObject>("RoR2/Base/Merc/MercMonsterMaster.prefab"), SURVIVORNAME + "Master", true);

            ContentAddition.AddMaster(characterMaster);

            CharacterMaster component = characterMaster.GetComponent<CharacterMaster>();
            component.bodyPrefab = characterPrefab;
        }
        void RegisterStates()
        {
            ContentAddition.AddEntityState<PrimaryButEpic>(out _);
            ContentAddition.AddEntityState<Secondary>(out _);
            ContentAddition.AddEntityState<Utility>(out _);
            ContentAddition.AddEntityState<UtilityStart>(out _);
            ContentAddition.AddEntityState<Special>(out _);
            ContentAddition.AddEntityState<SpecialStart>(out _);
            ContentAddition.AddEntityState<CharacterMain>(out _);
        }
        void SkillSetup()
        {
            foreach (GenericSkill obj in characterPrefab.GetComponentsInChildren<GenericSkill>())
            {
                BaseUnityPlugin.DestroyImmediate(obj);
            }
            PrimarySetup();
            SecondarySetup();
            UtilitySetup();
            SpecialSetup();

        }
        void PrimarySetup()
        {
            SkillLocator component = characterPrefab.GetComponent<SkillLocator>();
            LanguageAPI.Add(SURVIVORNAMEKEY + "_M1", "Shocking Swing");
            LanguageAPI.Add(SURVIVORNAMEKEY + "_M1_DESCRIPTION", "Deliver a shocking punch for <style=cIsDamage>250% damage</style>.");

            var SkillDef = ScriptableObject.CreateInstance<SteppedSkillDef>();
            SkillDef.activationState = new SerializableEntityStateType(typeof(PrimaryButEpic));
            SkillDef.activationStateMachineName = "Weapon";
            SkillDef.baseMaxStock = 0;
            SkillDef.baseRechargeInterval = 0f;
            SkillDef.beginSkillCooldownOnSkillEnd = true;
            SkillDef.canceledFromSprinting = false;
            SkillDef.fullRestockOnAssign = true;
            SkillDef.interruptPriority = InterruptPriority.Any;
            SkillDef.isCombatSkill = true;
            SkillDef.mustKeyPress = false;
            SkillDef.cancelSprintingOnActivation = true;
            SkillDef.rechargeStock = 0;
            SkillDef.requiredStock = 0;
            SkillDef.stockToConsume = 0;
            SkillDef.icon = Assets.MainAssetBundle.LoadAsset<Sprite>("primary");
            SkillDef.skillDescriptionToken = SURVIVORNAMEKEY + "_M1_DESCRIPTION";
            SkillDef.skillName = SURVIVORNAMEKEY + "_M1";
            SkillDef.skillNameToken = SURVIVORNAMEKEY + "_M1";
            SkillDef.stepCount = 2;
            SkillDef.stepGraceDuration = 0.5f;

            ContentAddition.AddSkillDef(SkillDef);

            component.primary = characterPrefab.AddComponent<GenericSkill>();
            SkillFamily newFamily = ScriptableObject.CreateInstance<SkillFamily>();
            newFamily.variants = new SkillFamily.Variant[1];
            component.primary.SetFieldValue("_skillFamily", newFamily);
            SkillFamily skillFamily = component.primary.skillFamily;

            skillFamily.variants[0] = new SkillFamily.Variant
            {
                skillDef = SkillDef,
                viewableNode = new ViewablesCatalog.Node(SkillDef.skillNameToken, false, null)
            };
            ContentAddition.AddSkillFamily(skillFamily);
        }
        void SecondarySetup()
        {
            SkillLocator component = characterPrefab.GetComponent<SkillLocator>();
            LanguageAPI.Add(SURVIVORNAMEKEY + "_M2", "Micro-Bullet");
            LanguageAPI.Add(SURVIVORNAMEKEY + "_M2_DESCRIPTION", "Fire 5 rounds of energy charges, each dealing <style=cIsDamage>200% damage</style>.");

            var SkillDef = ScriptableObject.CreateInstance<SkillDef>();
            SkillDef.activationState = new SerializableEntityStateType(typeof(Secondary));
            SkillDef.activationStateMachineName = "Weapon";
            SkillDef.baseMaxStock = 5;
            SkillDef.baseRechargeInterval = 8f;
            SkillDef.beginSkillCooldownOnSkillEnd = true;
            SkillDef.canceledFromSprinting = false;
            SkillDef.fullRestockOnAssign = true;
            SkillDef.interruptPriority = InterruptPriority.Skill;
            SkillDef.isCombatSkill = true;
            SkillDef.mustKeyPress = false;
            SkillDef.cancelSprintingOnActivation = true;
            SkillDef.rechargeStock = 5;
            SkillDef.requiredStock = 1;
            SkillDef.stockToConsume = 1;
            SkillDef.icon = Assets.MainAssetBundle.LoadAsset<Sprite>("secondary");
            SkillDef.skillDescriptionToken = SURVIVORNAMEKEY + "_M2_DESCRIPTION";
            SkillDef.skillName = SURVIVORNAMEKEY + "_M2";
            SkillDef.skillNameToken = SURVIVORNAMEKEY + "_M2";

            ContentAddition.AddSkillDef(SkillDef);

            Modules.Config.ConfigureSkillDef(SkillDef, configSection, "Secondary", true, true, true);

            component.secondary = characterPrefab.AddComponent<GenericSkill>();
            SkillFamily newFamily = ScriptableObject.CreateInstance<SkillFamily>();
            newFamily.variants = new SkillFamily.Variant[1];
            component.secondary.SetFieldValue("_skillFamily", newFamily);
            SkillFamily skillFamily = component.secondary.skillFamily;

            skillFamily.variants[0] = new SkillFamily.Variant
            {
                skillDef = SkillDef,
                viewableNode = new ViewablesCatalog.Node(SkillDef.skillNameToken, false, null)
            };
            ContentAddition.AddSkillFamily(skillFamily);

        }
        void UtilitySetup()
        {
            SkillLocator component = characterPrefab.GetComponent<SkillLocator>();
            LanguageAPI.Add(SURVIVORNAMEKEY + "_UTIL", "Nano-Spring");
            LanguageAPI.Add(SURVIVORNAMEKEY + "_UTIL_DESCRIPTION", "Execute a rapid dash in any given direction, stunning enemies in your path.");

            var SkillDef = ScriptableObject.CreateInstance<SkillDef>();
            SkillDef.activationState = new SerializableEntityStateType(typeof(UtilityStart));
            SkillDef.activationStateMachineName = "Body";
            SkillDef.baseMaxStock = 2;
            SkillDef.baseRechargeInterval = 5f;
            SkillDef.beginSkillCooldownOnSkillEnd = true;
            SkillDef.canceledFromSprinting = false;
            SkillDef.fullRestockOnAssign = false;
            SkillDef.interruptPriority = InterruptPriority.Any;
            SkillDef.isCombatSkill = false;
            SkillDef.mustKeyPress = true;
            SkillDef.cancelSprintingOnActivation = false;
            SkillDef.rechargeStock = 1;
            SkillDef.requiredStock = 1;
            SkillDef.stockToConsume = 1;
            SkillDef.icon = Assets.MainAssetBundle.LoadAsset<Sprite>("utility");
            SkillDef.skillDescriptionToken = SURVIVORNAMEKEY + "_UTIL_DESCRIPTION";
            SkillDef.skillName = SURVIVORNAMEKEY + "_UTIL";
            SkillDef.skillNameToken = SURVIVORNAMEKEY + "_UTIL";

            ContentAddition.AddSkillDef(SkillDef);

            Modules.Config.ConfigureSkillDef(SkillDef, configSection, "Utility");

            component.utility = characterPrefab.AddComponent<GenericSkill>();
            SkillFamily newFamily = ScriptableObject.CreateInstance<SkillFamily>();
            newFamily.variants = new SkillFamily.Variant[1];
            component.utility.SetFieldValue("_skillFamily", newFamily);
            SkillFamily skillFamily = component.utility.skillFamily;
            skillFamily.variants[0] = new SkillFamily.Variant
            {
                skillDef = SkillDef,
                viewableNode = new ViewablesCatalog.Node(SkillDef.skillNameToken, false, null)
            };
            ContentAddition.AddSkillFamily(skillFamily);

        }
        void SpecialSetup()
        {
            SkillLocator component = characterPrefab.GetComponent<SkillLocator>();
            LanguageAPI.Add(SURVIVORNAMEKEY + "_SPEC", "Architect Burst");
            LanguageAPI.Add(SURVIVORNAMEKEY + "_SPEC_DESCRIPTION", "<style=cIsUtility>EVASIVE</style>. Overload your body for a massive damage burst, at the cost of your movement speed and <style=cIsHealth>1/3 health</style>.");

            var SkillDef = ScriptableObject.CreateInstance<SkillDef>();
            SkillDef.activationState = new SerializableEntityStateType(typeof(SpecialStart));
            SkillDef.activationStateMachineName = "Weapon";
            SkillDef.baseMaxStock = 1;
            SkillDef.baseRechargeInterval = 20f;
            SkillDef.beginSkillCooldownOnSkillEnd = true;
            SkillDef.canceledFromSprinting = false;
            SkillDef.fullRestockOnAssign = false;
            SkillDef.interruptPriority = InterruptPriority.Skill;
            SkillDef.isCombatSkill = true;
            SkillDef.mustKeyPress = true;
            SkillDef.cancelSprintingOnActivation = false;
            SkillDef.rechargeStock = 1;
            SkillDef.requiredStock = 1;
            SkillDef.stockToConsume = 1;
            SkillDef.icon = Assets.MainAssetBundle.LoadAsset<Sprite>("special");
            SkillDef.skillDescriptionToken = SURVIVORNAMEKEY + "_SPEC_DESCRIPTION";
            SkillDef.skillName = SURVIVORNAMEKEY + "_SPEC";
            SkillDef.skillNameToken = SURVIVORNAMEKEY + "_SPEC";

            ContentAddition.AddSkillDef(SkillDef);

            Modules.Config.ConfigureSkillDef(SkillDef, configSection, "Special");

            component.special = characterPrefab.AddComponent<GenericSkill>();
            SkillFamily newFamily = ScriptableObject.CreateInstance<SkillFamily>();
            newFamily.variants = new SkillFamily.Variant[1];
            component.special.SetFieldValue("_skillFamily", newFamily);
            SkillFamily skillFamily = component.special.skillFamily;

            skillFamily.variants[0] = new SkillFamily.Variant
            {
                skillDef = SkillDef,
                viewableNode = new ViewablesCatalog.Node(SkillDef.skillNameToken, false, null)
            };
            ContentAddition.AddSkillFamily(skillFamily);

        }
    }
}
