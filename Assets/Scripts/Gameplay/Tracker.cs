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

    static int currentResolution = -1;
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
        get { return levelUpAmountDifference * Level; }
    }


    #endregion

}
