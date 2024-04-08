using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
///
/// </summary>
public class Shop : MonoBehaviour
{
    [SerializeField]
    GameObject shop;
    [SerializeField]
    List<GameObject> prefabButtons;
    [SerializeField]
    GameObject buttonParent;
    [SerializeField]
    TextMeshProUGUI infoText;

    private void OnEnable()
    {
        // Destroy any remnant buttons
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("ShopButton");
        if (buttons != null)
        {
            foreach (GameObject button in buttons)
            {
                Destroy(button);
            }
        }

        for (int i = 0; i < prefabButtons.Count; i++)
        {
            Instantiate(prefabButtons[i], buttonParent.transform);
        }
    }

}
