using Bastian.Modules;

namespace Bastian
{
    public class Configs
    {
        public static ConfigEntry<bool> Personality;

        public static ConfigEntry<float> M1_Damage;
        public static ConfigEntry<float> M1_Duration;

        public static ConfigEntry<float> M2_Damage;
        public static ConfigEntry<float> M2_Duration;

        public static ConfigEntry<float> M4_Charge_Multiplier;
        public static ConfigEntry<float> M4_Min_Damage;
        public static ConfigEntry<float> M4_Max_Damage;
        public static ConfigEntry<float> M4_Blast_Radius;
        public static ConfigEntry<float> M4_Regen;
        public static ConfigEntry<float> M4_AntiGravity;
        public static ConfigEntry<float> M4_hop;

        public static string SectionGeneral = "0. General";
        public static string SectionBody = "1. Bastian Body TEST";
        public static string SectionSkills = "2. Bastian Skills TEST";

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


            M4_Charge_Multiplier = Config.BindAndOptions(
                SectionSkills,
                "M4_Charge_Multiplier",
                0.5f,
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
                30.0f,
                0,
                100,
                "");

            M4_Regen = Config.BindAndOptions(
                SectionSkills,
                "M4_Regen",
                0.5f,
                0,
                3,
                "");

            M4_hop= Config.BindAndOptions(
                SectionSkills,
                "M4_hop",
                2f,
                0,
                100,
                "");
        }
    }
}