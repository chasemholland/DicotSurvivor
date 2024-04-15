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

    static float kills = 0;
    static float enemyMoveMod = 1;
    static float enemySpawnRateMod = 0;
    static float level = 1;
    static float levelUpAmount = 50;
    static float levelUpAmountDifference = 50;


    #endregion

    #region Properties

    public static float Kills
    {
        get { return kills; }
        set { kills = value; }
    }

    public static float EnemyMoveMod
    {
        get { return enemyMoveMod + (Kills / 100); }
    }

    public static float EnemySpawnRateMod
    {
        get { return enemySpawnRateMod + (Kills / 1000); }
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
