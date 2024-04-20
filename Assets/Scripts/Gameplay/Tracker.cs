using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

    /// <summary>
    /// Tracker for game stats
    /// </summary>
public static class Tracker
{
    #region Feilds

    // Only set here to keep track of resolution
    static int currentResolution = -1;

    // In game variables - reset on quit
    static float kills = 0;
    static float enemyHealthMod = 0;
    static float enemySpawnRateMod = 0;
    static float level = 1;
    static float levelUpAmount = 50;
    static float levelUpAmountDifference = 50;


    #endregion

    #region Properties

    public static int CurrentResolution
    {
        get { return currentResolution; }
        set { currentResolution = value; }
    }

    public static float Kills
    {
        get { return kills; }
        set { kills = value; }
    }

    public static float EnemyHealthMod
    {
        get { return enemyHealthMod + (Kills / 100); }
    }

    public static float EnemySpawnRateMod
    {
        get { return (enemySpawnRateMod + (Kills / 30)) / 100; }
    }

    public static float Level
    {
        get { return level; }
        set { level = value; }
    }

    public static float LevelUpAmount
    {
        get { return levelUpAmount + LevelUpAmountDifference; }
        set { levelUpAmount = value; }
    }

    public static float LevelUpAmountDifference
    {
        get { return Mathf.Floor(levelUpAmountDifference * Mathf.Sqrt(Level)); }
    }


    #endregion


    #region Methods

    public static void Initialize()
    {
        // Set defaults on game quit
        kills = 0;
        enemyHealthMod = 0;
        enemySpawnRateMod = 0;
        level = 1;
        levelUpAmount = 50;
        levelUpAmountDifference = 50;
    }

    #endregion

}
