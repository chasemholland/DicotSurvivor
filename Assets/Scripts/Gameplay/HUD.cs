using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// HUD behavior
/// </summary>
public class HUD : EventInvoker
{
    [SerializeField]
    GameObject levelLoader;

    [SerializeField]
    TextMeshProUGUI gameTime;
    float elapsedTime = 0;
    bool updateTime = true;

    [SerializeField]
    TextMeshProUGUI level;
    string levelPrefix = "Level: ";

    [SerializeField]
    TextMeshProUGUI kills;
    string killsPrefix = "Kills: ";

    [SerializeField]
    TextMeshProUGUI xpMultiplier;
    string multiplierPrefix = "XP Multiplier: ";
    float lastDamageTaken = 0;

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

        // Add as listener for enemy death event
        EventManager.AddListener(EventName.EnemyDeath, HandleEnemyDeath);

        // Add as lsitener for player death event
        EventManager.AddListener(EventName.PlayerDeathEvent, HandlePlayerDeath);

        // Add as invoker for fill player health
        unityEvents.Add(EventName.FillPlayerHealth, new FillPlayerHealthEvent());
        EventManager.AddInvoker(EventName.FillPlayerHealth, this);

        // Add as listener for xp multiplier changed event
        EventManager.AddListener(EventName.MultiplierChangedEvent, HandleMultiplierChanged);

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (updateTime)
        {
            // Update elapsed game time
            elapsedTime += Time.deltaTime;
            gameTime.text = GetTimeFormat(elapsedTime);
        }

        GetMultiplier();
        xpMultiplier.text = multiplierPrefix + Tracker.XPMulti.ToString();
    }

    private string GetTimeFormat(float time)
    {
        string hr, min, sec;
        sec = Math.Floor(time).ToString();
        if (time >= 60)
        {
            sec = Math.Floor(time % 60).ToString();
        }
        min = Math.Floor(time / 60).ToString();
        if (time >= 3600)
        {
            min = Math.Floor(time % 3600).ToString();
        }
        hr = Math.Floor(time / 3600).ToString();
        if (hr.Length < 2) hr = "0" + hr;
        if (min.Length < 2) min = "0" + min;
        if (sec.Length < 2) sec = "0" + sec;
        return hr + ":" + min + ":" + sec;
    }

    /// <summary>
    /// Gets xp multiplier based on time since damage taken
    /// </summary>
    private void GetMultiplier()
    {
        if (elapsedTime - lastDamageTaken < 30f)
        {
            Tracker.XPMulti = 1;
        }
        else if (elapsedTime - lastDamageTaken < 60f)
        {
            Tracker.XPMulti = 2;
        }
        else if (elapsedTime - lastDamageTaken < 90f)
        {
            Tracker.XPMulti = 3;
        }
        else if (elapsedTime - lastDamageTaken < 120f)
        {
            Tracker.XPMulti = 4;
        }
        else
        {
            Tracker.XPMulti = 5;
        }
    }

    private void SetGUI()
    {
        healthBar.GetComponent<Slider>().value = healthValue / maxHealth;
        shopBar.GetComponent<Slider>().value = xpValue / levelUpAmount;
        xpText.text = xpValue.ToString() + " / " + levelUpAmount.ToString();
        level.text = levelPrefix + Tracker.Level.ToString();
        kills.text = killsPrefix + Tracker.Kills.ToString();
        xpMultiplier.text = multiplierPrefix + Tracker.XPMulti.ToString();
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
    /// Resets multipier text
    /// </summary>
    private void HandleMultiplierChanged()
    {
        lastDamageTaken = elapsedTime;
        xpMultiplier.text = multiplierPrefix + Tracker.XPMulti.ToString();
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
            // Play level up sound
            AudioManager.Play(AudioName.LevelUp);

            // Fill the players health
            unityEvents[EventName.FillPlayerHealth].Invoke();
            healthValue = maxHealth;

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
        // Fill player health
        unityEvents[EventName.FillPlayerHealth].Invoke();
        healthValue = maxHealth;

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
    /// Updates kill count when invoked
    /// </summary>
    private void HandleEnemyDeath()
    {
        kills.text = killsPrefix + Tracker.Kills.ToString();
    }

    /// <summary>
    /// Stops updating game time on player death
    /// </summary>
    private void HandlePlayerDeath()
    {
        updateTime = false;
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
        // Disable player
        GameObject.FindWithTag("Player").SetActive(false);
        Time.timeScale = 1;
        levelLoader.GetComponent<LevelLoader>().LoadNextScene("MainMenu");
        //SceneManager.LoadScene("MainMenu");
    }
}
