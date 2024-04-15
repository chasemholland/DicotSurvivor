using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using System.Runtime.InteropServices.WindowsRuntime;

/// <summary>
/// Configuration data retrieved from csv or set to default values
/// </summary>
public class ConfigData
{
    #region Fields

    // File to load
    const string CONFIG_NAME = "SurvivorConfig.csv";

    // Dictionary to hold config data
    Dictionary<ConfigDataName, float> values = 
        new Dictionary<ConfigDataName, float>();

    #endregion

    #region Properties

    /// <summary>
    /// Gets the heart value
    /// </summary>
    public float Heart
    {
        get { return values[ConfigDataName.Heart]; }
    }

    /// <summary>
    /// Gets the pickup range
    /// </summary>
    public float PickupRange
    {
        get { return values[ConfigDataName.PickupRange]; }
    }

    /// <summary>
    /// Gets the small xp orb value
    /// </summary>
    public float SmallXpOrb
    {
        get { return values[ConfigDataName.SmallXpOrb]; }
    }

    /// <summary>
    /// Gets the medium xp orb value
    /// </summary>
    public float MediumXpOrb
    {
        get { return values[ConfigDataName.MediumXpOrb]; }
    }

    /// <summary>
    /// Gets the large xp orb value
    /// </summary>
    public float LargeXpOrb
    {
        get { return values[ConfigDataName.LargeXpOrb]; }
    }

    /// <summary>
    /// Gets the xlarge xp orb value
    /// </summary>
    public float XLargeXpOrb
    {
        get { return values[ConfigDataName.XLargeXpOrb]; }
    }

    /// <summary>
    /// Gets the xxlarge xp orb value
    /// </summary>
    public float XXLargeXpOrb
    {
        get { return values[ConfigDataName.XXLargeXpOrb]; }
    }

    /// <summary>
    /// Gets the xxxlarge xp orb value
    /// </summary>
    public float XXXLargeXpOrb
    {
        get { return values[ConfigDataName.XXXLargeXpOrb]; }
    }

    /// <summary>
    /// Gets the enemy health value
    /// </summary>
    public float EnemyHealth
    {
        get { return values[ConfigDataName.EnemyHealth]; }
    }

    /// <summary>
    /// Gets the boss health value
    /// </summary>
    public float BossHealth
    {
        get { return values[ConfigDataName.BossHealth]; }
    }

    /// <summary>
    /// Gets the king health value
    /// </summary>
    public float KingHealth
    {
        get { return values[ConfigDataName.KingHealth]; }
    }

    /// <summary>
    /// Gets the player health value
    /// </summary>
    public float PlayerHealth
    {
        get { return values[ConfigDataName.PlayerHealth]; }
    }

    /// <summary>
    /// Gets the player max health value
    /// </summary>
    public float PlayerMaxHealth
    {
        get { return values[ConfigDataName.PlayerMaxHealth]; }
    }

    /// <summary>
    /// Gets the player damage value
    /// </summary>
    public float PlayerDamage
    {
        get { return values[ConfigDataName.PlayerDamage]; }
    }

    /// <summary>
    /// Gets the player crit chance value
    /// </summary>
    public float PlayerCritChance
    {
        get { return values[ConfigDataName.PlayerCritChance]; }
    }

    /// <summary>
    /// Get the player move speed value
    /// </summary>
    public float PlayerMoveSpeed
    {
        get { return values[ConfigDataName.PlayerMoveSpeed]; }
    }

    /// <summary>
    /// Gets the player seed speed value
    /// </summary>
    public float PlayerSeedSpeed
    {
        get { return values[ConfigDataName.PlayerSeedSpeed]; }
    }

    /// <summary>
    /// Gets the player shoot cooldown value
    /// </summary>
    public float PlayerShootCooldown
    {
        get { return values[ConfigDataName.PlayerShootCooldown]; }
    }

    /// <summary>
    /// Gets the seedling health value
    /// </summary>
    public float SeedlingHealth
    {
        get { return values[ConfigDataName.SeedlingHealth]; }
    }

    /// <summary>
    /// Gets the seedling rate of fire mod
    /// </summary>
    public float SeedlingROF
    {
        get { return values[ConfigDataName.SeedlingROF]; }
    }

    /// <summary>
    /// Gets the seedling damage value
    /// </summary>
    public float SeedlingDamage
    {
        get { return values[ConfigDataName.SeedlingDamage]; }
    }

    /// <summary>
    /// Gets the thorn damage value
    /// </summary>
    public float ThornDamage
    {
        get { return values[ConfigDataName.ThornDamage]; }
    }

    /// <summary>
    /// Gets the thorn rate of fire value
    /// </summary>
    public float ThornROF
    {
        get { return values[ConfigDataName.ThornROF]; }
    }

    /// <summary>
    /// Gets the common move speed mod value
    /// </summary>
    public float ComMoveSpeedMod
    {
        get { return values[ConfigDataName.ComMoveSpeedMod]; }
    }


    /// <summary>
    /// Gets the uncommon move speed mod value
    /// </summary>
    public float UncMoveSpeedMod
    {
        get { return values[ConfigDataName.UncMoveSpeedMod]; }
    }

    /// <summary>
    /// Gets the rare move speed mod value
    /// </summary>
    public float RarMoveSpeedMod
    {
        get { return values[ConfigDataName.RarMoveSpeedMod]; }
    }

    /// <summary>
    /// Gets the common shoot cooldown mod value
    /// </summary>
    public float ComShootCooldownMod
    {
        get { return values[ConfigDataName.ComShootCooldownMod]; }
    }

    /// <summary>
    /// Gets the uncommon shoot cooldown mod value
    /// </summary>
    public float UncShootCooldownMod
    {
        get { return values[ConfigDataName.UncShootCooldownMod]; }
    }

    /// <summary>
    /// Gets the rare shoot cooldown mod value
    /// </summary>
    public float RarShootCooldownMod
    {
        get { return values[ConfigDataName.RarShootCooldownMod]; }
    }

    /// <summary>
    /// Gets the common max health mod value
    /// </summary>
    public float ComMaxHealthMod
    {
        get { return values[ConfigDataName.ComMaxHealthMod]; }
    }

    /// <summary>
    /// Gets the uncommon max health mod value
    /// </summary>
    public float UncMaxHealthMod
    {
        get { return values[ConfigDataName.UncMaxHealthMod]; }
    }

    /// <summary>
    /// Gets the rare max health mod value
    /// </summary>
    public float RarMaxHealthMod
    {
        get { return values[ConfigDataName.RarMaxHealthMod]; }
    }

    /// <summary>
    /// Gets the common seed speed mod value
    /// </summary>
    public float ComSeedSpeedMod
    {
        get { return values[ConfigDataName.ComSeedSpeedMod]; }
    }

    /// <summary>
    /// Gets the uncommon seed speed mod value
    /// </summary>
    public float UncSeedSpeedMod
    {
        get { return values[ConfigDataName.UncSeedSpeedMod]; }
    }

    /// <summary>
    /// Gets the rare seed speed mod value
    /// </summary>
    public float RarSeedSpeedMod
    {
        get { return values[ConfigDataName.RarSeedSpeedMod]; }
    }

    /// <summary>
    /// Gets the common crit chance mod value
    /// </summary>
    public float ComCritChanceMod
    {
        get { return values[ConfigDataName.ComCritChanceMod]; }
    }

    /// <summary>
    /// Gets the uncommon crit chance mod value
    /// </summary>
    public float UncCritChanceMod
    {
        get { return values[ConfigDataName.UncCritChanceMod]; }
    }

    /// <summary>
    /// Gets the rare crit chance mod value
    /// </summary>
    public float RarCritChanceMod
    {
        get { return values[ConfigDataName.RarCritChanceMod]; }
    }

    /// <summary>
    /// Gets the common damage mod value
    /// </summary>
    public float ComDamageMod
    {
        get { return values[ConfigDataName.ComDamageMod]; }
    }

    /// <summary>
    /// Gets the uncommon damage mod value
    /// </summary>
    public float UncDamageMod
    {
        get { return values[ConfigDataName.UncDamageMod]; }
    }

    /// <summary>
    /// Gets the rare damage mod value
    /// </summary>
    public float RarDamageMod
    {
        get { return values[ConfigDataName.RarDamageMod]; }
    }

    /// <summary>
    /// Gets the uncommon thorn damage mod value
    /// </summary>
    public float UncThornDamageMod
    {
        get { return values[ConfigDataName.UncThornDamageMod]; }
    }

    /// <summary>
    /// Gets the rare thorn damage mod value
    /// </summary>
    public float RarThornDamageMod
    {
        get { return values[ConfigDataName.RarThornDamageMod]; }
    }

    /// <summary>
    /// Gets the uncommon thorn rate of fire mod value
    /// </summary>
    public float UncThornROFMod
    {
        get { return values[ConfigDataName.UncThornROFMod]; }
    }

    /// <summary>
    /// Gets the rare thorn rate of fire mod value
    /// </summary>
    public float RarThornROFMod
    {
        get { return values[ConfigDataName.RarThornROFMod]; }
    }

    /// <summary>
    /// Gets the uncommon seedling damage mod value
    /// </summary>
    public float UncSeedlingDamageMod
    {
        get { return values[ConfigDataName.UncSeedlingDamageMod]; }
    }

    /// <summary>
    /// Gets the rare seedling damage mod value
    /// </summary>
    public float RarSeedlingDamageMod
    {
        get { return values[ConfigDataName.RarSeedlingDamageMod]; }
    }

    /// <summary>
    /// Get sthe uncommon seedling rate of fire mod value
    /// </summary>
    public float UncSeedlingROFMod
    {
        get { return values[ConfigDataName.UncSeedlingROFMod]; }
    }

    /// <summary>
    /// Gets the rare seedling rate of fire mod value
    /// </summary>
    public float RarSeedlingROFMod
    {
        get { return values[ConfigDataName.RarSeedlingROFMod]; }
    }

    /// <summary>
    /// Gets the uncommon seedling health mod value
    /// </summary>
    public float UncSeedlingHealthMod
    {
        get { return values[ConfigDataName.UncSeedlingHealthMod]; }
    }

    /// <summary>
    /// Gets the rare seedling health mod value
    /// </summary>
    public float RarSeedlingHealthMod
    {
        get { return values[ConfigDataName.RarSeedlingHealthMod]; }
    }

    #endregion

    #region Constructor

    public ConfigData()
    {
        // Reader for config file
        StreamReader input = null;

        // Try to retrieve values from config file
        try
        {
            // Create reader
            input = File.OpenText(Path.Combine(
                Application.streamingAssetsPath, CONFIG_NAME));

            // Fill in dictionary values
            string line = input.ReadLine();
            while (line != null)
            {
                string[] csvLine = line.Split(',');
                ConfigDataName name = (ConfigDataName)Enum.Parse(
                    typeof(ConfigDataName), csvLine[0]);
                values.Add(name, float.Parse(csvLine[1]));
                line = input.ReadLine();
            }
        }
        // Set default values if it breaks at any point
        catch (Exception ex)
        {
            // log the error
            Debug.Log("Failed to load config from csv: " + ex.Message);

            // Set default values
            SetDefaults();
        }
        finally
        {
            if (input != null)
            {
                input.Close();
            }
        }
    }

    #endregion

    #region Methods

    void SetDefaults()
    {
        // Clear all loaded data before the failure
        values.Clear();

        // Set default values
        values.Add(ConfigDataName.Heart, 1f);
        values.Add(ConfigDataName.PickupRange, 1f);
        values.Add(ConfigDataName.SmallXpOrb, 1f);
        values.Add(ConfigDataName.MediumXpOrb, 3f);
        values.Add(ConfigDataName.LargeXpOrb, 9f);
        values.Add(ConfigDataName.XLargeXpOrb, 27f);
        values.Add(ConfigDataName.XXLargeXpOrb, 81f);
        values.Add(ConfigDataName.XXXLargeXpOrb, 243f);
        values.Add(ConfigDataName.EnemyHealth, 2f);
        values.Add(ConfigDataName.BossHealth, 20f);
        values.Add(ConfigDataName.KingHealth, 100f);
        values.Add(ConfigDataName.PlayerHealth, 3f);
        values.Add(ConfigDataName.PlayerMaxHealth, 3f);
        values.Add(ConfigDataName.PlayerDamage, 1f);
        values.Add(ConfigDataName.PlayerCritChance, 0.05f);
        values.Add(ConfigDataName.PlayerMoveSpeed, 3f);
        values.Add(ConfigDataName.PlayerSeedSpeed, 8f);
        values.Add(ConfigDataName.PlayerShootCooldown, 1f);
        values.Add(ConfigDataName.SeedlingHealth, 1f);
        values.Add(ConfigDataName.SeedlingDamage, 0.5f);
        values.Add(ConfigDataName.SeedlingROF, 0.5f);
        values.Add(ConfigDataName.ThornDamage, 0.5f);
        values.Add(ConfigDataName.ThornROF, 0.5f);
        values.Add(ConfigDataName.ComMoveSpeedMod, 0.1f);
        values.Add(ConfigDataName.UncMoveSpeedMod, 0.2f);
        values.Add(ConfigDataName.RarMoveSpeedMod, 0.4f);
        values.Add(ConfigDataName.ComShootCooldownMod, 0.025f);
        values.Add(ConfigDataName.UncShootCooldownMod, 0.05f);
        values.Add(ConfigDataName.RarShootCooldownMod, 0.1f);
        values.Add(ConfigDataName.ComMaxHealthMod, 0.1f);
        values.Add(ConfigDataName.UncMaxHealthMod, 0.2f);
        values.Add(ConfigDataName.RarMaxHealthMod, 0.4f);
        values.Add(ConfigDataName.ComSeedSpeedMod, 0.05f);
        values.Add(ConfigDataName.UncSeedSpeedMod, 0.1f);
        values.Add(ConfigDataName.RarSeedSpeedMod, 0.2f);
        values.Add(ConfigDataName.ComCritChanceMod, 0.05f);
        values.Add(ConfigDataName.UncCritChanceMod, 0.1f);
        values.Add(ConfigDataName.RarCritChanceMod, 0.2f);
        values.Add(ConfigDataName.ComDamageMod, 0.1f);
        values.Add(ConfigDataName.UncDamageMod, 0.2f);
        values.Add(ConfigDataName.RarDamageMod, 0.2f);
        values.Add(ConfigDataName.UncThornDamageMod, 0.2f);
        values.Add(ConfigDataName.RarThornDamageMod, 0.4f);
        values.Add(ConfigDataName.UncThornROFMod, 0.05f);
        values.Add(ConfigDataName.RarThornDamageMod, 0.1f);
        values.Add(ConfigDataName.UncSeedlingDamageMod, 0.2f);
        values.Add(ConfigDataName.RarSeedlingDamageMod, 0.4f);
        values.Add(ConfigDataName.UncSeedlingROFMod, 0.05f);
        values.Add(ConfigDataName.RarSeedlingROFMod, 0.1f);
        values.Add(ConfigDataName.UncSeedlingHealthMod, 0.2f);
        values.Add(ConfigDataName.RarSeedlingHealthMod, 0.4f);

    }

    #endregion
}
