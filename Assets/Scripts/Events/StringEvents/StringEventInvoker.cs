using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// String event invoker
/// </summary>
public class StringEventInvoker : MonoBehaviour
{
    #region Fields

    protected Dictionary<StringEventName, UnityEvent<string>> unityStringEvents =
        new Dictionary<StringEventName, UnityEvent<string>>();

    #endregion

    #region Methods

    /// <summary>
    /// Adds listener for the given event name
    /// </summary>
    /// <param name="name">EventName</param>
    /// <param name="listener">Listener</param>
    public void AddListener(StringEventName name, UnityAction<string> listener)
    {
        // add listeners for supported events
        if (unityStringEvents.ContainsKey(name))
        {
            unityStringEvents[name].AddListener(listener);
        }
    }

    #endregion
}
