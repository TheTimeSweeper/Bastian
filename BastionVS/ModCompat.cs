using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using RoR2;
using RoR2.Skills;

namespace Bastion
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
        public static void AddEmoteSupport()
        {
            EmotesAPI.CustomEmotesAPI.ImportArmature(MainPlugin.characterPrefab, Assets.MainAssetBundle.LoadAsset<GameObject>("BastianMeme"), true);
        }
    }
}