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
        string text = Mod.Info[nam] + Mod.Stat[nam];
        GameObject.FindGameObjectWithTag("InfoText").gameObject.GetComponent<TextMeshProUGUI>().text = text;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        return;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Mod.ActiveModifiers[Mod.MiddleMan[nam]] += Mod.Modifier[nam];
        unityEvents[eventname].Invoke();
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("Shop").SetActive(false);
    }
}
