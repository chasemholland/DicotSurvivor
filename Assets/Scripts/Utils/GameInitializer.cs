using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Initialize any utility scripts
    /// </summary>
public class GameInitializer : MonoBehaviour
{
    private void Awake()
    {
        EventManager.Initialize();
        ConfigUtils.Initialize();
        Mod.Initialize();
        Tracker.Initialize();
    }
}
