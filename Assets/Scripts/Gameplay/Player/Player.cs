using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    ///
    /// </summary>
public class Player : FloatEventInvoker
{

    float health;
    bool vulnerable = true;
    Timer damageCooldown;
    float cooldown = 1f;
    CinemachineVirtualCamera killCam;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Set health
        health = ConfigUtils.PlayerHealth;

        // Add as invoker for add money event
        unityEvents.Add(EventName.AddMoneyEvent, new AddMoneyEvent());
        EventManager.AddInvoker(EventName.AddMoneyEvent, this);

        // Add as invoker for add health event
        unityEvents.Add(EventName.AddHealthEvent, new AddHealthEvent());
        EventManager.AddInvoker(EventName.AddHealthEvent, this);

        // Add as invoker for loose health event
        unityEvents.Add(EventName.LooseHealthEvent, new LooseHealthEvent());
        EventManager.AddInvoker(EventName.LooseHealthEvent, this);

        // Add as invoker for player death event
        unityEvents.Add(EventName.PlayerDeathEvent, new PlayerDeathEvent());
        EventManager.AddInvoker(EventName.PlayerDeathEvent, this);

        // Set up damage cooldown timer
        damageCooldown = gameObject.AddComponent<Timer>();
        damageCooldown.AddTimerFinishedListener(UpdateVulnerability);
        damageCooldown.Duration = cooldown;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (vulnerable)
        {
            if (coll.gameObject.CompareTag("Enemy"))
            {
                // Reduce health and notify HUD
                health -= 1f;
                unityEvents[EventName.LooseHealthEvent].Invoke(1f);
                
                // Go invulnerable for some time
                vulnerable = false;
                damageCooldown.Run();

                // Check if dead
                if (health <= 0)
                {
                    // Invoke death event
                    unityEvents[EventName.PlayerDeathEvent].Invoke(0);

                    // Make camera follow enemy that killed the player
                    killCam = GameObject.FindGameObjectWithTag("Follower").gameObject.GetComponent<CinemachineVirtualCamera>();
                    killCam.Follow = coll.gameObject.transform;

                    // Destroy the player
                    Destroy(gameObject);
                }
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get name of game object
        string t = collision.gameObject.tag;

        // Stops camera bounds from being a trigger
        if (t != "CameraBounds")
        {
            // Check if heart
            if (t == "Heart")
            {
                // Get the value
                float value = GetValue(t);

                // Keep health clamped
                float tempHealth = Mathf.Clamp(health + value, 0, 10);
                health = tempHealth;

                // Trigger add health event
                unityEvents[EventName.AddHealthEvent].Invoke(value);
            }
            // Get money value if not heart
            else
            {
                // Get the value
                float value = GetValue(t);

                // Trigger add money event
                unityEvents[EventName.AddMoneyEvent].Invoke(value);
            }

            // Destroy game object
            Destroy(collision.gameObject);
        }
    }

    /// <summary>
    /// Gets the value of the pickup
    /// </summary>
    /// <param name="name">pickup name</param>
    /// <returns></returns>
    private float GetValue(string name)
    {
        switch (name)
        {
            case "Heart":
                return ConfigUtils.Heart;
            case "BronzeCoin":
                return ConfigUtils.BronzeCoin;
            case "BronzeCoinStack":
                return ConfigUtils.BronzeCoinStack;
            case "BronzeCoinBag":
                return ConfigUtils.BronzeCoinBag;
            case "SilverCoin":
                return ConfigUtils.SilverCoin;
            case "SilverCoinStack":
                return ConfigUtils.SilverCoinStack;
            case "SilverCoinBag":
                return ConfigUtils.SilverCoinBag;
            case "GoldCoin":
                return ConfigUtils.GoldCoin;
            case "GoldCoinStack":
                return ConfigUtils.GoldCoinStack;
            case "GoldCoinBag":
                return ConfigUtils.GoldCoinBag;
            default:
                return 0;
        }

    }

    /// <summary>
    /// Makes player vulnerable after cooldown
    /// </summary>
    private void UpdateVulnerability()
    {
        vulnerable = true;
        damageCooldown.Duration = cooldown;
    }
}
