using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// HUD behavior
/// </summary>
public class HUD : MonoBehaviour
{
    [SerializeField]
    GameObject shopBar;
    float moneyValue = 99;
    float upgradeCost;

    [SerializeField]
    GameObject healthBar;
    float healthValue;
    float maxHealth;

    [SerializeField]
    GameObject shop;

    [SerializeField]
    GameObject pauseMenu;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Set pannels innactive
        shop.SetActive(false);
        pauseMenu.SetActive(false);

        // Set initial health
        healthValue = ConfigUtils.PlayerHealth;

        // Set max health
        maxHealth = ConfigUtils.PlayerMaxHealth + Mod.ActiveModifiers["MaxHealthMod"];

        // Set upgrade cost
        upgradeCost = Tracker.UpgradeCost;

        // Set the text
        SetGUI();

        // Add as listener for add health event
        EventManager.AddFloatListener(FloatEventName.AddHealthEvent, HandleAddHealth);

        // Add as listener for loose health event
        EventManager.AddFloatListener(FloatEventName.LooseHealthEvent, HandleLooseHealth);

        // Add as listener for add money event
        EventManager.AddFloatListener(FloatEventName.AddMoneyEvent, HandleAddMoney);

        // Add as listener for max health mod event
        EventManager.AddFloatListener(FloatEventName.MaxHealthMod, HandleMaxHealthChanged);

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        
    }

    private void SetGUI()
    {
        healthBar.GetComponent<Slider>().value = healthValue / maxHealth;
        shopBar.GetComponent<Slider>().value = moneyValue / upgradeCost;
    }

    /// <summary>
    /// Handles player gaining health
    /// </summary>
    /// <param name="value"></param>
    private void HandleAddHealth(float value)
    {
        healthValue += value;
        healthValue = Mathf.Clamp(healthValue, 0, maxHealth);
        SetGUI();
    }

    /// <summary>
    /// Handle player loosing health
    /// </summary>
    /// <param name="value"></param>
    private void HandleLooseHealth(float value)
    {
        healthValue -= value;
        healthValue = Mathf.Clamp(healthValue, 0, maxHealth);
        SetGUI();
    }

    /// <summary>
    /// Handles player gaining money
    /// </summary>
    /// <param name="value"></param>
    private void HandleAddMoney(float value)
    {
        moneyValue += value;
        SetGUI();
        if (moneyValue >= upgradeCost)
        {
            moneyValue -= upgradeCost;
            SetGUI();
            Time.timeScale = 0;
            shop.SetActive(true);
            Tracker.UpgradesUnlocked += 1;
            upgradeCost = Tracker.UpgradeCost;
        }
    }

    /// <summary>
    /// Updates max health when invoked
    /// </summary>
    /// <param name="n">unused</param>
    private void HandleMaxHealthChanged(float n)
    {
        // update max health
        maxHealth = ConfigUtils.PlayerMaxHealth + Mod.ActiveModifiers["MaxHealthMod"];
        SetGUI();
    }

    public void OnShopSelection()
    {
        Time.timeScale = 1;
        shop.SetActive(false);
    }


    public void OnPauseClick()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void OnResumeClick()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnQuitClick()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
}
