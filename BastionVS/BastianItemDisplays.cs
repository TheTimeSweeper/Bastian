using Bastian.Modules;
using HG;
using RoR2;
using RoR2.ContentManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

/* for custom copy format in keb's helper
{childName},
                    {localPos}, 
                    {localAngles},
                    {localScale})
*/

namespace Bastian
{
    class BastianItemDisplays
    {
        private ItemDisplayRuleSet itemDisplayRuleSet;

        public void Init(ItemDisplayRuleSet itemDisplayRuleSet_)
        {
            itemDisplayRuleSet = itemDisplayRuleSet_;
            RoR2.ContentManagement.ContentManager.onContentPacksAssigned += SetItemDisplays;
        }

        private void SetItemDisplays(ReadOnlyArray<ReadOnlyContentPack> obj)
        {
            ItemDisplays.LazyInit();

            List<ItemDisplayRuleSet.KeyAssetRuleGroup> itemDisplayRules = new List<ItemDisplayRuleSet.KeyAssetRuleGroup>();
            TheFunStuff(itemDisplayRules);
            itemDisplayRuleSet.keyAssetRuleGroups = itemDisplayRules.ToArray();

            ItemDisplays.DisposeWhenDone();
        }

        private void TheFunStuff(List<ItemDisplayRuleSet.KeyAssetRuleGroup> itemDisplayRules)
        {
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(RoR2Content.Items.Syringe,
                ItemDisplays.CreateDisplayRule(GetPrefab("RoR2/Base/Syringe/DisplaySyringeCluster.prefab"),
                    "Chest",
                    new Vector3(0.65914F, 0.09031F, -0.44892F),
                    new Vector3(296.4444F, 67.4237F, 241.678F),
                    new Vector3(0.88676F, 0.88676F, 0.88676F)
                    )
                ));

            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(RoR2Content.Items.SecondarySkillMagazine,
                ItemDisplays.CreateDisplayRule(GetPrefab("RoR2/Base/SecondarySkillMagazine/DisplayDoubleMag.prefab"),
                    "forearm.R",
                    new Vector3(-0.30973F, 1.15523F, -0.92675F),
                    new Vector3(314.3555F, 353.8904F, 22.50784F),
                    new Vector3(0.35241F, 0.35241F, 0.35241F)
                    )
                ));

            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(RoR2Content.Items.Bear,
                ItemDisplays.CreateDisplayRule(GetPrefab("RoR2/Base/Bear/DisplayBear.prefab"),
                    "Chest",
                    new Vector3(0.03924F, 1.53129F, -0.62422F),
                    new Vector3(339.5294F, 184.3654F, 1.10872F),
                    new Vector3(0.9F, 0.9F, 0.9F)
                    //"Chest",
                    //new Vector3(0.00002F, 0.84445F, -1.07513F),
                    //new Vector3(351.5264F, 183.8089F, 0F),
                    //new Vector3(1F, 1F, 1F)
                    )
                ));

            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(DLC1Content.Items.BearVoid,
                ItemDisplays.CreateDisplayRule(GetPrefab("RoR2/DLC1/BearVoid/DisplayBearVoid.prefab"),
                    "Chest",
                    new Vector3(0.03924F, 1.53129F, -0.62422F),
                    new Vector3(339.5294F, 184.3654F, 1.10872F),
                    new Vector3(0.9F, 0.9F, 0.9F)
                    )
                ));

            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(RoR2Content.Items.CritGlasses,
                ItemDisplays.CreateDisplayRule(GetPrefab("RoR2/Base/CritGlasses/DisplayGlasses.prefab"),
                    "head",
                    new Vector3(0.00001F, 0.80001F, 0.53515F),
                    new Vector3(0F, 0F, 0F),
                    new Vector3(1.1096F, 1.1412F, 0.34684F)
                    )
                ));

            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(DLC1Content.Items.CritGlassesVoid,
                ItemDisplays.CreateDisplayRule(GetPrefab("RoR2/DLC1/CritGlassesVoid/DisplayGlassesVoid.prefab"),
                    "head",
                    new Vector3(0.00001F, 0.80001F, 0.53515F),
                    new Vector3(0F, 0F, 0F),
                    new Vector3(1.1096F, 1.1412F, 0.34684F)
                    )
                ));

            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["ArmorPlate"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayRepulsionArmorPlate"),
                    "shin.L",
                    new Vector3(-0.00835F, 0.78056F, -0.1653F),
                    new Vector3(86.38264F, 21.33881F, 31.062F),
                    new Vector3(1F, 1F, 1F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Bandolier"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayBandolier"),
                    "LowerArmR",
                    new Vector3(0.07031F, 0.4685F, -0.03584F),
                    new Vector3(272.6296F, 233.1989F, 112.998F),
                    new Vector3(0.91672F, 1.19463F, 1F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["BarrierOnKill"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayBrooch"),
                    "neck",
                    new Vector3(0.03062F, 0.32682F, 0.55458F),
                    new Vector3(45.26312F, 2.29439F, 0.62226F),
                    new Vector3(1.35937F, 1.35937F, 1.35937F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["DeathProjectile"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayDeathProjectile"),
                    "thigh.L",
                    new Vector3(-0.63108F, 0.42659F, -0.09557F),
                    new Vector3(354.9184F, 273.6032F, 165.5404F),
                    new Vector3(0.27578F, 0.27578F, 0.27578F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Hoof"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayHoof"),
                    "shin.L",
                    new Vector3(-0.02676F, 2.24497F, -0.23945F),
                    new Vector3(77.77466F, 7.5278F, 339.761F),
                    new Vector3(0.52266F, 0.52266F, 0.52266F)
                    ),
                ItemDisplays.CreateLimbMaskDisplayRule(LimbFlags.RightCalf)
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["ElementalRingVoid"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayVoidRing"),
                    "LowerArmR",
                    new Vector3(0.00007F, 0.83197F, -0.04357F),
                    new Vector3(87.76192F, 180F, 180F),
                    new Vector3(1.25F, 1.25F, 1.25F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["EquipmentMagazineVoid"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayFuelCellVoid"),
                    "thigh.L",
                    new Vector3(-0.12464F, 0.28825F, -0.60926F),
                    new Vector3(3.85885F, 36.05694F, 180.5845F),
                    new Vector3(0.625F, 0.625F, 0.625F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["HalfAttackSpeedHalfCooldowns"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayLunarShoulderNature"),
                    "upper_arm.L",
                    new Vector3(0.24997F, -0.02843F, 0.07597F),
                    new Vector3(351.8407F, 2.84121F, 222.6856F),
                    new Vector3(2.55F, 2.55F, 2.55F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Lightning"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayLightningArmRight"),
                    "UpperArmR",
                    new Vector3(0F, 0F, 0F),
                    new Vector3(0F, 0F, 0F),
                    new Vector3(4.06211F, 4.06211F, 4.06211F)
                    ),
                ItemDisplays.CreateLimbMaskDisplayRule(LimbFlags.RightArm)
                ));

            #region the rest

            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["AlienHead"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayAlienHead"),
                    "head",
                    new Vector3(0.5111F, 0.48275F, 0.07351F),
                    new Vector3(302.6911F, 318.6962F, 36.48134F),
                    new Vector3(1.25F, 1.25F, 1.25F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["ArmorReductionOnHit"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayWarhammer"),
                    "Chest",
                    new Vector3(0.80498F, 1.55764F, -0.81514F),
                    new Vector3(315.3939F, 77.88396F, 287.3567F),
                    new Vector3(1F, 1F, 1F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["AttackSpeedAndMoveSpeed"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayCoffee"),
                    "thigh.L",
                    new Vector3(-0.07729F, 0.14655F, 0.64592F),
                    new Vector3(348.6571F, 307.7055F, 182.8272F),
                    new Vector3(0.65F, 0.65F, 0.65F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["AttackSpeedOnCrit"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayWolfPelt"),
                    "upper_arm.L",
                    new Vector3(0.00434F, 1.80601F, -0.32815F),
                    new Vector3(284.4092F, 170.8345F, 200.7453F),
                    new Vector3(1.05F, 1.05F, 1.05F) // really didn't want to put it on the head, and i already saved the shoulder for lunar pauldrons + dios 
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["AutoCastEquipment"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayFossil"),
                    "head",
                    new Vector3(0.57517F, 0.70778F, 0.06581F),
                    new Vector3(40.96642F, 178.2968F, 2.4525F),
                    new Vector3(0.825F, 0.825F, 0.825F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["BarrierOnOverHeal"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayAegis"),
                    "forearm.L",
                    new Vector3(0.14603F, 0.07325F, -0.27036F),
                    new Vector3(76.55911F, 237.4158F, 305.7302F),
                    new Vector3(0.85F, 0.85F, 0.85F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["BeetleGland"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayBeetleGland"),
                    "stomach",
                    new Vector3(-1.02084F, 0.42962F, 0.0658F),
                    new Vector3(5.29222F, 268.5566F, 325.7473F),
                    new Vector3(0.325F, 0.325F, 0.35F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Behemoth"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayBehemoth"),
                    "LowerArmR",
                    new Vector3(-0.0899F, 1.26216F, 0.4964F),
                    new Vector3(359.603F, 346.9713F, 358.2848F),
                    new Vector3(0.175F, 0.175F, 0.175F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["BleedOnHit"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayTriTip"),
                    "fingerMuzzle",
                    new Vector3(-0.04328F, 0.11021F, 0.32714F),
                    new Vector3(357.1168F, 185.5943F, 179.7177F),
                    new Vector3(1F, 1F, 1F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["BleedOnHitAndExplode"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayBleedOnHitAndExplode"),
                    "thigh.R",
                    new Vector3(0.2092F, -0.12873F, 0.26331F),
                    new Vector3(6.37099F, 15.64148F, 205.9389F),
                    new Vector3(0.225F, 0.225F, 0.225F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["BleedOnHitVoid"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayTriTipVoid"),
                    "fingerMuzzle",
                    new Vector3(-0.02598F, 0.10146F, 0.4725F),
                    new Vector3(357.1168F, 185.5943F, 179.7177F),
                    new Vector3(0.65F, 0.65F, 0.65F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["BonusGoldPackOnKill"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayTome"),
                    "thigh.L",
                    new Vector3(-0.40909F, 0.18814F, -0.08888F),
                    new Vector3(0.135F, 273.9857F, 267.9514F),
                    new Vector3(0.285F, 0.285F, 0.285F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["BossDamageBonus"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayAPRound"),
                    "upper_arm.L",
                    new Vector3(0.24169F, 0.8004F, -0.28217F),
                    new Vector3(277.8599F, 245.2563F, 224.8089F),
                    new Vector3(1.5F, 1.5F, 1.5F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["BounceNearby"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayHook"),
                    "neck",
                    new Vector3(-0.08829F, 0.55066F, -0.35047F),
                    new Vector3(343.724F, 193.6995F, 181.7064F),
                    new Vector3(0.5F, 0.5F, 0.5F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["ChainLightning"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayUkulele"),
                    "Chest",
                    new Vector3(0.31914F, 0.06933F, -1.08754F),
                    new Vector3(343.7868F, 176.6131F, 326.4982F),
                    new Vector3(2.95F, 2.95F, 2.95F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["ChainLightningVoid"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayUkuleleVoid"),
                    "Chest",
                    new Vector3(0.33914F, 0.04835F, -1.11396F),
                    new Vector3(343.7868F, 176.6131F, 326.4982F),
                    new Vector3(2.95F, 2.95F, 2.95F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Clover"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayClover"),
                    "Chest",
                    new Vector3(0.82893F, 0.84994F, 0.69815F),
                    new Vector3(74.8625F, 346.2426F, 359.3071F),
                    new Vector3(1F, 1F, 1F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["CloverVoid"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayCloverVoid"),
                    "Chest",
                    new Vector3(0.82893F, 0.84994F, 0.69815F),
                    new Vector3(78.82142F, 345.9959F, 359.0669F),
                    new Vector3(1.05F, 1.05F, 1.05F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["CooldownOnCrit"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplaySkull"),
                    "thigh.R",
                    new Vector3(-0.42009F, 0.78623F, -0.26855F),
                    new Vector3(72.92178F, 229.2379F, 173.043F),
                    new Vector3(0.38797F, 0.5F, 0.42501F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["CritDamage"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayLaserSight"),
                    "fingerMuzzle",
                    new Vector3(-0.08153F, 0.00993F, 0.46314F),
                    new Vector3(84.65108F, 208.7761F, 295.0798F),
                    new Vector3(0.115F, 0.115F, 0.115F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Crowbar"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayCrowbar"),
                    "Chest",
                    new Vector3(0.75592F, 0.69083F, -0.6279F),
                    new Vector3(8.69183F, 337.6341F, 339.0851F),
                    new Vector3(1.25F, 1.25F, 1.25F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Dagger"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayDagger"),
                    "UpperArmR",
                    new Vector3(0.21383F, -0.20976F, 0.04857F),
                    new Vector3(13.2054F, 66.17889F, 167.8059F),
                    new Vector3(3F, 3F, 3F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["DeathMark"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayDeathMark"),
                    "hand.R",
                    new Vector3(-0.10332F, 0.52485F, -0.03046F),
                    new Vector3(286.5023F, 278.106F, 346.4438F),
                    new Vector3(0.1F, 0.1F, 0.1F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["LunarSun"],
                ItemDisplays.CreateLimbMaskDisplayRule(LimbFlags.Head),
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplaySunHeadNeck"),
                    "neck",
                    new Vector3(0.023F, 0.38215F, 0.00772F),
                    new Vector3(0F, 40.97836F, 0F),
                    new Vector3(4F, 4F, 4F)
                    ),
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplaySunHead"),
                    "head",
                    new Vector3(0.00001F, 0.69484F, -0.04622F),
                    new Vector3(0F, 0F, 0F),
                    new Vector3(3.75F, 3.75F, 3.75F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["EnergizedOnEquipmentUse"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayWarHorn"),
                    "stomach",
                    new Vector3(0.62817F, 0.09141F, -0.38643F),
                    new Vector3(334.6201F, 107.1652F, 36.12887F),
                    new Vector3(1.25F, 1.25F, 1.25F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["EquipmentMagazine"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayBattery"),
                    "thigh.L",
                    new Vector3(-0.12464F, 0.28825F, -0.60926F),
                    new Vector3(85.61769F, 208.3847F, 352.345F),
                    new Vector3(0.625F, 0.625F, 0.625F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["ExecuteLowHealthElite"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayGuillotine"),
                    "hand.L",
                    new Vector3(0.1512F, 0.34743F, -0.0046F),
                    new Vector3(84.51624F, 296.4939F, 204.7878F),
                    new Vector3(0.45F, 0.45F, 0.45F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["ExplodeOnDeath"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayWilloWisp"),
                    "thigh.R",
                    new Vector3(-0.43468F, 0.38866F, -0.56036F),
                    new Vector3(352.2151F, 138.4997F, 184.8371F),
                    new Vector3(0.175F, 0.175F, 0.175F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["ExplodeOnDeathVoid"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayWillowWispVoid"),
                    "thigh.R",
                    new Vector3(-0.42377F, 0.35028F, -0.55874F),
                    new Vector3(352.2151F, 138.4997F, 184.8371F),
                    new Vector3(0.175F, 0.175F, 0.175F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["ExtraLife"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayHippo"),
                    "shoulder.L",
                    new Vector3(0.14066F, 0.48374F, 0.53259F),
                    new Vector3(12.43329F, 316.0789F, 259.6472F),
                    new Vector3(0.75F, 0.75F, 0.75F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["ExtraLifeVoid"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayHippoVoid"),
                    "shoulder.L",
                    new Vector3(0.08585F, 0.53249F, 0.36434F),
                    new Vector3(2.53777F, 317.8202F, 283.0677F),
                    new Vector3(0.75F, 0.75F, 0.75F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["FallBoots"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayGravBoots"),
                    "shin.L",
                    new Vector3(-0.00596F, 2.4321F, -0.00846F),
                    new Vector3(352.0836F, 0.74224F, 354.6263F),
                    new Vector3(0.99165F, 1F, 1.09991F)
                    ),
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayGravBoots"),
                    "shin.R",
                    new Vector3(-0.00596F, 2.4321F, -0.00846F),
                    new Vector3(352.0836F, 0.74224F, 354.6263F),
                    new Vector3(0.99165F, 1F, 1.09991F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Feather"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayFeather"),
                    "head",
                    new Vector3(-0.02729F, 1.06048F, -0.44469F),
                    new Vector3(283.3843F, 0F, 0F),
                    new Vector3(0.075F, 0.075F, 0.075F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["FireballsOnHit"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayFireballsOnHit"),
                    "foot.l",
                    new Vector3(0.0288F, -0.13096F, 0.47934F),
                    new Vector3(61.58504F, 7.82522F, 187.1064F),
                    new Vector3(0.19F, 0.19F, 0.19F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["FireRing"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayFireRing"),
                    "LowerArmR",
                    new Vector3(0.00013F, 0.97745F, -0.038F),
                    new Vector3(87.762F, 180F, 180F),
                    new Vector3(1.225F, 1.225F, 1.225F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Firework"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayFirework"),
                    "shoulder.L",
                    new Vector3(-0.64439F, 0.30536F, -0.07126F),
                    new Vector3(8.45015F, 28.80458F, 294.7059F),
                    new Vector3(1F, 1F, 1F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["FlatHealth"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplaySteakCurved"),
                    "head",
                    new Vector3(0.23406F, 1.39417F, -0.10743F),
                    new Vector3(292.2183F, 91.92725F, 11.06188F),
                    new Vector3(-0.385F, 0.385F, 0.385F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["FocusConvergence"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayFocusedConvergence"),
                    "Chest",
                    new Vector3(-1.35324F, 2.33636F, -1.09748F),
                    new Vector3(0F, 0F, 0F),
                    new Vector3(0.05F, 0.05F, 0.05F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["FragileDamageBonus"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayDelicateWatch"),
                    "forearm.L",
                    new Vector3(0.00445F, 1.22666F, -0.01165F),
                    new Vector3(279.0276F, 91.9601F, 178.853F),
                    new Vector3(2.78957F, 5.95044F, 3F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["FreeChest"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayShippingRequestForm"),
                    "thigh.R",
                    new Vector3(0.39698F, 0.2586F, -0.13709F),
                    new Vector3(281.985F, 22.7633F, 258.1265F),
                    new Vector3(1.5F, 1.5F, 1.5F)
                    //"thigh.R",
                    //new Vector3(0.40914F, -0.05285F, 0.03904F),
                    //new Vector3(297.3885F, 24.37732F, 257.6275F),
                    //new Vector3(1.5F, 1.5F, 1.5F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["GhostOnKill"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayMask"),
                    "head",
                    new Vector3(0F, 0.69487F, 0.37315F),
                    new Vector3(349.5436F, 0F, 0F),
                    new Vector3(2.2F, 2.2F, 2.2F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["GoldOnHit"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayBoneCrown"),
                    "head",
                    new Vector3(-0.21325F, 1.2826F, -0.12616F),
                    new Vector3(340.9547F, 334.3771F, 28.53958F),
                    new Vector3(1.45F, 1.45F, 1.45F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["GoldOnHurt"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayRollOfPennies"),
                    "neck",
                    new Vector3(-0.27554F, 0.44683F, 0.08916F),
                    new Vector3(356.3288F, 252.6902F, 358.4403F),
                    new Vector3(1.2F, 1.2F, 1.2F)
                    ),
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayRollOfPennies"),
                    "neck",
                    new Vector3(-0.28499F, 0.45886F, 0.04706F),
                    new Vector3(25.28688F, 253.1018F, 21.15645F),
                    new Vector3(1.2F, 1.2F, 1.2F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["HalfSpeedDoubleHealth"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayLunarShoulderStone"),
                    "shoulder.R",
                    new Vector3(-0.02848F, 1.36768F, 0.00426F),
                    new Vector3(7.53408F, 255.7398F, 262.8926F),
                    new Vector3(2.55F, 2.55F, 2.55F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["HeadHunter"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplaySkullcrown"),
                    "head",
                    new Vector3(0.00001F, 0.9819F, 0.17078F),
                    new Vector3(331.572F, 0F, 0F),
                    new Vector3(0.5F, 0.5F, 0.5F) //item was fucky in editor - might need to see it in another build first
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["HealingPotion"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayHealingPotion"),
                    "thigh.L",
                    new Vector3(-0.23108F, 0.5099F, 0.41779F),
                    new Vector3(13.24944F, 7.61538F, 196.8625F),
                    new Vector3(0.2F, 0.2F, 0.2F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["HealOnCrit"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayScythe"),
                    "upper_arm.L",
                    new Vector3(0.03924F, 0.57957F, -0.43947F),
                    new Vector3(79.18025F, 67.32583F, 243.8508F),
                    new Vector3(0.5F, 0.5F, 0.5F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["HealWhileSafe"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplaySnail"),
                    "upper_arm.L",
                    new Vector3(0.12583F, 1.16878F, -0.09158F),
                    new Vector3(346.0052F, 190.0813F, 87.96476F),
                    new Vector3(0.15F, 0.15F, 0.15F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["IceRing"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayIceRing"),
                    "LowerArmR",
                    new Vector3(0.00011F, 0.68072F, -0.04957F),
                    new Vector3(87.76209F, 180F, 180F),
                    new Vector3(1.225F, 1.225F, 1.225F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Icicle"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayFrostRelic"),
                    "Chest",
                    new Vector3(-0.82947F, 1.21202F, -1.91688F),
                    new Vector3(55.95175F, 312.4438F, 317.4887F),
                    new Vector3(0.8F, 0.8F, 0.8F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["IgniteOnKill"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayGasoline"),
                    "thigh.R",
                    new Vector3(0.11328F, 0.4911F, -0.4045F),
                    new Vector3(77.30357F, 204.8111F, 139.3803F),
                    new Vector3(1.5F, 1.5F, 1.5F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["ImmuneToDebuff"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayRainCoatBelt"),
                    "UpperArmR",
                    new Vector3(0.11087F, 0.91323F, 0.09146F),
                    new Vector3(353.0442F, 39.34427F, 180.52F),
                    new Vector3(2F, 2F, 2F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["IncreaseHealing"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayAntler"),
                    "head",
                    new Vector3(-0.15093F, 0.98046F, -0.11134F),
                    new Vector3(0F, 271.6122F, 0F),
                    new Vector3(0.95F, 0.95F, 0.95F)
                    ),
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayAntler"),
                    "head",
                    new Vector3(0.13872F, 0.97915F, -0.11948F),
                    new Vector3(0F, 271.6122F, 0F),
                    new Vector3(0.95F, 0.95F, -0.95F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Incubator"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayAncestralIncubator"),
                    "shoulder.L",
                    new Vector3(0.08305F, 1.14571F, 0.15555F),
                    new Vector3(72.01577F, 334.7948F, 312.5343F),
                    new Vector3(0.05F, 0.05F, 0.05F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Infusion"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayInfusion"),
                    "upper_arm.L",
                    new Vector3(0.29344F, 1.12625F, -0.17525F),
                    new Vector3(0.58085F, 98.87872F, 176.272F),
                    new Vector3(1.2F, 1.2F, 1.2F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["JumpBoost"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayWaxBird"),
                    "head",
                    new Vector3(-0.03038F, 0.61813F, -0.12434F),
                    new Vector3(0F, 0F, 0F),
                    new Vector3(1.35F, 1.35F, 1.35F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["KillEliteFrenzy"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayBrainstalk"),
                    "head",
                    new Vector3(-0.03666F, 0.85729F, 0.00004F),
                    new Vector3(0F, 0F, 0F),
                    new Vector3(0.825F, 0.825F, 0.825F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Knurl"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayKnurl"),
                    "thigh.R",
                    new Vector3(-0.03247F, 1.46697F, -0.35374F),
                    new Vector3(58.38268F, 163.1733F, 69.40005F),
                    new Vector3(0.175F, 0.175F, 0.175F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["LaserTurbine"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayLaserTurbine"),
                    "thigh.R",
                    new Vector3(-0.1953F, 1.62086F, 0.35522F),
                    new Vector3(7.08666F, 172.4889F, 196.8262F),
                    new Vector3(0.85F, 0.85F, 0.85F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["LightningStrikeOnHit"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayChargedPerforator"),
                    "foot.r",
                    new Vector3(0.02094F, -0.00379F, 0.46626F),
                    new Vector3(34.37665F, 179.2556F, 355.9098F),
                    new Vector3(2.85F, 2.85F, 2.85F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["LunarDagger"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayLunarDagger"),
                    "LowerArmR",
                    new Vector3(0.27176F, 0.53667F, 0.35409F),
                    new Vector3(293.4359F, 350.9343F, 259.8839F),
                    new Vector3(1.25F, 1.25F, 1.25F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["LunarPrimaryReplacement"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayBirdEye"),
                    "head",
                    new Vector3(0.18131F, 0.74912F, 0.48168F),
                    new Vector3(275.9312F, 180F, 184.3448F),
                    new Vector3(1F, 1F, 1F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["LunarSecondaryReplacement"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayBirdClaw"),
                    "thigh.L",
                    new Vector3(0.03513F, 1.78325F, -0.59014F),
                    new Vector3(357.2934F, 84.62841F, 299.5834F),
                    new Vector3(1.25F, 1.25F, 1.25F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["LunarSpecialReplacement"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayBirdHeart"),
                    "Chest",
                    new Vector3(-1.03119F, 2.86487F, 0.27696F),
                    new Vector3(0F, 0F, 0F),
                    new Vector3(0.125F, 0.125F, 0.125F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["LunarTrinket"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayBeads"),
                    "forearm.L",
                    new Vector3(-0.01115F, 0.34872F, 0.26127F),
                    new Vector3(351.588F, 240.3141F, 117.2933F),
                    new Vector3(3.5F, 3.5F, 3.5F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["LunarUtilityReplacement"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayBirdFoot"),
                    "shin.R",
                    new Vector3(0.01069F, 0.27533F, -0.50857F),
                    new Vector3(1.10389F, 82.98939F, 256.8687F),
                    new Vector3(2F, 2F, 2F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Medkit"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayMedkit"),
                    "thigh.L",
                    new Vector3(-0.20032F, 0.92296F, -0.33502F),
                    new Vector3(86.81291F, 33.12066F, 344.4794F),
                    new Vector3(1.5F, 1.5F, 1.5F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["MinorConstructOnKill"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayDefenseNucleus"),
                    "Chest",
                    new Vector3(-1.92226F, 2.49894F, -0.33007F),
                    new Vector3(4.83807F, 0F, 0F),
                    new Vector3(0.15F, 0.15F, 0.15F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Missile"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayMissileLauncher"),
                    "shoulder.L",
                    new Vector3(0.10621F, -0.08337F, 1.04984F),
                    new Vector3(25.6844F, 278.0459F, 278.0459F),
                    new Vector3(0.275F, 0.275F, 0.275F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["MissileVoid"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayMissileLauncherVoid"),
                    "shoulder.L",
                    new Vector3(0.10621F, -0.08337F, 1.04984F),
                    new Vector3(25.6844F, 278.0459F, 278.0459F),
                    new Vector3(0.275F, 0.275F, 0.275F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["MonstersOnShrineUse"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayMonstersOnShrineUse"),
                    "upper_arm.L",
                    new Vector3(0.10616F, 0.89927F, 0.26584F),
                    new Vector3(28.07198F, 139.2053F, 4.92336F),
                    new Vector3(0.2F, 0.2F, 0.2F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["MoreMissile"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayICBM"),
                    "thigh.R",
                    new Vector3(0.27514F, 1.25113F, -0.1916F),
                    new Vector3(356.3924F, 189.6946F, 177.7955F),
                    new Vector3(0.3F, 0.3F, 0.3F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["MoveSpeedOnKill"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayGrappleHook"),
                    "thigh.L",
                    new Vector3(-0.35118F, 1.97815F, 0.10365F),
                    new Vector3(6.00314F, 2.54896F, 358.0298F),
                    new Vector3(0.46197F, 0.45F, 0.45F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Mushroom"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayMushroom"),
                    "forearm.L",
                    new Vector3(0.21204F, 0.3081F, 0.10448F),
                    new Vector3(353.6204F, 329.489F, 259.322F),
                    new Vector3(0.15F, 0.15F, 0.15F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["MushroomVoid"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayMushroomVoid"),
                    "forearm.L",
                    new Vector3(0.21204F, 0.3081F, 0.10448F),
                    new Vector3(353.6204F, 329.489F, 259.322F),
                    new Vector3(0.15F, 0.15F, 0.15F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["NearbyDamageBonus"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayDiamond"),
                    "hand.L",
                    new Vector3(0.18588F, 0.26959F, -0.00007F),
                    new Vector3(0F, 0F, 0F),
                    new Vector3(0.15F, 0.15F, 0.15F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["NovaOnHeal"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayDevilHorns"),
                    "head",
                    new Vector3(0.42619F, 1.01539F, 0.02603F),
                    new Vector3(342.5062F, 356.5984F, 1.02362F),
                    new Vector3(1.4F, 1.4F, 1.4F)
                    ),
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayDevilHorns"),
                    "head",
                    new Vector3(-0.26547F, 1.0534F, 0.16303F),
                    new Vector3(350.9752F, 334.1729F, 351.3747F),
                    new Vector3(-1.4F, 1.4F, 1.4F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["NovaOnLowHealth"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayJellyGuts"),
                    "shoulder.R",
                    new Vector3(0.24947F, 0.03511F, 0.15334F),
                    new Vector3(318.4078F, 171.9727F, 343.6446F),
                    new Vector3(0.475F, 0.475F, 0.475F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["OutOfCombatArmor"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayOddlyShapedOpal"),
                    "shoulder.L",
                    new Vector3(-0.6177F, 0.64471F, 0.01785F),
                    new Vector3(358.7075F, 300.6926F, 96.65579F),
                    new Vector3(1F, 1F, 1F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["ParentEgg"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayParentEgg"),
                    "stomach",
                    new Vector3(-0.91142F, 0.2723F, -0.46619F),
                    new Vector3(349.9658F, 212.4049F, 0F),
                    new Vector3(0.2F, 0.2F, 0.2F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Pearl"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayPearl"),
                    "head",
                    new Vector3(0.00677F, 0.73442F, 0.0915F),
                    new Vector3(290.516F, 180F, 180F),
                    new Vector3(0.35F, 0.35F, 0.35F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["PermanentDebuffOnHit"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayScorpion"),
                    "head",
                    new Vector3(0.0008F, 1.01485F, -0.51655F),
                    new Vector3(342.6157F, 3.44338F, 179.4629F),
                    new Vector3(1.5F, 1.5F, 1.5F) // no ass bone, crying
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["PersonalShield"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayShieldGenerator"),
                    "head",
                    new Vector3(-0.49142F, 0.70056F, 0.05503F),
                    new Vector3(47.03817F, 173.7864F, 85.57451F),
                    new Vector3(0.35F, 0.35F, 0.35F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Phasing"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayStealthkit"),
                    "upper_arm.L",
                    new Vector3(0.04156F, 1.19074F, 0.31912F),
                    new Vector3(276.1288F, 132.2501F, 241.6219F),
                    new Vector3(0.85F, 0.85F, 0.85F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Plant"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayInterstellarDeskPlant"),
                    "upper_arm.L",
                    new Vector3(-0.01453F, 0.8484F, -0.31034F),
                    new Vector3(11.07422F, 180.3719F, 179.9338F),
                    new Vector3(0.2F, 0.2F, 0.2F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["PrimarySkillShuriken"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayShuriken"),
                    "thigh.R",
                    new Vector3(-0.46117F, 1.48286F, -0.12175F),
                    new Vector3(5.53812F, 69.80327F, 5.45595F),
                    new Vector3(0.85F, 0.85F, 0.85F)
                    ),
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayShuriken"),
                    "thigh.R",
                    new Vector3(-0.31009F, 1.87571F, -0.2966F),
                    new Vector3(7.57949F, 51.05711F, 334.4413F),
                    new Vector3(0.85F, 0.85F, 0.85F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["RandomDamageZone"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayRandomDamageZone"),
                    "head",
                    new Vector3(0.00001F, 1.14339F, -0.51373F),
                    new Vector3(7.67072F, 0F, 0F),
                    new Vector3(0.2F, 0.2F, 0.2F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["RandomEquipmentTrigger"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayBottledChaos"),
                    "stomach",
                    new Vector3(0.81706F, -0.09517F, 0.01592F),
                    new Vector3(357.1425F, 273.676F, 0.18279F),
                    new Vector3(0.65F, 0.65F, 0.65F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["RandomlyLunar"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayDomino"),
                    "Chest",
                    new Vector3(-0.56864F, 3.15622F, -1.42046F),
                    new Vector3(0F, 0F, 0F),
                    new Vector3(0.55F, 0.55F, 0.55F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["RegeneratingScrap"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayRegeneratingScrap"),
                    "shoulder.L",
                    new Vector3(0.2326F, 1.23952F, -0.08706F),
                    new Vector3(36.73652F, 309.901F, 321.1572F),
                    new Vector3(0.7F, 0.7F, 0.7F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["RepeatHeal"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayCorpseflower"),
                    "stomach",
                    new Vector3(-0.70311F, 0.38833F, -0.46985F),
                    new Vector3(303.3846F, 328.6296F, 85.79055F),
                    new Vector3(0.75F, 0.75F, 0.75F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Seed"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplaySeed"),
                    "shin.R",
                    new Vector3(0.32065F, 0.17391F, -0.11726F),
                    new Vector3(9.53142F, 143.8804F, 216.2039F),
                    new Vector3(0.15F, 0.15F, 0.15F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["ShieldOnly"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayShieldBug"),
                    "head",
                    new Vector3(-0.16482F, 1.14432F, 0.37755F),
                    new Vector3(351.498F, 80.39565F, 346.5061F),
                    new Vector3(0.9F, 0.9F, 0.9F)
                    ),
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayShieldBug"),
                    "head",
                    new Vector3(0.19974F, 1.14118F, 0.3707F),
                    new Vector3(6.79524F, 106.1144F, 347.0914F),
                    new Vector3(0.9F, 0.9F, -0.9F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["ShinyPearl"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayShinyPearl"),
                    "head",
                    new Vector3(0.00687F, 0.9458F, 0.01418F),
                    new Vector3(290.516F, 180F, 180F),
                    new Vector3(0.5F, 0.5F, 0.5F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["ShockNearby"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayTeslaCoil"),
                    "shoulder.R",
                    new Vector3(-0.24216F, 0.5329F, 0.18207F),
                    new Vector3(61.23521F, 77.56551F, 120.9407F),
                    new Vector3(1F, 1F, 1F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["SiphonOnLowHealth"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplaySiphonOnLowHealth"),
                    "stomach",
                    new Vector3(0.65596F, 0.42108F, -0.41182F),
                    new Vector3(351.3147F, 299.653F, 0.28759F),
                    new Vector3(0.25F, 0.25F, 0.25F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["SlowOnHit"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayBauble"),
                    "shin.R",
                    new Vector3(0.28951F, 1.31017F, 0.15899F),
                    new Vector3(37.08418F, 54.1804F, 115.7449F),
                    new Vector3(1F, 1F, 1F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["SlowOnHitVoid"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayBaubleVoid"),
                    "shin.R",
                    new Vector3(0.28951F, 1.31017F, 0.15899F),
                    new Vector3(37.08418F, 54.18039F, 115.7449F),
                    new Vector3(1F, 1F, 1F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["SprintArmor"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayBuckler"),
                    "shin.R",
                    new Vector3(0.28729F, 1.35729F, 0.17945F),
                    new Vector3(0F, 83.44807F, 0F),
                    new Vector3(0.7F, 0.7F, 0.7F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["SprintBonus"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplaySoda"),
                    "thigh.L",
                    new Vector3(0.25979F, 0.27777F, -0.68095F),
                    new Vector3(274.64F, 67.08646F, 55.4657F),
                    new Vector3(0.875F, 0.875F, 0.875F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["SprintOutOfCombat"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayWhip"),
                    "thigh.L",
                    new Vector3(-0.38613F, 1.24039F, -0.0691F),
                    new Vector3(359.6162F, 16.09659F, 182.2861F),
                    new Vector3(0.8F, 0.8F, 0.8F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["SprintWisp"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayBrokenMask"),
                    "shin.R",
                    new Vector3(-0.02779F, 0.08175F, -0.25332F),
                    new Vector3(17.69007F, 180.1609F, 178.6926F),
                    new Vector3(0.8F, 0.8F, 0.63181F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Squid"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplaySquidTurret"),
                    "thigh.L",
                    new Vector3(0.14642F, 0.91433F, -0.48298F),
                    new Vector3(271.6796F, -0.00023F, 332.4403F),
                    new Vector3(0.15F, 0.15F, 0.15F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["StickyBomb"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayStickyBomb"),
                    "thigh.R",
                    new Vector3(-0.1505F, 1.04302F, -0.45571F),
                    new Vector3(5.71665F, 2.98507F, 351.4571F),
                    new Vector3(1F, 1F, 1F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["StrengthenBurn"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayGasTank"),
                    "thigh.L",
                    new Vector3(-0.09882F, 1.46846F, 0.2569F),
                    new Vector3(357.0201F, 294.8174F, 188.0716F),
                    new Vector3(0.5F, 0.5F, 0.5F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["StunChanceOnHit"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayStunGrenade"),
                    "shin.R",
                    new Vector3(-0.36811F, 1.08144F, -0.10426F),
                    new Vector3(78.3979F, 327.2696F, 254.0192F),
                    new Vector3(3F, 3F, 3F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Talisman"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayTalisman"),
                    "Chest",
                    new Vector3(1.34863F, 2.87548F, -0.15912F),
                    new Vector3(0F, 0F, 0F),
                    new Vector3(0.2F, 0.2F, 0.2F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Thorns"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayRazorwireLeft"),
                    "shin.R",
                    new Vector3(-0.03439F, 1.22263F, 0.14297F),
                    new Vector3(284.8663F, 231.2365F, 126.4213F),
                    new Vector3(2F, 2F, 1.25F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["TitanGoldDuringTP"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayGoldHeart"),
                    "shin.L",
                    new Vector3(-0.23813F, 1.43118F, 0.18721F),
                    new Vector3(13.74904F, 310.217F, 32.36697F),
                    new Vector3(0.85F, 0.85F, 0.85F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Tooth"],
                //ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayToothNecklaceDecal"),
                //    "Chest",
                //    new Vector3(2, 2, 2),
                //    new Vector3(0, 0, 0),
                //    new Vector3(1, 1, 1)
                //    ), //this thing fucking sucks
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayToothMeshLarge"),
                    "head",
                    new Vector3(0.01039F, 0.15582F, 0.56619F),
                    new Vector3(19.15273F, 0F, 0F),
                    new Vector3(3F, 3F, 3F)
                    ),
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayToothMeshSmall1"),
                    "head",
                    new Vector3(0.14418F, 0.13168F, 0.52651F),
                    new Vector3(0F, 43.02681F, 17.55604F),
                    new Vector3(2F, 2F, 2F)
                    ),
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayToothMeshSmall2"),
                    "head",
                    new Vector3(0.09316F, 0.10928F, 0.56744F),
                    new Vector3(1.04728F, 26.68753F, 12.315F),
                    new Vector3(2F, 2F, 2F)
                    ),
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayToothMeshSmall2"),
                    "head",
                    new Vector3(-0.08597F, 0.11618F, 0.56146F),
                    new Vector3(1.04728F, 333.3125F, 347.685F),
                    new Vector3(2F, 2F, 2F)
                    ),
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayToothMeshSmall1"),
                    "head",
                    new Vector3(-0.14418F, 0.13168F, 0.52651F),
                    new Vector3(0F, 316.9732F, 347.685F),
                    new Vector3(2F, 2F, 2F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["TPHealingNova"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayGlowFlower"),
                    "head",
                    new Vector3(0.24864F, 0.93096F, -0.5411F),
                    new Vector3(347.4423F, 147.1683F, 16.78338F),
                    new Vector3(1.1F, 1.1F, 1.1F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["TreasureCache"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayKey"),
                    "foot.r",
                    new Vector3(0.05331F, 0.93064F, -0.45256F),
                    new Vector3(19.68071F, 174.8401F, 268.0032F),
                    new Vector3(4F, 4F, 4F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["TreasureCacheVoid"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayKeyVoid"),
                    "foot.r",
                    new Vector3(0.05385F, 0.97301F, -0.47731F),
                    new Vector3(22.22432F, 174.7443F, 267.9689F),
                    new Vector3(4F, 4F, 4F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["UtilitySkillMagazine"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayAfterburnerShoulderRing"),
                    "shin.R",
                    new Vector3(-0.00961F, 0.94622F, 0.27915F),
                    new Vector3(274.5694F, 180F, 180F),
                    new Vector3(2F, 2F, 2F)
                    ),
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayAfterburnerShoulderRing"),
                    "shin.L",
                    new Vector3(-0.00961F, 0.94622F, 0.27915F),
                    new Vector3(274.5694F, 180F, 180F),
                    new Vector3(2F, 2F, 2F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["VoidMegaCrabItem"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayMegaCrabItem"),
                    "stomach",
                    new Vector3(-0.00104F, -0.15371F, 0.51196F),
                    new Vector3(0F, 0F, 355.0366F),
                    new Vector3(0.675F, 0.675F, 0.675F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["WarCryOnMultiKill"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayPauldron"),
                    "shin.L",
                    new Vector3(0.00096F, -0.0078F, -0.39098F),
                    new Vector3(82.47963F, 170.6424F, 347.5049F),
                    new Vector3(2.5F, 2.5F, 2.5F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["WardOnLevel"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayWarbanner"),
                    "stomach",
                    new Vector3(-0.04838F, -0.01158F, -0.8222F),
                    new Vector3(279.571F, 60.73092F, 208.9278F),
                    new Vector3(1F, 1F, 1F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["BFG"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayBFG"),
                    "shoulder.L",
                    new Vector3(0.1987F, 1.03695F, 0.16485F),
                    new Vector3(29.95523F, 276.1305F, 298.4904F),
                    new Vector3(0.7F, 0.7F, 0.7F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Blackhole"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayGravCube"),
                    "Chest",
                    new Vector3(2.64334F, 2.62523F, 0.29092F),
                    new Vector3(0F, 0F, 0F),
                    new Vector3(0.5F, 0.5F, 0.5F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["BossHunter"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayTricornGhost"),
                    "head",
                    new Vector3(0.00002F, 1.46286F, -0.29403F),
                    new Vector3(5.39101F, 0F, 0F),
                    new Vector3(1.4F, 1.4F, 1.4F)
                    ),
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayBlunderbuss"),
                    "Chest",
                    new Vector3(-3.54654F, 0.33578F, 0.64506F),
                    new Vector3(70.97169F, 206.2616F, 180F),
                    new Vector3(0.6F, 0.6F, 0.6F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["BossHunterConsumed"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayTricornUsed"),
                    "head",
                    new Vector3(0.00002F, 1.46286F, -0.29403F),
                    new Vector3(5.39101F, 0F, 0F),
                    new Vector3(1.4F, 1.4F, 1.4F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["BurnNearby"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayPotion"),
                    "stomach",
                    new Vector3(-0.60792F, -0.12229F, -0.68656F),
                    new Vector3(2.58112F, 24.69919F, 310.1623F),
                    new Vector3(0.125F, 0.125F, 0.125F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Cleanse"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayWaterPack"),
                    "upper_arm.L",
                    new Vector3(0.04446F, 0.47534F, 0.30709F),
                    new Vector3(340.8037F, 26.28072F, 188.1407F),
                    new Vector3(0.125F, 0.125F, 0.125F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["CommandMissile"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayMissileRack"),
                    "forearm.L",
                    new Vector3(0.00574F, 0.65323F, 0.21374F),
                    new Vector3(79.07755F, 341.1386F, 340.3347F),
                    new Vector3(1F, 1F, 1F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["CrippleWard"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayEffigy"),
                    "thigh.L",
                    new Vector3(0.27355F, 1.89511F, -0.31277F),
                    new Vector3(12.36601F, 341.1104F, 180.4509F),
                    new Vector3(1F, 1F, 1F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["CritOnUse"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayNeuralImplant"),
                    "head",
                    new Vector3(0.00003F, 0.62143F, 0.90571F),
                    new Vector3(0F, 0F, 0F),
                    new Vector3(0.8F, 0.8F, 0.8F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["DroneBackup"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayRadio"),
                    "shoulder.L",
                    new Vector3(0.31203F, 1.04174F, 0.12021F),
                    new Vector3(344.0619F, 57.70744F, 355.7838F),
                    new Vector3(1.1F, 1.1F, 1.1F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["EliteEarthEquipment"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayEliteMendingAntlers"),
                    "head",
                    new Vector3(0.00001F, 1.03261F, 0.00472F),
                    new Vector3(0F, 180F, 0F),
                    new Vector3(1.75F, 1.75F, 1.75F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["EliteFireEquipment"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayEliteHorn"),
                    "head",
                    new Vector3(0.04583F, 1.20131F, 0.01445F),
                    new Vector3(0F, 346.3451F, 0F),
                    new Vector3(0.25F, 0.25F, 0.25F)
                    ),
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayEliteHorn"),
                    "head",
                    new Vector3(-0.04583F, 1.20131F, 0.01445F),
                    new Vector3(0F, 13.6549F, 0F),
                    new Vector3(-0.25F, 0.25F, 0.25F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["EliteHauntedEquipment"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayEliteStealthCrown"),
                    "head",
                    new Vector3(0.00001F, 1.25381F, -0.10635F),
                    new Vector3(284.0021F, 180F, 180F),
                    new Vector3(0.165F, 0.165F, 0.165F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["EliteIceEquipment"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayEliteIceCrown"),
                    "head",
                    new Vector3(0.00001F, 1.20568F, -0.15119F),
                    new Vector3(277.1487F, 180F, 180F),
                    new Vector3(0.1F, 0.1F, 0.1F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["EliteLightningEquipment"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayEliteRhinoHorn"),
                    "head",
                    new Vector3(0.00002F, 1.02527F, 0.19144F),
                    new Vector3(295.0447F, 0F, 0F),
                    new Vector3(1F, 1F, 1F)
                    ),
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayEliteRhinoHorn"),
                    "head",
                    new Vector3(0.00002F, 1.20716F, -0.04816F),
                    new Vector3(285.0408F, 0F, 0F),
                    new Vector3(0.7F, 0.7F, 0.7F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["EliteLunarEquipment"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayEliteLunar,Eye"),
                    "head",
                    new Vector3(0.00001F, 0.71273F, 0.63883F),
                    new Vector3(0F, 0F, 0F),
                    new Vector3(0.9F, 0.9F, 0.9F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["ElitePoisonEquipment"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayEliteUrchinCrown"),
                    "head",
                    new Vector3(0.00001F, 0.77489F, 0.00011F),
                    new Vector3(0F, 0F, 0F),
                    new Vector3(0.2F, 0.2F, 0.2F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["EliteVoidEquipment"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayAffixVoid"),
                    "neck",
                    new Vector3(0.03726F, -0.13889F, 0.61555F),
                    new Vector3(25.46804F, 0F, 4.54309F),
                    new Vector3(0.7F, 0.7F, 0.7F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["FireBallDash"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayEgg"),
                    "shin.L",
                    new Vector3(0.48676F, -0.64164F, 0.27732F),
                    new Vector3(72.74002F, 0F, 0F),
                    new Vector3(0.85F, 0.85F, 0.85F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Fruit"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayFruit"),
                    "shoulder.L",
                    new Vector3(0.21429F, 0.95567F, -0.68871F),
                    new Vector3(30.72727F, 94.35963F, 119.3413F),
                    new Vector3(0.7F, 0.7F, 0.7F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["GainArmor"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayElephantFigure"),
                    "thigh.L",
                    new Vector3(0.53747F, 0.98106F, 0.32533F),
                    new Vector3(273.2013F, 276.0268F, 312.6279F),
                    new Vector3(1.75F, 1.75F, 1.75F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Gateway"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayVase"),
                    "thigh.R",
                    new Vector3(0.08529F, 0.48019F, 0.37073F),
                    new Vector3(82.21149F, 22.62072F, 353.1722F),
                    new Vector3(0.5F, 0.5F, 0.5F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["GoldGat"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayGoldGat"),
                    "upper_arm.L",
                    new Vector3(0.60858F, 0.27279F, 0.01153F),
                    new Vector3(350.5862F, 280.5148F, 141.0066F),
                    new Vector3(0.3F, 0.3F, 0.3F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["GummyClone"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayGummyClone"),
                    "stomach",
                    new Vector3(-0.6286F, 0.33621F, 0.74915F),
                    new Vector3(0F, 0F, 349.259F),
                    new Vector3(0.8F, 0.8F, 0.8F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["IrradiatingLaser"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayIrradiatingLaser"),
                    "forearm.L",
                    new Vector3(-0.03734F, 0.01932F, 0.15731F),
                    new Vector3(280.8947F, 8.76038F, 170.77F),
                    new Vector3(0.4F, 0.4F, 0.4F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Jetpack"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayBugWings"),
                    "Chest",
                    new Vector3(0F, 0.77635F, -0.00003F),
                    new Vector3(0F, 0F, 0F),
                    new Vector3(0.6F, 0.6F, 0.6F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["LifestealOnHit"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayLifestealOnHit"),
                    "head",
                    new Vector3(-0.27639F, 1.08228F, -0.51577F),
                    new Vector3(19.54163F, 14.68903F, 352.8665F),
                    new Vector3(0.2F, 0.2F, 0.2F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["LunarPortalOnUse"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayLunarPortalOnUse"),
                    "Chest",
                    new Vector3(2.46326F, 1.99926F, 0.27532F),
                    new Vector3(0F, 0F, 0F),
                    new Vector3(0.5F, 0.5F, 0.5F) //i love this silly guy. i wish he had been implemented.
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Meteor"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayMeteor"),
                    "Chest",
                    new Vector3(2.07035F, 2.27864F, -1.00133F),
                    new Vector3(0F, 0F, 0F),
                    new Vector3(0.4F, 0.4F, 0.4F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Molotov"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayMolotov"),
                    "stomach",
                    new Vector3(0.64823F, -0.19208F, 0.38708F),
                    new Vector3(356.1169F, 59.59679F, 5.29476F),
                    new Vector3(0.6F, 0.6F, 0.6F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["MultiShopCard"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayExecutiveCard"),
                    "shoulder.L",
                    new Vector3(-0.00074F, 0.03669F, 0.4548F),
                    new Vector3(71.34967F, 8.96146F, 18.50891F),
                    new Vector3(1.5F, 1.5F, 1.5F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["QuestVolatileBattery"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayBatteryArray"),
                    "stomach",
                    new Vector3(-0.76146F, 0.04714F, 0.44305F),
                    new Vector3(342.2057F, 320.9873F, 339.3327F),
                    new Vector3(0.45F, 0.45F, 0.45F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Recycle"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayRecycler"),
                    "shin.R",
                    new Vector3(0.11373F, 0.41957F, 0.47255F),
                    new Vector3(351.6469F, 115.0603F, 194.922F),
                    new Vector3(0.15F, 0.15F, 0.15F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Saw"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplaySawmerangFollower"),
                    "Chest",
                    new Vector3(-1.99846F, 2.99339F, -1.14752F),
                    new Vector3(90F, 0F, 0F),
                    new Vector3(0.4F, 0.4F, 0.4F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Scanner"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayScanner"),
                    "upper_arm.L",
                    new Vector3(0.05048F, 1.09249F, -0.15948F),
                    new Vector3(4.99235F, 106.0927F, 258.518F),
                    new Vector3(0.5F, 0.5F, 0.5F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["TeamWarCry"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayTeamWarCry"),
                    "stomach",
                    new Vector3(0.00004F, 0.72755F, -0.92141F),
                    new Vector3(16.97928F, 180.8235F, 6.44034F),
                    new Vector3(0.25F, 0.25F, 0.25F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["Tonic"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayTonic"),
                    "stomach",
                    new Vector3(-0.57261F, -0.02783F, -0.68254F),
                    new Vector3(8.2565F, 29.22128F, 349.5754F),
                    new Vector3(0.6F, 0.6F, 0.6F)
                    )
                ));
            itemDisplayRules.Add(ItemDisplays.CreateDisplayRuleGroupWithRules(ItemDisplays.KeyAssets["VendingMachine"],
                ItemDisplays.CreateDisplayRule(ItemDisplays.LoadDisplay("DisplayVendingMachine"),
                    "shin.L",
                    new Vector3(0.37163F, -0.91294F, -0.11255F),
                    new Vector3(343.1044F, 335.5036F, 188.1486F),
                    new Vector3(0.5F, 0.5F, 0.5F)
                    )
                ));
            #endregion the rest
        }

        private GameObject GetPrefab(string path)
        {
            return Addressables.LoadAssetAsync<GameObject>(path).WaitForCompletion();
        }
    }
}
