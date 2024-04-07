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

    protected Dictionary<EventName, UnityEvent<float>> unityEvents =
        new Dictionary<EventName, UnityEvent<float>>();

    #endregion

    #region Methods

    /// <summary>
    /// Adds listener for the given event name
    /// </summary>
    /// <param name="name">EventName</param>
    /// <param name="listener">Listener</param>
    public void AddListener(EventName name, UnityAction<float> listener)
    {
        // add listeners for supported events
        if (unityEvents.ContainsKey(name))
        {
            unityEvents[name].AddListener(listener);
        }
    }

    #endregion
}
