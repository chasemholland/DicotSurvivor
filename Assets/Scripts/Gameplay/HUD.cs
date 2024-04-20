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
    float xpValue = 0;
    float levelUpAmount;

    [SerializeField]
    TextMeshProUGUI xpText;

    [SerializeField]
    GameObject healthBar;
    float healthValue;
    float maxHealth;

    [SerializeField]
    GameObject shop;

    [SerializeField]
    GameObject mutationShop;

    [SerializeField]
    GameObject pauseMenu;

    Material playerOutline;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Get player material
        playerOutline = GameObject.FindGameObjectWithTag("Player").GetComponent<SpriteRenderer>().material;

        // Set pannels innactive
        shop.SetActive(false);
        mutationShop.SetActive(false);
        pauseMenu.SetActive(false);

        // Set initial health
        healthValue = ConfigUtils.PlayerHealth;

        // Set max health
        maxHealth = ConfigUtils.PlayerMaxHealth + Mod.ActiveModifiers["MaxHealthMod"];

        // Set upgrade cost
        levelUpAmount = Tracker.LevelUpAmount;

        // Set the text
        SetGUI();

        // Add as listener for add health event
        EventManager.AddFloatListener(FloatEventName.AddHealthEvent, HandleAddHealth);

        // Add as listener for loose health event
        EventManager.AddFloatListener(FloatEventName.LooseHealthEvent, HandleLooseHealth);

        // Add as listener for add experience event
        EventManager.AddFloatListener(FloatEventName.AddExperienceEvent, HandleAddExperience);

        // Add as listener for max health mod event
        EventManager.AddListener(EventName.MaxHealthMod, HandleMaxHealthChanged);

        // Add as listener for boss death event
        EventManager.AddListener(EventName.BossDeathEvent, HandleBossDeath);

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
        shopBar.GetComponent<Slider>().value = xpValue / levelUpAmount;
        xpText.text = xpValue.ToString() + " / " + levelUpAmount.ToString();
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
    
    private void HandleAddExperience(float value)
    {
        xpValue += value;
        SetGUI();
        // Iradiate player by increasing the outline glow
        playerOutline.SetFloat("_Intensity", (xpValue / levelUpAmount) * 20);

        // Check for level up
        if (xpValue >= levelUpAmount)
        {
            xpValue -= levelUpAmount;
            // Update level up amount with previous amount
            Tracker.LevelUpAmount = levelUpAmount;
            // Update level
            Tracker.Level += 1;
            // Get new level up amount
            levelUpAmount = Tracker.LevelUpAmount;
            SetGUI();

            // Iradiate player by increasing the outline glow
            playerOutline.SetFloat("_Intensity", (xpValue / levelUpAmount) * 20);

            Time.timeScale = 0;
            shop.SetActive(true);
        }
    }

    /// <summary>
    /// Enables the mutation shop when invoked
    /// </summary>
    private void HandleBossDeath()
    {
        // Check that there are still mutations to be unlocked
        foreach (float value in Mod.ActiveMutations.Values)
        {
            if (value != 3) 
            {
                Time.timeScale = 0;
                mutationShop.SetActive(true);
                break;
            }
        }
    }

    /// <summary>
    /// Updates max health when invoked
    /// </summary>
    private void HandleMaxHealthChanged()
    {
        // update max health
        maxHealth = ConfigUtils.PlayerMaxHealth + Mod.ActiveModifiers["MaxHealthMod"];
        SetGUI();
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
