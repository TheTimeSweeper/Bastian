using HG;
using RoR2;
using RoR2.ContentManagement;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

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
            List<ItemDisplayRuleSet.KeyAssetRuleGroup> itemDisplayRules = new List<ItemDisplayRuleSet.KeyAssetRuleGroup>();

            itemDisplayRules.Add(BastianItemDisplays.CreateDisplayRuleGroupWithRules(RoR2Content.Items.Syringe,
                BastianItemDisplays.CreateDisplayRule(GetPrefab("RoR2/Base/Syringe/DisplaySyringeCluster.prefab"),
                    "Chest",
                    new Vector3(0.65914F, 0.09031F, -0.44892F),
                    new Vector3(293.2744F, 44.20782F, 262.8014F),
                    new Vector3(0.88676F, 0.88676F, 0.88676F)
                    )
                ));

            itemDisplayRules.Add(BastianItemDisplays.CreateDisplayRuleGroupWithRules(RoR2Content.Items.SecondarySkillMagazine,
                BastianItemDisplays.CreateDisplayRule(GetPrefab("RoR2/Base/SecondarySkillMagazine/DisplayDoubleMag.prefab"),
                    "forearm.R",
                    new Vector3(-0.30973F, 1.15523F, -0.92675F),
                    new Vector3(314.3555F, 353.8904F, 22.50784F),
                    new Vector3(0.35241F, 0.35241F, 0.35241F)
                    )
                ));

            itemDisplayRules.Add(BastianItemDisplays.CreateDisplayRuleGroupWithRules(RoR2Content.Items.Bear,
                BastianItemDisplays.CreateDisplayRule(GetPrefab("RoR2/Base/Bear/DisplayBear.prefab"),
                    "Chest",
                    new Vector3(0.00002F, 0.84445F, -1.07513F),
                    new Vector3(351.5264F, 183.8089F, 0F),
                    new Vector3(1F, 1F, 1F)
                    )
                ));

            itemDisplayRules.Add(BastianItemDisplays.CreateDisplayRuleGroupWithRules(DLC1Content.Items.BearVoid,
                BastianItemDisplays.CreateDisplayRule(GetPrefab("RoR2/DLC1/BearVoid/DisplayBearVoid.prefab"),
                    "Chest",
                    new Vector3(0.00002F, 0.84445F, -1.07513F),
                    new Vector3(351.5264F, 183.8089F, 0F),
                    new Vector3(1F, 1F, 1F)
                    )
                ));

            itemDisplayRules.Add(BastianItemDisplays.CreateDisplayRuleGroupWithRules(RoR2Content.Items.CritGlasses,
                BastianItemDisplays.CreateDisplayRule(GetPrefab("RoR2/Base/CritGlasses/DisplayGlasses.prefab"),
                    "head",
                    new Vector3(0.00001F, 0.89782F, 0.53514F),
                    new Vector3(0F, 0F, 0F),
                    new Vector3(1.1096F, 1.1412F, 0.34684F)
                    )
                ));

            itemDisplayRules.Add(BastianItemDisplays.CreateDisplayRuleGroupWithRules(DLC1Content.Items.CritGlassesVoid,
                BastianItemDisplays.CreateDisplayRule(GetPrefab("RoR2/DLC1/CritGlassesVoid/DisplayGlassesVoid.prefab"),
                    "head",
                    new Vector3(0.00001F, 0.89782F, 0.53514F),
                    new Vector3(0F, 0F, 0F),
                    new Vector3(1.1096F, 1.1412F, 0.34684F)
                    )
                ));

            itemDisplayRuleSet.keyAssetRuleGroups = itemDisplayRules.ToArray();
        }

        private GameObject GetPrefab(string path)
        {
            return Addressables.LoadAssetAsync<GameObject>(path).WaitForCompletion();
        }

        #region add rule helpers

        public static ItemDisplayRuleSet.KeyAssetRuleGroup CreateDisplayRuleGroupWithRules(Object keyAsset_, params ItemDisplayRule[] rules)
        {
            if (keyAsset_ == null)
                Debug.LogError("could not find keyasset for bastian item display");

            return new ItemDisplayRuleSet.KeyAssetRuleGroup
            {
                keyAsset = keyAsset_,
                displayRuleGroup = new DisplayRuleGroup
                {
                    rules = rules
                }
            };
        }

        public static ItemDisplayRule CreateDisplayRule(GameObject itemPrefab, string childName, Vector3 position, Vector3 rotation, Vector3 scale)
        {
            return new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.ParentedPrefab,
                childName = childName,
                followerPrefab = itemPrefab,
                limbMask = LimbFlags.None,
                localPos = position,
                localAngles = rotation,
                localScale = scale
            };
        }
        public static ItemDisplayRule CreateLimbMaskDisplayRule(LimbFlags limb)
        {
            return new ItemDisplayRule
            {
                ruleType = ItemDisplayRuleType.LimbMask,
                limbMask = limb,
                childName = "",
                followerPrefab = null
                //localPos = Vector3.zero,
                //localAngles = Vector3.zero,
                //localScale = Vector3.zero
            };
        }

        #endregion add rule helpers
    }
}
