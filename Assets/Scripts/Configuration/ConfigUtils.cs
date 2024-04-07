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
    /// Player damage value
    /// </summary>
    public static float PlayerDamage
    {
        get { return configData.PlayerDamage; }
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
