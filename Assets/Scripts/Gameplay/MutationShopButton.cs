using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Mutation shop button behavior for mouse over event
/// </summary>
public class MutationShopButton : EventInvoker, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    string nam;
    EventName eventname;

    public void Start()
    {
        // Remove "(Clone)" from the name
        string[] splitName = gameObject.name.Split("(");
        nam = splitName[0];

        // Parse the float event name
        eventname = (EventName)Enum.Parse(typeof(EventName), nam);

        // Add as float invoker mutation unlocked event
        unityEvents.Add(eventname, new MutationUnlockedEvent());
        EventManager.AddInvoker(eventname, this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Get the information text
        string text = Mod.MutationInfo[nam];

        // Display the information text
        GameObject.FindGameObjectWithTag("MutationInfoText").gameObject.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        return;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        string[] splitKey = nam.Split("I");
        string key = splitKey[0];

        // Update the modifier value
        Mod.ActiveMutations[key]++;

        // Invoke the event to notify all listeners
        unityEvents[eventname].Invoke();

        // Remove button from invokers
        EventManager.RemoveInvoker(eventname, this);

        // Unpause game
        Time.timeScale = 1;

        // Set shop innactive
        GameObject.FindGameObjectWithTag("MutationShop").SetActive(false);
    }
}
