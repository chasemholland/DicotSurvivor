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
    static float upgradesUnlocked = 0;
    static float upgradeCost = 50;


    #endregion

    #region Properties

    public static float Kills
    {
        get { return kills; }
        set { kills = value; }
    }

    public static float EnemyMoveMod
    {
        get { return enemyMoveMod + (kills / 100); }
    }

    public static float EnemySpawnRateMod
    {
        get { return enemySpawnRateMod + (kills / 1000); }
    }

    public static float UpgradesUnlocked
    {
        get { return upgradesUnlocked; }
        set { upgradesUnlocked = value; }
    }

    public static float UpgradeCost
    {
        get { return upgradeCost + (upgradesUnlocked * 10); }
    }


    #endregion

}
