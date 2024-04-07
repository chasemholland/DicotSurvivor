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


    #endregion

}
