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
    string key;
    EventName eventname;

    public void Start()
    {
        // Remove "(Clone)" from the name
        string[] splitName = gameObject.name.Split("(");
        nam = splitName[0];
        
        // Get root name: Thorns, Reproduction, Multiseed
        string[] splitNam = nam.Split("I");
        key = splitNam[0];

        // Parse the float event name
        eventname = (EventName)Enum.Parse(typeof(EventName), key);

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
