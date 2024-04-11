using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    ///
    /// </summary>
public class Player : EventInvoker
{

    float health;
    float maxHealth;
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
        maxHealth = ConfigUtils.PlayerMaxHealth + Mod.ActiveModifiers["MaxHealthMod"];

        // Add as listener for max health mod changed
        EventManager.AddListener(EventName.MaxHealthMod, HandleMaxHealthModChanged);

        // Add as invoker for add money event
        unityFloatEvents.Add(FloatEventName.AddMoneyEvent, new AddMoneyEvent());
        EventManager.AddFloatInvoker(FloatEventName.AddMoneyEvent, this);

        // Add as invoker for add health event
        unityFloatEvents.Add(FloatEventName.AddHealthEvent, new AddHealthEvent());
        EventManager.AddFloatInvoker(FloatEventName.AddHealthEvent, this);

        // Add as invoker for loose health event
        unityFloatEvents.Add(FloatEventName.LooseHealthEvent, new LooseHealthEvent());
        EventManager.AddFloatInvoker(FloatEventName.LooseHealthEvent, this);

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
            if (coll.gameObject.CompareTag("Enemy") || coll.gameObject.CompareTag("RedBoss"))
            {
                HandleDamage(coll.gameObject);
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get name of game object
        string t = collision.gameObject.tag;

        if (collision.gameObject.CompareTag("Projectile"))
        {
            HandleDamage(collision.gameObject);
            return;
        }

        // Stops camera bounds and boss wall from being a trigger
        if (!collision.gameObject.CompareTag("CameraBounds") && !collision.gameObject.CompareTag("BossWall"))
        {
            // Check if heart
            if (t == "Heart")
            {
                // Get the value
                float value = GetValue(t);

                // Keep health clamped
                float tempHealth = Mathf.Clamp(health + value, 0, maxHealth);
                health = tempHealth;

                // Trigger add health event
                unityFloatEvents[FloatEventName.AddHealthEvent].Invoke(value);
            }
            // Get money value if not heart
            else
            {
                // Get the value
                float value = GetValue(t);

                // Trigger add money event
                unityFloatEvents[FloatEventName.AddMoneyEvent].Invoke(value);
            }

            // Destroy game object
            Destroy(collision.gameObject);
        }
    }

    // Handles taking damage
    private void HandleDamage(GameObject collision)
    {
        // Reduce health and notify HUD
        health -= 1f;
        unityFloatEvents[FloatEventName.LooseHealthEvent].Invoke(1f);

        // Go invulnerable for some time
        vulnerable = false;
        damageCooldown.Run();

        // Check if dead
        if (health <= 0)
        {
            // Invoke death event
            unityEvents[EventName.PlayerDeathEvent].Invoke();

            // Make camera follow enemy that killed the player
            killCam = GameObject.FindGameObjectWithTag("Follower").gameObject.GetComponent<CinemachineVirtualCamera>();

            // Check if player killed by boss projectile
            if (collision.CompareTag("Projectile"))
            {
                // Check if player and boss killed each other
                if (GameObject.FindGameObjectWithTag("RedBoss") == null)
                {
                    // Follow random enemy
                    killCam.Follow = GameObject.FindGameObjectWithTag("Enemy").transform;
                }
                // Follow boss if still alive
                else
                {
                    killCam.Follow = GameObject.FindGameObjectWithTag("RedBoss").transform;
                }

            }
            // Follow the enemy that killed the player
            else
            {
                // Check if player and enemy killed each other
                if (collision.gameObject == null)
                {
                    // Folow random enemy
                    killCam.Follow = GameObject.FindGameObjectWithTag("Enemy").transform;
                }
                // Follow killer if still alive
                else
                {
                    killCam.Follow = collision.transform;
                }
            }

            // Destroy the player
            Destroy(gameObject);
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

    /// <summary>
    /// Updates player max health on change
    /// </summary>
    private void HandleMaxHealthModChanged()
    {
        maxHealth = ConfigUtils.PlayerMaxHealth + Mod.ActiveModifiers["MaxHealthMod"];
    }
}
