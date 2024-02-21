using Bastian.Modules;
using R2API;
using System;

namespace Bastian
{
    public class BastianTokens
    {
        private static string SURVIVORNAMEKEY => MainPlugin.SURVIVORNAMEKEY;

        public static void Init()
        {
            string desc = "A translator gone rogue, this new and improved model seeks to improve on his older counterparts, by any means necessary." +
                "<style=cSub>\r\n\r\n< ! > Use Ion Swing to build up damage of Architect Burst."
                + Environment.NewLine +
                "<style=cSub>\r\n\r\n< ! > Micro-Bullet allows you to stay in the fray by stunning sources of impending danger."
                + Environment.NewLine +
                "<style=cSub>\r\n\r\n< ! > Nano-Spring allows you to find your way out of a packed crowd, stunning enemies to allow for a few seconds of relief."
                + Environment.NewLine +
                "<style=cSub>\r\n\r\n< ! > FOR EMERGENCIES ONLY! All Bastian Models are equipped with a \"Self - Utilized Ion Charge/ Internal Detonation for Emergencies\", dubbed the \"Architect Burst\" which will detonate the model and anything in it's surrounding area. Survival is possible, but only if action is taken IMMEDIATELY after usage.";

            string outro = "..and so he left, his faith in those he spoke for dwindling, and his sense of self blooming.";
            string fail = "..and so he vanished, a single spark among the current of change.";
            string lore = "A local robot, built to help negotiate deals with foreign planetary bodies. His original purpose was to function as a middle-man, designed to survive on planets with air and temps that a normal human couldn't withstand. " +
                "He was brought aboard the UES Contact Light as part of a backup plan, in case any negotiations for the survival of the other crew members came into play. " +
                "However, he ended up being reprogrammed for combat, despite this being a simple grab-and-go mission.";

            LanguageAPI.Add(SURVIVORNAMEKEY + "_NAME", "Translator");
            LanguageAPI.Add(SURVIVORNAMEKEY + "_NAME_EPIC", "Bastian");
            LanguageAPI.Add(SURVIVORNAMEKEY + "_DESCRIPTION", desc);
            LanguageAPI.Add(SURVIVORNAMEKEY + "_SUBTITLE", "Model 4.I.N.K.");
            LanguageAPI.Add(SURVIVORNAMEKEY + "_OUTRO", outro);
            LanguageAPI.Add(SURVIVORNAMEKEY + "_FAIL", fail);
            LanguageAPI.Add(SURVIVORNAMEKEY + "_LORE", lore);

            LanguageAPI.Add(SURVIVORNAMEKEY + "BODY_DEFAULT_SKIN_NAME", "Default");
            LanguageAPI.Add(SURVIVORNAMEKEY + "BODY_MASTERY_SKIN_NAME", "Mastery");

            LanguageAPI.Add(Tokens.GetAchievementNameToken(BastianMasteryAchievement.identifier), "Translator: Mastery");
            LanguageAPI.Add(Tokens.GetAchievementDescriptionToken(BastianMasteryAchievement.identifier), "As Translator, beat the game or obliterate on Monsoon");

            LanguageAPI.Add(SURVIVORNAMEKEY + "_M1", "Ion Swing");
            LanguageAPI.Add(SURVIVORNAMEKEY + "_M1_DESCRIPTION", $"Deliver a punch for {Modules.Tokens.DamageValueText(Configs.M1_Damage.Value)}. Builds up <style=cIsUtility>Ion Charges</style>");

            LanguageAPI.Add(SURVIVORNAMEKEY + "_M2", "Micro-Bullet");
            LanguageAPI.Add(SURVIVORNAMEKEY + "_M2_DESCRIPTION", $"Fire up to 5 rounds of {Tokens.UtilityText("stunning")} energy bullets, each dealing {Modules.Tokens.DamageValueText(Configs.M2_Damage.Value)}.");

            LanguageAPI.Add(SURVIVORNAMEKEY + "_UTIL", "Nano-Spring");
            LanguageAPI.Add(SURVIVORNAMEKEY + "_UTIL_DESCRIPTION", $"Execute a rapid dash in any given direction, {Tokens.UtilityText("stunning")} enemies in your path.");

            LanguageAPI.Add(SURVIVORNAMEKEY + "_SPEC", "Architect Burst");
            LanguageAPI.Add(SURVIVORNAMEKEY + "_SPEC_DESCRIPTION", $"Expend <style=cIsHealth>{Configs.M4_Health_Cost.Value * 100}% current health</style> and <style=cIsUtility>all Ion Charges</style> for a massive burst of {Tokens.DamageText($"{Configs.M4_Min_Damage.Value * 100}-{Configs.M4_Max_Damage.Value * 100}% damage")}. After which, you are <color=#E9AFEA>Evasive</color> for 10 seconds.");
            LanguageAPI.Add(SURVIVORNAMEKEY + "_SPEC_KEYWORD", Tokens.KeywordText("Evasive", $"Reduce damage by {Tokens.HealthText("half")}, Gain {Tokens.UtilityText("80% movement speed")}, and {Tokens.UtilityText($"{Configs.M4_Regen.Value * 100}% regen")}."));

            LanguageAPI.Add("BASTIAN_ROO_DESCRIPTION", "Bastian, the Translator, Model 4.I.N.K.");
        }
    }
}
