using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

    /// <summary>
    /// HUD behavior
    /// </summary>
public class HUD : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI healthText;
    string healthPrefix = "Health: ";
    float healthValue;

    [SerializeField]
    TextMeshProUGUI moneyText;
    string moneyPrefix = "Money: ";
    float moneyValue = 0;
    
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Set initial health
        healthValue = ConfigUtils.PlayerHealth;

        // Set the text
        SetText();

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

    private void SetText()
    {
        healthText.text = healthPrefix + healthValue.ToString();
        moneyText.text = moneyPrefix + moneyValue.ToString();
    }

    /// <summary>
    /// Handles player gaining health
    /// </summary>
    /// <param name="value"></param>
    private void HandleAddHealth(float value)
    {
        healthValue += value;
        healthValue = Mathf.Clamp(healthValue, 0, 10);
        SetText();
    }

    /// <summary>
    /// Handle player loosing health
    /// </summary>
    /// <param name="value"></param>
    private void HandleLooseHealth(float value)
    {
        healthValue -= value;
        healthValue = Mathf.Clamp(healthValue, 0, 10);
        SetText();
    }

    /// <summary>
    /// Handles player gaining money
    /// </summary>
    /// <param name="value"></param>
    private void HandleAddMoney(float value)
    {
        moneyValue += value;
        SetText();
    }
}
