using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
///
/// </summary>
public class FloatEventInvoker : MonoBehaviour
{
    #region Fields

    protected Dictionary<FloatEventName, UnityEvent<float>> unityFloatEvents =
        new Dictionary<FloatEventName, UnityEvent<float>>();

    #endregion

    #region Methods

    /// <summary>
    /// Adds listener for the given event name
    /// </summary>
    /// <param name="name">EventName</param>
    /// <param name="listener">Listener</param>
    public void AddListener(FloatEventName name, UnityAction<float> listener)
    {
        // add listeners for supported events
        if (unityFloatEvents.ContainsKey(name))
        {
            unityFloatEvents[name].AddListener(listener);
        }
    }

    #endregion
}
