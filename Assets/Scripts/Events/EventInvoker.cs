using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Event invoker class 
/// </summary>
public class EventInvoker : MonoBehaviour
{
    #region Fields

    protected Dictionary<EventName, UnityEvent> unityEvents =
        new Dictionary<EventName, UnityEvent>();

    protected Dictionary<FloatEventName, UnityEvent<float>> unityFloatEvents =
        new Dictionary<FloatEventName, UnityEvent<float>>();

    protected Dictionary<StringEventName, UnityEvent<string>> unityStringEvents =
        new Dictionary<StringEventName, UnityEvent<string>>();

    #endregion

    #region Methods

    /// <summary>
    /// Adds listener for the given event name
    /// </summary>
    /// <param name="name">EventName</param>
    /// <param name="listener">Listener</param>
    public virtual void AddListener(EventName name, UnityAction listener)
    {
        // add listeners for supported events
        if (unityEvents.ContainsKey(name))
        {
            unityEvents[name].AddListener(listener);
        }
    }

    /// <summary>
    /// Adds listener for the given float event name
    /// </summary>
    /// <param name="name">EventName</param>
    /// <param name="listener">Listener</param>
    public virtual void AddListener(FloatEventName name, UnityAction<float> listener)
    {
        // add listeners for supported events
        if (unityFloatEvents.ContainsKey(name))
        {
            unityFloatEvents[name].AddListener(listener);
        }
    }

    /// <summary>
    /// Adds listener for the given string event name
    /// </summary>
    /// <param name="name">EventName</param>
    /// <param name="listener">Listener</param>
    public virtual void AddListener(StringEventName name, UnityAction<string> listener)
    {
        // add listeners for supported events
        if (unityStringEvents.ContainsKey(name))
        {
            unityStringEvents[name].AddListener(listener);
        }
    }

    #endregion
}
