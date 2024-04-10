using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class Shop : MonoBehaviour
{
    [SerializeField]
    GameObject shop;
    [SerializeField]
    List<GameObject> prefabCommonButtons;
    List<int> usedCommon;
    [SerializeField]
    List<GameObject> prefabUncommonButtons;
    List<int> usedUncommon;
    [SerializeField]
    GameObject buttonParent;
    float uncChance = 0.1f;

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

        // Create empty list to ensure no button repeats
        usedCommon = new List<int>();
        usedUncommon = new List<int>();

        for (int i = 0; i < 3; i++)
        {
            if (Random.Range(0f, 1f) <= uncChance)
            {
                int index = Random.Range(0, prefabUncommonButtons.Count);
                while (usedUncommon.Contains(index))
                {
                    index = Random.Range(0, prefabUncommonButtons.Count);
                }
                usedUncommon.Add(index);
                Instantiate(prefabUncommonButtons[index], buttonParent.transform);
            }
            else
            {
                int index = Random.Range(0, prefabCommonButtons.Count);
                while (usedCommon.Contains(index))
                {
                    index = Random.Range(0, prefabCommonButtons.Count);
                }
                usedCommon.Add(index);
                Instantiate(prefabCommonButtons[index], buttonParent.transform);
            }
        }
    }

}
