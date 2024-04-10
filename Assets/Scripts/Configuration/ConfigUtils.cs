using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Provides access to config data
    /// </summary>
public static class ConfigUtils
{
    #region Fields

    static ConfigData configData;

    #endregion

    #region Properties

    /// <summary>
    /// Heart value
    /// </summary>
    public static float Heart
    {
        get { return configData.Heart; }
    }

    /// <summary>
    /// Pickup range value
    /// </summary>
    public static float PickupRange
    {
        get { return configData.PickupRange; }
    }

    /// <summary>
    /// Bronze coin value
    /// </summary>
    public static float BronzeCoin
    {
        get { return configData.BronzeCoin; }
    }

    /// <summary>
    /// Bronze coin stack value
    /// </summary>
    public static float BronzeCoinStack
    {
        get { return configData.BronzeCoinStack; }
    }

    /// <summary>
    /// Bronze coin bag value
    /// </summary>
    public static float BronzeCoinBag
    {
        get { return configData.BronzeCoinBag; }
    }

    /// <summary>
    /// Silver coin value
    /// </summary>
    public static float SilverCoin
    {
        get { return configData.SilverCoin; }
    }

    /// <summary>
    /// Silver coin stack value
    /// </summary>
    public static float SilverCoinStack
    {
        get { return configData.SilverCoinStack; }
    }

    /// <summary>
    /// Silver coin bag value
    /// </summary>
    public static float SilverCoinBag
    {
        get { return configData.SilverCoinBag; }
    }

    /// <summary>
    /// Gold coin value
    /// </summary>
    public static float GoldCoin
    {
        get { return configData.GoldCoin; }
    }

    /// <summary>
    /// Gold coin stack value
    /// </summary>
    public static float GoldCoinStack
    {
        get { return configData.GoldCoinStack; }
    }

    /// <summary>
    /// Gold coin bag value
    /// </summary>
    public static float GoldCoinBag
    {
        get { return configData.GoldCoinBag; }
    }

    /// <summary>
    /// Player health value
    /// </summary>
    public static float PlayerHealth
    {
        get { return configData.PlayerHealth; }
    }

    /// <summary>
    /// Player max health
    /// </summary>
    public static float PlayerMaxHealth
    {
        get { return configData.PlayerMaxHealth; }
    }

    /// <summary>
    /// Player damage value
    /// </summary>
    public static float PlayerDamage
    {
        get { return configData.PlayerDamage; }
    }

    /// <summary>
    /// Player crit chance value
    /// </summary>
    public static float PlayerCritChance
    {
        get { return configData.PlayerCritChance; }
    }

    /// <summary>
    /// Player move speed value
    /// </summary>
    public static float PlayerMoveSpeed
    {
        get { return configData.PlayerMoveSpeed; }
    }

    /// <summary>
    /// Player seed speed value
    /// </summary>
    public static float PlayerSeedSpeed
    {
        get { return configData.PlayerSeedSpeed; }
    }

    /// <summary>
    /// Player shoot cooldown value
    /// </summary>
    public static float PlayerShootCooldown
    {
        get { return configData.PlayerShootCooldown; }
    }

    /// <summary>
    /// Common move speed mod value
    /// </summary>
    public static float ComMoveSpeedMod
    {
        get { return configData.ComMoveSpeedMod; }
    }

    /// <summary>
    /// Uncommon move speed mod value
    /// </summary>
    public static float UncMoveSpeedMod
    {
        get { return configData.UncMoveSpeedMod; }
    }

    /// <summary>
    /// Common shoot cooldown mod value
    /// </summary>
    public static float ComShootCooldownMod
    {
        get { return configData.ComShootCooldownMod; }
    }

    /// <summary>
    /// Uncommon shoot cooldown mod value
    /// </summary>
    public static float UncShootCooldownMod
    {
        get { return configData.UncShootCooldownMod; }
    }

    /// <summary>
    /// Common max health mod value
    /// </summary>
    public static float ComMaxHealthMod
    {
        get { return configData.ComMaxHealthMod; }
    }

    /// <summary>
    /// Uncommon max health mod value
    /// </summary>
    public static float UncMaxHealthMod
    {
        get { return configData.UncMaxHealthMod; }
    }

    /// <summary>
    /// Common seed speed mod value
    /// </summary>
    public static float ComSeedSpeedMod
    {
        get { return configData.ComSeedSpeedMod; }
    }

    /// <summary>
    /// Uncommon seed speed mod value
    /// </summary>
    public static float UncSeedSpeedMod
    {
        get { return configData.UncSeedSpeedMod; }
    }

    /// <summary>
    /// Common crit chance mod value
    /// </summary>
    public static float ComCritChanceMod
    {
        get { return configData.ComCritChanceMod; }
    }

    /// <summary>
    /// Uncommon crit chance mod value
    /// </summary>
    public static float UncCritChanceMod
    {
        get { return configData.UncCritChanceMod; }
    }

    /// <summary>
    /// Common damage mod value
    /// </summary>
    public static float ComDamageMod
    {
        get { return configData.ComDamageMod; }
    }

    /// <summary>
    /// Uncommon damage mod value
    /// </summary>
    public static float UncDamageMod
    {
        get { return configData.UncDamageMod; }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Initialize the configuration data
    /// </summary>
    public static void Initialize()
    {
        configData = new ConfigData();
    }

    #endregion
}
