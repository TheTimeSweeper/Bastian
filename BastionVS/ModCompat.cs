using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using RoR2;
using RoR2.Skills;
using System.Runtime.CompilerServices;

namespace Bastian
{
    public static class ModCompat
    {
        private static bool? emoteEnabled;

        public static bool EmoteAPIEnabled
        {
            get
            {
                if (emoteEnabled == null)
                {
                    emoteEnabled = BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.weliveinasociety.CustomEmotesAPI");
                }
                return (bool)emoteEnabled;
            }
        }

        public static bool ROOEnabled
        {
            get
            {
                return BepInEx.Bootstrap.Chainloader.PluginInfos.ContainsKey("com.rune580.riskofoptions");
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void InitEmoteSupport()
        {
            EmotesAPI.CustomEmotesAPI.ImportArmature(MainPlugin.characterPrefab, Asset.MainAssetBundle.LoadAsset<GameObject>("BastianMeme"), true);
        }

        [MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
        public static void InitROOSupport(UnityEngine.Sprite sprite)
        {
            RiskOfOptions.ModSettingsManager.SetModIcon(sprite);

            RiskOfOptions.ModSettingsManager.SetModDescriptionToken("BASTIAN_ROO_DESCRIPTION");
        }
    }
}