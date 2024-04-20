using System.Collections.Generic;
using UnityEngine;

/// <summary>
///
/// </summary>
public class Shop : MonoBehaviour
{
    [SerializeField]
    GameObject shop;

    // Loot pool for increased uptake
    [SerializeField]
    List<GameObject> prefabCommonButtons;
    List<int> usedCommon;
    [SerializeField]
    List<GameObject> prefabUncommonButtons;
    List<int> usedUncommon;
    [SerializeField]
    List<GameObject> prefabRareButtons;
    List<int> usedRare;
    
    // Loot pool for enhancements of unclocked mutations
    // if mutation unlocked the corresponding pool will 
    // be merged with the corresponding above pool
    [SerializeField]
    List<GameObject> prefabThornsEnhaceUncommonButtons;
    [SerializeField]
    List<GameObject> prefabThornsEnhaceRareButtons;
    [SerializeField]
    List<GameObject> prefabSeedlingEnhanceUncommonButtons;
    [SerializeField]
    List<GameObject> prefabSeedlingEnhanceRareButtons;
    bool thornModsAdded = false;
    bool reproductionModsAdded = false;

    [SerializeField]
    GameObject buttonParent;
    float uncThreshold = 0.3f;
    float rarThreshold = 0.1f;

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
        usedRare = new List<int>();

        // Check for unlocked mutations to merge into loot pool
        if (!thornModsAdded && !reproductionModsAdded)
        {
            MergeEnchancements();
        }
        
        for (int i = 0; i < 4; i++)
        {
            float type = Random.Range(0f, 1f);
            if (type <= rarThreshold)
            {
                int index = Random.Range(0, prefabRareButtons.Count);
                while (usedRare.Contains(index))
                {
                    index = Random.Range(0, prefabRareButtons.Count);
                }
                usedRare.Add(index);
                Instantiate(prefabRareButtons[index], buttonParent.transform);
            }
            else if (type <= uncThreshold)
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

    /// <summary>
    /// Merges enhancement lists into loot pools
    /// </summary>
    private void MergeEnchancements()
    {
        if (Mod.ActiveMutations["Thorns"] >= 1 && !thornModsAdded)
        {
            // Merge thorns enhancements into loot pool
            foreach (GameObject button in prefabThornsEnhaceUncommonButtons)
            {
                prefabUncommonButtons.Add(button);
            }

            foreach (GameObject button in prefabThornsEnhaceRareButtons)
            {
                prefabRareButtons.Add(button);
            }

            thornModsAdded = true;
        }

        if (Mod.ActiveMutations["Reproduction"] >= 1 && !reproductionModsAdded)
        {
            foreach (GameObject button in prefabSeedlingEnhanceUncommonButtons)
            {
                prefabUncommonButtons.Add(button);
            }

            foreach (GameObject button in prefabSeedlingEnhanceRareButtons)
            {
                prefabRareButtons.Add(button);
            }

            reproductionModsAdded = true;
        }
    }
}
