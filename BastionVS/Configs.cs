using Bastian.Modules;
using System.Runtime.CompilerServices;

namespace Bastian
{
    public class Configs
    {
        public static ConfigEntry<bool> Personality;

        public static ConfigEntry<float> M1_Damage;
        public static ConfigEntry<float> M1_Duration;

        public static ConfigEntry<float> M2_Damage;
        public static ConfigEntry<float> M2_Duration;

        public static ConfigEntry<float> M3_Duration;

        public static ConfigEntry<float> M4_Charge_Multiplier;
        public static ConfigEntry<float> M4_Min_Damage;
        public static ConfigEntry<float> M4_Max_Damage;
        public static ConfigEntry<float> M4_Blast_Radius;
        public static ConfigEntry<float> M4_Regen;
        public static ConfigEntry<float> M4_Health_Cost;
        public static ConfigEntry<float> M4_Charge_Decay;
        public static ConfigEntry<float> M4_Charge_Decay_Delay;

        public static string SectionGeneral = "0. General";
        public static string SectionBody = "1. Bastian Body";
        public static string SectionSkills = "2. Bastian Skills";

        public static void InitGeneral()
        {
            Personality = Config.BindAndOptions(
                SectionGeneral,
                "Personality",
                false,
                "Sets the name to Bastian, and adds voice lines in lobby",
                true);
        }

        public static void InitSkills()
        {
            M1_Damage = Config.BindAndOptions(
                SectionSkills,
                "M1_Damage",
                2.5f,
                0,
                20,
                "");

            M1_Duration = Config.BindAndOptions(
                SectionSkills,
                "M1_Duration",
                0.69f,
                0,
                20,
                "");

            M2_Damage = Config.BindAndOptions(
                SectionSkills,
                "M2_Damage",
                2.5f,
                0,
                20,
                "");
            M2_Duration = Config.BindAndOptions(
                SectionSkills,
                "M2_Duration",
                1.0f,
                0,
                20,
                "");

            M3_Duration = Config.BindAndOptions(
                SectionSkills,
                "M3_Duration",
                0.169f,
                0,
                2,
                "aka distance");

            M4_Charge_Multiplier = Config.BindAndOptions(
                SectionSkills,
                "M4_Charge_Multiplier",
                0.4f,
                0,
                20,
                "");

            M4_Min_Damage = Config.BindAndOptions(
                SectionSkills,
                "M4_Min_Damage",
                10.0f,
                0,
                100,
                "");

            M4_Max_Damage = Config.BindAndOptions(
                SectionSkills,
                "M4_Max_Damage",
                60.0f,
                0,
                100,
                "");

            M4_Blast_Radius = Config.BindAndOptions(
                SectionSkills,
                "M4_Blast_Radius",
                40.0f,
                0,
                100,
                "");

            M4_Health_Cost = Config.BindAndOptions(
                SectionSkills,
                "M4_Health_Cost",
                0.33f,
                0,
                1,
                "% of health cost to use this move");

            M4_Regen = Config.BindAndOptions(
                SectionSkills,
                "M4_Regen",
                0.1f,
                0,
                3,
                "regen after expending your health");

            M4_Charge_Decay = Config.BindAndOptions(
                SectionSkills,
                "M4_Charge_Decay",
                5f,
                0,
                20,
                "amount of charge deceayed per second");

            M4_Charge_Decay_Delay = Config.BindAndOptions(
                SectionSkills,
                "M4_Charge_Decay_Delay",
                5f,
                0,
                20,
                "delay between gaining charges that they start to decay");
        }
    }
}