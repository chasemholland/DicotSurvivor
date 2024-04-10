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
        {"CommonNitrogen", "NITROGEN - Increase your movement speed by "},
        {"CommonPhosphorus", "PHOSPHORUS - Decrease seed shooter cooldown by "},
        {"CommonPotassium", "POTASSIUM - Increase your max health by "},
        {"CommonZinc", "ZINC - Increase seed projectile movement speed by "},
        {"CommonIron", "IRON - Increase seed projectile damage by "},
        {"CommonCopper", "COPPER - Increase your critical hit chance by "},
        {"UncommonNitrogen", "NITROGEN - Increase your movement speed by "},
        {"UncommonPhosphorus", "PHOSPHORUS - Decrease seed shooter cooldown by "},
        {"UncommonPotassium", "POTASSIUM - Increase your max health by "},
        {"UncommonZinc", "ZINC - Increase seed projectile movement speed by "},
        {"UncommonIron", "IRON - Increase seed projectile damage by "},
        {"UncommonCopper", "COPPER - Increase your critical hit chance by "},
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

        // Set up dictionary after utils has been initialized
        ActiveModifiers.Add("MoveSpeedMod", 0);
        ActiveModifiers.Add("ShootCooldownMod", 0);
        ActiveModifiers.Add("MaxHealthMod", 0);
        ActiveModifiers.Add("SeedSpeedMod", 0);
        ActiveModifiers.Add("DamageMod", 0);
        ActiveModifiers.Add("CritChanceMod", 0);
    }
}
