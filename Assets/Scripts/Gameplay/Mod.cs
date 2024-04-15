using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Class that contains strings of mod info
/// </summary>
public static class Mod
{
    /// <summary>
    /// Modifier description for info text
    /// </summary>
    public static Dictionary<string, string> Info = new Dictionary<string, string>()
    {
        {"CommonNitrogen", "NITROGEN UPTAKE - Increase your movement speed by "},
        {"CommonPhosphorus", "PHOSPHORUS UPTAKE - Decrease seed shooter cooldown by "},
        {"CommonPotassium", "POTASSIUM UPTAKE - Increase your max health by "},
        {"CommonZinc", "ZINC UPTAKE - Increase seed projectile movement speed by "},
        {"CommonIron", "IRON UPTAKE - Increase seed projectile damage by "},
        {"CommonCopper", "COPPER UPTAKE - Increase your critical hit chance by "},
        {"UncommonNitrogen", "NITROGEN UPTAKE - Increase your movement speed by "},
        {"UncommonPhosphorus", "PHOSPHORUS UPTAKE - Decrease seed shooter cooldown by "},
        {"UncommonPotassium", "POTASSIUM UPTAKE - Increase your max health by "},
        {"UncommonZinc", "ZINC UPTAKE - Increase seed projectile movement speed by "},
        {"UncommonIron", "IRON UPTAKE - Increase seed projectile damage by "},
        {"UncommonCopper", "COPPER UPTAKE - Increase your critical hit chance by "},
        {"RareNitrogen", "NITROGEN UPTAKE - Increase your movement speed by "},
        {"RarePhosphorus", "PHOSPHORUS UPTAKE - Decrease seed shooter cooldown by "},
        {"RarePotassium", "POTASSIUM UPTAKE - Increase your max health by "},
        {"RareZinc", "ZINC UPTAKE - Increase seed projectile movement speed by "},
        {"RareIron", "IRON UPTAKE - Increase seed projectile damage by "},
        {"RareCopper", "COPPER UPTAKE - Increase your critical hit chance by "},
        {"UncommonThornDamage", "THORNS ENHANCEMENT - Increase your thorns damage by "},
        {"UncommonThornROF", "THORNS ENHANCEMENT - Increase your thorns rate of fire by "},
        {"UncommonSeedlingDamage", "SEEDLING ENHANCEMENT - Increase your seedling damage by "},
        {"UncommonSeedlingROF", "SEEDLING ENHANCEMENT - Increase your seedling rate of fire by "},
        {"UncommonSeedlingHealth", "SEEDLING ENHANCEMENT - Increase your seedling health by "},
        {"RareThornDamage", "THORNS ENHANCEMENT - Increase your thorns damage by "},
        {"RareThornROF", "THORNS ENHANCEMENT - Increase your thorns rate of fire by "},
        {"RareSeedlingDamage", "SEEDLING ENHANCEMENT - Increase your seedling damage by "},
        {"RareSeedlingROF", "SEEDLING ENHANCEMENT - Increase your seedling rate of fire by "},
        {"RareSeedlingHealth", "SEEDLING ENHANCEMENT - Increase your seedling health by "},
    };

    /// <summary>
    /// Middle man map button name(key) to ActiveMod name(value)
    /// </summary>
    public static Dictionary<string, string> MiddleMan = new Dictionary<string, string>()
    {
        {"CommonNitrogen", "MoveSpeedMod"},
        {"CommonPhosphorus", "ShootCooldownMod"},
        {"CommonPotassium", "MaxHealthMod"},
        {"CommonZinc", "SeedSpeedMod"},
        {"CommonIron", "DamageMod"},
        {"CommonCopper", "CritChanceMod"},
        {"UncommonNitrogen", "MoveSpeedMod"},
        {"UncommonPhosphorus", "ShootCooldownMod"},
        {"UncommonPotassium", "MaxHealthMod"},
        {"UncommonZinc", "SeedSpeedMod"},
        {"UncommonIron", "DamageMod"},
        {"UncommonCopper", "CritChanceMod"},
        {"RareNitrogen", "MoveSpeedMod"},
        {"RarePhosphorus", "ShootCooldownMod"},
        {"RarePotassium", "MaxHealthMod"},
        {"RareZinc", "SeedSpeedMod"},
        {"RareIron", "DamageMod"},
        {"RareCopper", "CritChanceMod"},
        {"UncommonThornDamage", "ThornDamageMod"},
        {"UncommonThornROF", "ThornROFMod"},
        {"UncommonSeedlingDamage", "SeedlingDamageMod"},
        {"UncommonSeedlingROF", "SeedlingROFMod"},
        {"UncommonSeedlingHealth", "SeedlingHealthMod"},
        {"RareThornDamage", "ThornDamageMod"},
        {"RareThornROF", "ThornROFMod"},
        {"RareSeedlingDamage", "SeedlingDamageMod"},
        {"RareSeedlingROF", "SeedlingROFMod"},
        {"RareSeedlingHealth", "SeedlingHealthMod"},
    };

    /// <summary>
    /// Player active modifier values
    /// </summary>
    public static Dictionary<string, float> ActiveModifiers = new Dictionary<string, float>();

    /// <summary>
    /// Modifier value in string form
    /// </summary>
    public static Dictionary<string, string> Stat = new Dictionary<string, string>();

    /// <summary>
    /// Modifier value
    /// </summary>
    public static Dictionary<string, float> Modifier = new Dictionary<string, float>();


    /// <summary>
    /// Mutation description for info text
    /// </summary>
    public static Dictionary<string, string> MutationInfo = new Dictionary<string, string>()
    {
        {"ThornsI", "THORNS I - Gain the ability to grow 2 thorns."},
        {"ThornsII", "THORNS II - You now grow 4 thorns."},
        {"ThornsIII", "THORNS III - You now grow 6 thorns."},
        {"MultiseedI", "MULTISEED I - Gain the ability to shoot 2 seeds at once."},
        {"MultiseedII", "MULTISEED II - You now shoot 3 seeds at once."},
        {"MultiseedIII", "MULTISEED III - You now shoot 4 seeds at once."},
        {"ReproductionI", "REPRODUCTION I - Gain the ability to have 1 seedling."},
        {"ReproductionII", "REPRODUCTION II - You can now have 2 seedlings."},
        {"ReproductionIII", "REPRODUCTION III - You can now have 3 seedlings."},
    };

    /// <summary>
    /// Player active mutation values
    /// </summary>
    public static Dictionary<string, float> ActiveMutations = new Dictionary<string, float>();



    public static void Initialize()
    {
        // Set up dictionary after utils has been initialized
        Stat.Add("CommonNitrogen", (ConfigUtils.ComMoveSpeedMod * 100).ToString());
        Stat.Add("CommonPhosphorus", (ConfigUtils.ComShootCooldownMod * 100).ToString());
        Stat.Add("CommonPotassium", (ConfigUtils.ComMaxHealthMod * 100).ToString());
        Stat.Add("CommonZinc", (ConfigUtils.ComSeedSpeedMod * 100).ToString());
        Stat.Add("CommonIron", (ConfigUtils.ComDamageMod * 100).ToString());
        Stat.Add("CommonCopper", (ConfigUtils.ComCritChanceMod * 100).ToString());

        Stat.Add("UncommonNitrogen", (ConfigUtils.UncMoveSpeedMod * 100).ToString());
        Stat.Add("UncommonPhosphorus", (ConfigUtils.UncShootCooldownMod * 100).ToString());
        Stat.Add("UncommonPotassium", (ConfigUtils.UncMaxHealthMod * 100).ToString());
        Stat.Add("UncommonZinc", (ConfigUtils.UncSeedSpeedMod * 100).ToString());
        Stat.Add("UncommonIron", (ConfigUtils.UncDamageMod * 100).ToString());
        Stat.Add("UncommonCopper", (ConfigUtils.UncCritChanceMod * 100).ToString());

        Stat.Add("RareNitrogen", (ConfigUtils.RarMoveSpeedMod * 100).ToString());
        Stat.Add("RarePhosphorus", (ConfigUtils.RarShootCooldownMod * 100).ToString());
        Stat.Add("RarePotassium", (ConfigUtils.RarMaxHealthMod * 100).ToString());
        Stat.Add("RareZinc", (ConfigUtils.RarSeedSpeedMod * 100).ToString());
        Stat.Add("RareIron", (ConfigUtils.RarDamageMod * 100).ToString());
        Stat.Add("RareCopper", (ConfigUtils.RarCritChanceMod * 100).ToString());

        Stat.Add("UncommonThornDamage", (ConfigUtils.UncThornDamageMod * 100).ToString());
        Stat.Add("UncommonThornROF", (ConfigUtils.UncThornROFMod * 100).ToString());
        Stat.Add("UncommonSeedlingDamage", (ConfigUtils.UncSeedlingDamageMod * 100).ToString());
        Stat.Add("UncommonSeedlingROF", (ConfigUtils.UncSeedlingROFMod * 100).ToString());
        Stat.Add("UncommonSeedlingHealth", (ConfigUtils.UncSeedlingHealthMod * 100).ToString());

        Stat.Add("RareThornDamage", (ConfigUtils.RarThornDamageMod * 100).ToString());
        Stat.Add("RareThornROF", (ConfigUtils.RarThornROFMod * 100).ToString());
        Stat.Add("RareSeedlingDamage", (ConfigUtils.RarSeedlingDamageMod * 100).ToString());
        Stat.Add("RareSeedlingROF", (ConfigUtils.RarSeedlingROFMod * 100).ToString());
        Stat.Add("RareSeedlingHealth", (ConfigUtils.RarSeedlingHealthMod * 100).ToString());

        // Set up dictionary after utils has been initialized
        Modifier.Add("CommonNitrogen", ConfigUtils.ComMoveSpeedMod);
        Modifier.Add("CommonPhosphorus", ConfigUtils.ComShootCooldownMod);
        Modifier.Add("CommonPotassium", ConfigUtils.ComMaxHealthMod);
        Modifier.Add("CommonZinc", ConfigUtils.ComSeedSpeedMod);
        Modifier.Add("CommonIron", ConfigUtils.ComDamageMod);
        Modifier.Add("CommonCopper", ConfigUtils.ComCritChanceMod);

        Modifier.Add("UncommonNitrogen", ConfigUtils.UncMoveSpeedMod);
        Modifier.Add("UncommonPhosphorus", ConfigUtils.UncShootCooldownMod);
        Modifier.Add("UncommonPotassium", ConfigUtils.UncMaxHealthMod);
        Modifier.Add("UncommonZinc", ConfigUtils.UncSeedSpeedMod);
        Modifier.Add("UncommonIron", ConfigUtils.UncDamageMod);
        Modifier.Add("UncommonCopper", ConfigUtils.UncCritChanceMod);

        Modifier.Add("RareNitrogen", ConfigUtils.RarMoveSpeedMod);
        Modifier.Add("RarePhosphorus", ConfigUtils.RarShootCooldownMod);
        Modifier.Add("RarePotassium", ConfigUtils.RarMaxHealthMod);
        Modifier.Add("RareZinc", ConfigUtils.RarSeedSpeedMod);
        Modifier.Add("RareIron", ConfigUtils.RarDamageMod);
        Modifier.Add("RareCopper", ConfigUtils.RarCritChanceMod);

        Modifier.Add("UncommonThornDamage", ConfigUtils.UncThornDamageMod);
        Modifier.Add("UncommonThornROF", ConfigUtils.UncThornROFMod);
        Modifier.Add("UncommonSeedlingDamage", ConfigUtils.UncSeedlingDamageMod);
        Modifier.Add("UncommonSeedlingROF", ConfigUtils.UncSeedlingROFMod);
        Modifier.Add("UncommonSeedlingHealth", ConfigUtils.UncSeedlingHealthMod);

        Modifier.Add("RareThornDamage", ConfigUtils.RarThornDamageMod);
        Modifier.Add("RareThornROF", ConfigUtils.RarThornROFMod);
        Modifier.Add("RareSeedlingDamage", ConfigUtils.RarSeedlingDamageMod);
        Modifier.Add("RareSeedlingROF", ConfigUtils.RarSeedlingROFMod);
        Modifier.Add("RareSeedlingHealth", ConfigUtils.RarSeedlingHealthMod);


        // Set up dictionary on initialize
        ActiveModifiers.Add("MoveSpeedMod", 0);
        ActiveModifiers.Add("ShootCooldownMod", 0);
        ActiveModifiers.Add("MaxHealthMod", 0);
        ActiveModifiers.Add("SeedSpeedMod", 0);
        ActiveModifiers.Add("DamageMod", 0);
        ActiveModifiers.Add("CritChanceMod", 0);

        ActiveModifiers.Add("ThornDamageMod", 0);
        ActiveModifiers.Add("ThornROFMod", 0);
        ActiveModifiers.Add("SeedlingDamageMod", 0);
        ActiveModifiers.Add("SeedlingROFMod", 0);
        ActiveModifiers.Add("SeedlingHealthMod", 0);

        // Set up dictionary on initialize
        ActiveMutations.Add("Thorns", 0);
        ActiveMutations.Add("Multiseed", 0);
        ActiveMutations.Add("Reproduction", 0);
    }
}
