using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

    /// <summary>
    /// Event Manager
    /// </summary>
public static class EventManager
{
    #region Fields

    // Empty events
    static Dictionary<EventName, List<EventInvoker>> Invokers =
        new Dictionary<EventName, List<EventInvoker>>();

    static Dictionary<EventName, List<UnityAction>> Listeners =
        new Dictionary<EventName, List<UnityAction>>();

    // Float events
    static Dictionary<FloatEventName, List<EventInvoker>> floatInvokers =
        new Dictionary<FloatEventName, List<EventInvoker>>();

    static Dictionary<FloatEventName, List<UnityAction<float>>> floatListeners =
        new Dictionary<FloatEventName, List<UnityAction<float>>>();

    // String events
    static Dictionary<StringEventName, List<EventInvoker>> stringInvokers =
        new Dictionary<StringEventName, List<EventInvoker>>();

    static Dictionary<StringEventName, List<UnityAction<string>>> stringListeners =
        new Dictionary<StringEventName, List<UnityAction<string>>>();

    #endregion

    #region Public Methods

    /// <summary>
    /// Initialize the event dictionaries
    /// </summary>
    public static void Initialize()
    {
        // create empty list for each name in empty dictionary
        foreach (EventName name in Enum.GetValues(typeof(EventName)))
        {
            if (!Invokers.ContainsKey(name))
            {
                Invokers.Add(name, new List<EventInvoker>());
                Listeners.Add(name, new List<UnityAction>());
            }
            else
            {
                Invokers[name].Clear();
                Listeners[name].Clear();
            }
        }

        // create empty list for each name in float dictionary
        foreach (FloatEventName name in Enum.GetValues(typeof(FloatEventName)))
        {
            if (!floatInvokers.ContainsKey(name))
            {
                floatInvokers.Add(name, new List<EventInvoker>());
                floatListeners.Add(name, new List<UnityAction<float>>());
            }
            else
            {
                floatInvokers[name].Clear();
                floatListeners[name].Clear();
            }
        }

        // create empty list for each name in string dictionary
        foreach (StringEventName name in Enum.GetValues(typeof(StringEventName)))
        {
            if (!stringInvokers.ContainsKey(name))
            {
                stringInvokers.Add(name, new List<EventInvoker>());
                stringListeners.Add(name, new List<UnityAction<string>>());
            }
            else
            {
                stringInvokers[name].Clear();
                stringListeners[name].Clear();
            }
        }
    }

    /// <summary>
    /// Adds empty invoker for the given event name
    /// </summary>
    /// <param name="name">EventName</param>
    /// <param name="invoker">Invoker</param>
    public static void AddInvoker(EventName name, EventInvoker invoker)
    {
        // add Listeners to new invoker and add invoker to dictionary
        foreach (UnityAction listener in Listeners[name])
        {
            invoker.AddListener(name, listener);
        }
        Invokers[name].Add(invoker);
    }

    /// <summary>
    /// Adds float invoker for the given event name
    /// </summary>
    /// <param name="name">EventName</param>
    /// <param name="invoker">Invoker</param>
    public static void AddFloatInvoker(FloatEventName name, EventInvoker invoker)
    {
        // add floatListeners to new invoker and add invoker to dictionary
        foreach (UnityAction<float> listener in floatListeners[name])
        {
            invoker.AddListener(name, listener);
        }
        floatInvokers[name].Add(invoker);
    }

    /// <summary>
    /// Adds string invoker for the given event name
    /// </summary>
    /// <param name="name">EventName</param>
    /// <param name="invoker">Invoker</param>
    public static void AddStringInvoker(StringEventName name, EventInvoker invoker)
    {
        // add stringListeners to new invoker and add invoker to dictionary
        foreach (UnityAction<string> listener in stringListeners[name])
        {
            invoker.AddListener(name, listener);
        }
        stringInvokers[name].Add(invoker);
    }

    /// <summary>
    /// Adds empty listener for the given event name
    /// </summary>
    /// <param name="name">EventName/param>
    /// <param name="listener">Listener</param>
    public static void AddListener(EventName name, UnityAction listener)
    {
        // add as listener to all Invokers and add new listener to dictionary
        foreach (EventInvoker invoker in Invokers[name])
        {
            invoker.AddListener(name, listener);
        }
        Listeners[name].Add(listener);
    }

    /// <summary>
    /// Adds float listener for the given event name
    /// </summary>
    /// <param name="name">EventName/param>
    /// <param name="listener">Listener</param>
    public static void AddFloatListener(FloatEventName name, UnityAction<float> listener)
    {
        // add as listener to all floatInvokers and add new listener to dictionary
        foreach (EventInvoker invoker in floatInvokers[name])
        {
            invoker.AddListener(name, listener);
        }
        floatListeners[name].Add(listener);
    }

    /// <summary>
    /// Adds string listener for the given event name
    /// </summary>
    /// <param name="name">EventName/param>
    /// <param name="listener">Listener</param>
    public static void AddStringListener(StringEventName name, UnityAction<string> listener)
    {
        // add as listener to all stringInvokers and add new listener to dictionary
        foreach (EventInvoker invoker in stringInvokers[name])
        {
            invoker.AddListener(name, listener);
        }
        stringListeners[name].Add(listener);
    }

    /// <summary>
    /// Removes empty invoker for the given event name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="invoker"></param>
    public static void RemoveInvoker(EventName name, EventInvoker invoker)
    {
        // remove invoker from dictionary
        Invokers[name].Remove(invoker);
    }

    /// <summary>
    /// Removes float invoker for the given event name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="invoker"></param>
    public static void RemoveFloatInvoker(FloatEventName name, EventInvoker invoker)
    {
        // remove invoker from dictionary
        floatInvokers[name].Remove(invoker);
    }

    /// <summary>
    /// Removes string invoker for the given event name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="invoker"></param>
    public static void RemoveStringInvoker(StringEventName name, EventInvoker invoker)
    {
        // remove invoker from dictionary
        stringInvokers[name].Remove(invoker);
    }

    #endregion
}
