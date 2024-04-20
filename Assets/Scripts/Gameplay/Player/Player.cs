using Cinemachine;
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

    // Seedling variables
    bool reproductionUnlocked = false;
    Timer spawnTimer;
    float trySpawn = 2f;
    float reproductionChance = 0.2f;
    float seedlingNum;
    GameObject[] seedlingsActive;
    [SerializeField]
    GameObject seedling;

    // Thorn variables
    [SerializeField]
    GameObject thorn;
    float thornNum = 0;
    float thornForce = 8;
    float radius = 0.5f;
    float angleStep;
    Timer thornTimer;
    float thornShootDelay;


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Set health
        health = ConfigUtils.PlayerHealth;
        maxHealth = ConfigUtils.PlayerMaxHealth + Mod.ActiveModifiers["MaxHealthMod"];

        // Get max number of seedlings
        seedlingNum = Mod.ActiveMutations["Reproduction"];

        // Set seedling spawn timer
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = trySpawn;
        spawnTimer.AddTimerFinishedListener(ChanceToSpawnSeedling);

        // Get thorn shoot delay
        thornShootDelay = ConfigUtils.ThornROF - Mod.ActiveModifiers["ThornROFMod"];

        // Set up thorn shoot timer
        thornTimer = gameObject.AddComponent<Timer>();
        thornTimer.AddTimerFinishedListener(ShootThorns);

        // Add as listener for max health mod changed
        EventManager.AddListener(EventName.MaxHealthMod, HandleMaxHealthModChanged);

        // Add as listener for thorns unlocked event
        EventManager.AddListener(EventName.Thorns, HandleThornsUnlocked);

        // Add as listener for reproduction unlocked event
        EventManager.AddListener(EventName.Reproduction, HandleReproductionUnlocked);
        
        // Add as invoker for add experience event
        unityFloatEvents.Add(FloatEventName.AddExperienceEvent, new AddExperienceEvent());
        EventManager.AddFloatInvoker(FloatEventName.AddExperienceEvent, this);

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
                return;
            }
        } 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get name of game object
        string t = collision.gameObject.tag;

        // Ignor triggers
        if (collision.gameObject.CompareTag("CollectionField") || collision.gameObject.CompareTag("DetectionField") ||
            collision.gameObject.CompareTag("Seed") || collision.gameObject.CompareTag("SeedlingSeed") || collision.gameObject.CompareTag("Thorn"))
        {
            return;
        }

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

                // Destroy game object
                Destroy(collision.gameObject);
            }
            else
            {
                // Get the value
                float value = GetValue(t);

                // Trigger add money event
                unityFloatEvents[FloatEventName.AddExperienceEvent].Invoke(value);

                // Destroy game object
                Destroy(collision.gameObject);
            }
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
    /// <param name="tag">pickup tag</param>
    /// <returns></returns>
    private float GetValue(string tag)
    {
        switch (tag)
        {
            case "Heart":
                return ConfigUtils.Heart;
            case "SmallOrb":
                return ConfigUtils.SmallXpOrb;
            case "MediumOrb":
                return ConfigUtils.MediumXpOrb;
            case "LargeOrb":
                return ConfigUtils.LargeXpOrb;
            case "XLargeOrb":
                return ConfigUtils.XLargeXpOrb;
            case "XXLargeOrb":
                return ConfigUtils.XXLargeXpOrb;
            case "XXXLargeOrb":
                return ConfigUtils.XXXLargeXpOrb;
            default:
                return 0;
        }

    }

    /// <summary>
    /// Handles spawning seedlings
    /// </summary>
    private void ChanceToSpawnSeedling()
    {
        // Check how many seedling are active
        seedlingsActive = GameObject.FindGameObjectsWithTag("Seedling");

        if (seedlingsActive.Length < seedlingNum && Random.Range(0f, 1f) < reproductionChance)
        {
            Instantiate(seedling, transform.position, Quaternion.identity);
        }

        spawnTimer.Duration = trySpawn;
        spawnTimer.Run();
    }

    /// <summary>
    /// Hanldes shooting thorns
    /// </summary>
    private void ShootThorns()
    {
        for (int i = 0; i < thornNum; i++)
        {
            // Calculate position based on angle
            float angle = i * angleStep;

            // Spawn position, offset on the y to account for rotation point being at the bottom of the player
            Vector3 offSetPosition = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 1f, 0);
            Vector3 spawnPosition = offSetPosition + Quaternion.Euler(0, 0, angle) * Vector3.right * radius;

            // Instantiate the projectile
            GameObject proj = Instantiate(thorn, spawnPosition, Quaternion.identity);

            // Calculate direction to shoot
            Vector3 shootDirection = (proj.transform.position - offSetPosition).normalized;
            Vector2 direction = new Vector2(shootDirection.x, shootDirection.y);

            // Rotate the seed in direction of travel
            Vector3 rotation = offSetPosition - proj.transform.position;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            proj.transform.rotation = Quaternion.Euler(0, 0, rot + 90);

            proj.GetComponent<Rigidbody2D>().velocity = direction * thornForce;
        }
        
        thornTimer.Duration = thornShootDelay;
        thornTimer.Run();
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

    /// <summary>
    /// Unlocks reproduction if not unlocked
    /// </summary>
    private void HandleReproductionUnlocked()
    {
        // Update max seedling value
        seedlingNum = Mod.ActiveMutations["Reproduction"];

        if (!reproductionUnlocked)
        {
            spawnTimer.Run();
            reproductionUnlocked = true;
        }
    }

    /// <summary>
    /// Unlocks thorns if not unlocked
    /// </summary>
    private void HandleThornsUnlocked()
    {
        // 1, 2, 3 Multiply by 2 to get 2, 4, 6 thorns
        thornNum = Mod.ActiveMutations["Thorns"] * 2;

        angleStep = 360f / thornNum;
        
        if (thornTimer.Running)
        {
            thornTimer.Stop();
        }

        thornTimer.Duration = thornShootDelay;
        thornTimer.Run();
    }
}
