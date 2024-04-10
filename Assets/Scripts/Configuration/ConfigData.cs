using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;

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
    /// Gets the bronze coin value
    /// </summary>
    public float BronzeCoin
    {
        get { return values[ConfigDataName.BronzeCoin]; }
    }

    /// <summary>
    /// Gets the bronze coin stack value
    /// </summary>
    public float BronzeCoinStack
    {
        get { return values[ConfigDataName.BronzeCoinStack]; }
    }

    /// <summary>
    /// Gets the broze coin bag value
    /// </summary>
    public float BronzeCoinBag
    {
        get { return values[ConfigDataName.BronzeCoinBag]; }
    }

    /// <summary>
    /// Gets the silver coin value
    /// </summary>
    public float SilverCoin
    {
        get { return values[ConfigDataName.SilverCoin]; }
    }

    /// <summary>
    /// Gets the silver coin stack value
    /// </summary>
    public float SilverCoinStack
    {
        get { return values[ConfigDataName.SilverCoinStack]; }
    }

    /// <summary>
    /// Gets the silver coin bag value
    /// </summary>
    public float SilverCoinBag
    {
        get { return values[ConfigDataName.SilverCoinBag]; }
    }

    /// <summary>
    /// Gets the gold coin value
    /// </summary>
    public float GoldCoin
    {
        get { return values[ConfigDataName.GoldCoin]; }
    }

    /// <summary>
    /// Gets the gold coin stack value
    /// </summary>
    public float GoldCoinStack
    {
        get { return values[ConfigDataName.GoldCoinStack]; }
    }

    /// <summary>
    /// Gets the gold coin bag value
    /// </summary>
    public float GoldCoinBag
    {
        get { return values[ConfigDataName.GoldCoinBag]; }
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
        values.Add(ConfigDataName.BronzeCoin, 1f);
        values.Add(ConfigDataName.BronzeCoinStack, 5f);
        values.Add(ConfigDataName.BronzeCoinBag, 25f);
        values.Add(ConfigDataName.SilverCoin, 2f);
        values.Add(ConfigDataName.SilverCoinStack, 10f);
        values.Add(ConfigDataName.SilverCoinBag, 50f);
        values.Add(ConfigDataName.GoldCoin, 3f);
        values.Add(ConfigDataName.GoldCoinStack, 15f);
        values.Add(ConfigDataName.GoldCoinBag, 75f);
        values.Add(ConfigDataName.PlayerHealth, 3f);
        values.Add(ConfigDataName.PlayerMaxHealth, 3f);
        values.Add(ConfigDataName.PlayerDamage, 1f);
        values.Add(ConfigDataName.PlayerCritChance, 0.05f);
        values.Add(ConfigDataName.PlayerMoveSpeed, 3f);
        values.Add(ConfigDataName.PlayerSeedSpeed, 8f);
        values.Add(ConfigDataName.PlayerShootCooldown, 1f);
        values.Add(ConfigDataName.ComMoveSpeedMod, 0.1f);
        values.Add(ConfigDataName.UncMoveSpeedMod, 0.2f);
        values.Add(ConfigDataName.ComShootCooldownMod, 0.025f);
        values.Add(ConfigDataName.UncShootCooldownMod, 0.05f);
        values.Add(ConfigDataName.ComMaxHealthMod, 0.1f);
        values.Add(ConfigDataName.UncMaxHealthMod, 0.2f);
        values.Add(ConfigDataName.ComSeedSpeedMod, 0.05f);
        values.Add(ConfigDataName.UncSeedSpeedMod, 0.1f);
        values.Add(ConfigDataName.ComCritChanceMod, 0.05f);
        values.Add(ConfigDataName.UncCritChanceMod, 0.1f);
        values.Add(ConfigDataName.ComDamageMod, 0.1f);
        values.Add(ConfigDataName.UncDamageMod, 0.2f);
    }

    #endregion
}
