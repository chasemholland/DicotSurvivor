using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>
/// Shop button behavior for mouse over event
/// </summary>
public class ShopButton : EventInvoker, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    string nam;
    EventName eventname;

    public void Start()
    {
        // Remove "(Clone)" from the name
        string[] splitName = gameObject.name.Split("(");
        nam = splitName[0];

        // Parse the event name
        eventname = (EventName)Enum.Parse(typeof(EventName), Mod.MiddleMan[nam]);

        // Add as invoker for loose health event
        unityEvents.Add(eventname, new ModChangedEvent());
        EventManager.AddInvoker(eventname, this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Get the information text
        string text = Mod.Info[nam] + Mod.Stat[nam];

        // Display the information text
        GameObject.FindGameObjectWithTag("InfoText").gameObject.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        return;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        AudioManager.Play(AudioName.Select);

        // Updat the modifier value
        Mod.ActiveModifiers[Mod.MiddleMan[nam]] += Mod.Modifier[nam];

        // Invoke the evennt to notify all listeners
        unityEvents[eventname].Invoke();

        // Remove button from invokers
        EventManager.RemoveInvoker(eventname, this);

        // Unpause game
        Time.timeScale = 1;

        // Set shop innactive
        GameObject.FindGameObjectWithTag("Shop").SetActive(false);
    }
}
