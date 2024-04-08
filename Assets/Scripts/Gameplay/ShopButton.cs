using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

    /// <summary>
    /// Shop button behavior for mouse over event
    /// </summary>
public class ShopButton : FloatEventInvoker, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    string nam;
    FloatEventName eventname;

    public void Start()
    {
        string[] splitName = gameObject.name.Split("(");
        nam = splitName[0];

        // Parse the event name
        eventname = (FloatEventName)Enum.Parse(typeof(FloatEventName), Mod.MiddleMan[nam]);

        // Add as invoker for loose health event
        unityFloatEvents.Add(eventname, new ModChangedEvent());
        EventManager.AddFloatInvoker(eventname, this);
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
        unityFloatEvents[eventname].Invoke(0);
        Time.timeScale = 1;
        GameObject.FindGameObjectWithTag("Shop").SetActive(false);
    }
}
