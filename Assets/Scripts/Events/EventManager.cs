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

    static Dictionary<FloatEventName, List<FloatEventInvoker>> floatInvokers =
        new Dictionary<FloatEventName, List<FloatEventInvoker>>();

    static Dictionary<FloatEventName, List<UnityAction<float>>> floatListeners =
        new Dictionary<FloatEventName, List<UnityAction<float>>>();

    static Dictionary<StringEventName, List<StringEventInvoker>> stringInvokers =
        new Dictionary<StringEventName, List<StringEventInvoker>>();

    static Dictionary<StringEventName, List<UnityAction<string>>> stringListeners =
        new Dictionary<StringEventName, List<UnityAction<string>>>();

    #endregion

    #region Public Methods

    /// <summary>
    /// Initialize the event dictionaries
    /// </summary>
    public static void Initialize()
    {
        // create empty list for each name in float dictionary
        foreach (FloatEventName name in Enum.GetValues(typeof(FloatEventName)))
        {
            if (!floatInvokers.ContainsKey(name))
            {
                floatInvokers.Add(name, new List<FloatEventInvoker>());
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
                stringInvokers.Add(name, new List<StringEventInvoker>());
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
    /// Adds float invoker for the given event name
    /// </summary>
    /// <param name="name">EventName</param>
    /// <param name="invoker">Invoker</param>
    public static void AddFloatInvoker(FloatEventName name, FloatEventInvoker invoker)
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
    public static void AddStringInvoker(StringEventName name, StringEventInvoker invoker)
    {
        // add stringListeners to new invoker and add invoker to dictionary
        foreach (UnityAction<string> listener in stringListeners[name])
        {
            invoker.AddListener(name, listener);
        }
        stringInvokers[name].Add(invoker);
    }

    /// <summary>
    /// Adds float listener for the given event name
    /// </summary>
    /// <param name="name">EventName/param>
    /// <param name="listener">Listener</param>
    public static void AddFloatListener(FloatEventName name, UnityAction<float> listener)
    {
        // add as listener to all floatInvokers and add new listener to dictionary
        foreach (FloatEventInvoker invoker in floatInvokers[name])
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
        foreach (StringEventInvoker invoker in stringInvokers[name])
        {
            invoker.AddListener(name, listener);
        }
        stringListeners[name].Add(listener);
    }

    /// <summary>
    /// Removes float invoker for the given event name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="invoker"></param>
    public static void RemoveFloatInvoker(FloatEventName name, FloatEventInvoker invoker)
    {
        // remove invoker from dictionary
        floatInvokers[name].Remove(invoker);
    }

    /// <summary>
    /// Removes string invoker for the given event name
    /// </summary>
    /// <param name="name"></param>
    /// <param name="invoker"></param>
    public static void RemoveStringInvoker(StringEventName name, StringEventInvoker invoker)
    {
        // remove invoker from dictionary
        stringInvokers[name].Remove(invoker);
    }

    #endregion
}
