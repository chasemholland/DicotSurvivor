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
        {"CommonNitrogen", "Increase your movement speed by "},
        {"CommonPhosphorus", "Decrease seed shooter cooldown by "},
        {"CommonPotassium", "Increase your max health by "}
    };

    /// <summary>
    /// Middle man map button name(key) to ActiveMod name(value)
    /// </summary>
    public static Dictionary<string, string> MiddleMan = new Dictionary<string, string>()
    {
        {"CommonNitrogen", "MoveSpeedMod"},
        {"CommonPhosphorus", "ShootCooldownMod"},
        {"CommonPotassium", "MaxHealthMod"}
    };

    /// <summary>
    /// Player active modifier values
    /// </summary>
    public static Dictionary<string, float> ActiveModifiers = new Dictionary<string, float>()
    {
        {"MoveSpeedMod", 0},
        {"ShootCooldownMod", 0},
        {"MaxHealthMod", 0}
    };

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
        // Set up dictionary  after utils has been initialized
        Stat.Add("CommonNitrogen", (ConfigUtils.ComMoveSpeedMod * 100).ToString());
        Stat.Add("CommonPhosphorus", (ConfigUtils.ComShootCooldownMod * 100).ToString());
        Stat.Add("CommonPotassium", (ConfigUtils.ComMaxHealthMod * 100).ToString());
        
        // Set up dictionary acter utils has been initialized
        Modifier.Add("CommonNitrogen", ConfigUtils.ComMoveSpeedMod);
        Modifier.Add("CommonPhosphorus", ConfigUtils.ComShootCooldownMod);
        Modifier.Add("CommonPotassium", ConfigUtils.ComMaxHealthMod);
    }
}
