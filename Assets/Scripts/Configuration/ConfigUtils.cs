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
    /// Small xp orb value
    /// </summary>
    public static float SmallXpOrb
    {
        get { return configData.SmallXpOrb; }
    }

    /// <summary>
    /// Medium xp orb value
    /// </summary>
    public static float MediumXpOrb
    {
        get { return configData.MediumXpOrb; }
    }

    /// <summary>
    /// Large xp orb value
    /// </summary>
    public static float LargeXpOrb
    {
        get { return configData.LargeXpOrb; }
    }

    /// <summary>
    /// XLarge xp orb value
    /// </summary>
    public static float XLargeXpOrb
    {
        get { return configData.XLargeXpOrb; }
    }

    /// <summary>
    /// XXLarge xp orb value
    /// </summary>
    public static float XXLargeXpOrb
    {
        get { return configData.XXLargeXpOrb; }
    }

    /// <summary>
    /// XXXLarge xp orb value
    /// </summary>
    public static float XXXLargeXpOrb
    {
        get { return configData.XXXLargeXpOrb; }
    }

    /// <summary>
    /// Enemy health value
    /// </summary>
    public static float EnemyHealth
    {
        get { return configData.EnemyHealth; }
    }

    /// <summary>
    /// Boss health value
    /// </summary>
    public static float BossHealth
    {
        get { return configData.BossHealth; }
    }

    /// <summary>
    /// King health value
    /// </summary>
    public static float KingHealth
    {
        get { return configData.KingHealth; }
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
    /// Seedling health value
    /// </summary>
    public static float SeedlingHealth
    {
        get { return configData.SeedlingHealth; }
    }

    /// <summary>
    /// Seedling damage value
    /// </summary>
    public static float SeedlingDamage
    {
        get { return configData.SeedlingDamage; }
    }

    /// <summary>
    /// Seedling rate of fire value
    /// </summary>
    public static float SeedlingROF
    {
        get { return configData.SeedlingROF; }
    }

    /// <summary>
    /// Thorn damage value
    /// </summary>
    public static float ThronDamage
    {
        get { return configData.ThornDamage; }
    }

    /// <summary>
    /// Thorn rate of fire value
    /// </summary>
    public static float ThornROF
    {
        get { return configData.ThornROF; }
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
    /// Rare move speed mod value
    /// </summary>
    public static float RarMoveSpeedMod
    {
        get { return configData.RarMoveSpeedMod; }
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
    /// Rare shoot cooldown mod value
    /// </summary>
    public static float RarShootCooldownMod
    {
        get { return configData.RarShootCooldownMod; }
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
    /// Rare max health mod value
    /// </summary>
    public static float RarMaxHealthMod
    {
        get { return configData.RarMaxHealthMod; }
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
    /// Rare seed speed mod value
    /// </summary>
    public static float RarSeedSpeedMod
    {
        get { return configData.RarSeedSpeedMod; }
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
    /// Rare crit chance mod value
    /// </summary>
    public static float RarCritChanceMod
    {
        get { return configData.RarCritChanceMod; }
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

    /// <summary>
    /// Uncommon damage mod value
    /// </summary>
    public static float RarDamageMod
    {
        get { return configData.RarDamageMod; }
    }


    /// <summary>
    /// Uncommon thorn damage mod value
    /// </summary>
    public static float UncThornDamageMod
    {
        get { return configData.UncThornDamageMod; }
    }

    /// <summary>
    /// Rare thorn damage mod value
    /// </summary>
    public static float RarThornDamageMod
    {
        get { return configData.RarThornDamageMod; }
    }

    /// <summary>
    /// Uncommon thorn rate of fire mod value
    /// </summary>
    public static float UncThornROFMod
    {
        get { return configData.UncThornROFMod; }
    }

    /// <summary>
    /// Rare thorn rate of fire mod value
    /// </summary>
    public static float RarThornROFMod
    {
        get { return configData.RarThornROFMod; }
    }

    /// <summary>
    /// Uncommon seedling damage mod value
    /// </summary>
    public static float UncSeedlingDamageMod
    {
        get { return configData.UncSeedlingDamageMod; }
    }

    /// <summary>
    /// Rare seedling damage mod value
    /// </summary>
    public static float RarSeedlingDamageMod
    {
        get { return configData.RarSeedlingDamageMod; }
    }

    /// <summary>
    /// Uncommon seedling rate of fire mod value
    /// </summary>
    public static float UncSeedlingROFMod
    {
        get { return configData.UncSeedlingROFMod; }
    }

    /// <summary>
    /// Rare seedling rate of fire mod value
    /// </summary>
    public static float RarSeedlingROFMod
    {
        get { return configData.RarSeedlingROFMod; }
    }

    /// <summary>
    /// Uncommon seedling health mod value
    /// </summary>
    public static float UncSeedlingHealthMod
    {
        get { return configData.UncSeedlingHealthMod; }
    }

    /// <summary>
    /// Rare seedling health mod value
    /// </summary>
    public static float RarSeedlingHealthMod
    {
        get { return configData.RarSeedlingHealthMod; }
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
