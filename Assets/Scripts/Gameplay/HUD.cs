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
    bool paused = false;

    [SerializeField]
    GameObject shopBar;
    float moneyValue = 0;
    float upgradeCost;

    [SerializeField]
    GameObject healthBar;
    [SerializeField]
    TextMeshProUGUI healthText;
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

        // Set max health (Hard coded 10 for now) -------------------------
        maxHealth = 10;

        // Set upgrade cost (hard coded 100 for now) ------------------------------
        upgradeCost = 100;

        // Set the text
        SetGUI();

        // Add as listener for add health event
        EventManager.AddListener(EventName.AddHealthEvent, HandleAddHealth);

        // Add as listener for loose health event
        EventManager.AddListener(EventName.LooseHealthEvent, HandleLooseHealth);

        // Add as listener for add money event
        EventManager.AddListener(EventName.AddMoneyEvent, HandleAddMoney);

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
        healthText.text = healthValue.ToString();
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
