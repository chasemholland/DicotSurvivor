using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    ///
    /// </summary>
public class MutationShop : MonoBehaviour
{
    [SerializeField]
    GameObject mutationShop;

    // Loot pool for mutations
    [SerializeField]
    List<GameObject> prefabMutationButtons;
    Dictionary<string, GameObject> mutationButtons;
    List<string> activePool;
    string[] splitName;
    string mutationName;

    [SerializeField]
    GameObject buttonParent;

    
    private void OnEnable()
    {
        // Destroy any remnant buttons
        GameObject[] buttons = GameObject.FindGameObjectsWithTag("MutationShopButton");
        if (buttons != null)
        {
            foreach (GameObject button in buttons)
            {
                Destroy(button);
            }
        }

        // Create empty dictionary and list
        mutationButtons = new Dictionary<string, GameObject>();
        activePool = new List<string>();

        // Fill dictionary with buttons
        foreach (GameObject prefab in prefabMutationButtons)
        {
            splitName = prefab.name.Split("(");
            mutationName = splitName[0];
            mutationButtons.Add(mutationName, prefab);
        }

        // Instantiate correct buttons
        foreach (string key in Mod.ActiveMutations.Keys)
        {
            if (!activePool.Contains(key))
            {
                if (Mod.ActiveMutations[key] == 0)
                {
                    activePool.Add(key);
                    Instantiate(mutationButtons[key + "I"], buttonParent.transform);
                }
                else if (Mod.ActiveMutations[key] == 1)
                {
                    activePool.Add(key);
                    Instantiate(mutationButtons[key + "II"], buttonParent.transform);
                }
                else if (Mod.ActiveMutations[key] == 2)
                {
                    activePool.Add(key);
                    Instantiate(mutationButtons[key + "III"], buttonParent.transform);
                }
            }
        }
    }
}
