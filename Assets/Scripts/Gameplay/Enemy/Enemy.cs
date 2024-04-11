using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

    /// <summary>
    /// Enemy attributes other than movement
    /// </summary>
public class Enemy : MonoBehaviour
{
    // Enemy health
    float health;

    // Player damage and crit chance
    float damageAmount;
    float critChance;

    // Death effect
    Timer stepDeathEffect;
    float stepDeathDuration;
    Material mat;
    float fade = 1f;

    // Loot
    [SerializeField]
    GameObject heart;
    [SerializeField]
    List<GameObject> commonLoot;
    [SerializeField]
    List<GameObject> uncommonLoot;
    [SerializeField]
    List<GameObject> rareLoot;
    List<List<GameObject>> loot = new List<List<GameObject>>();


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Fill loot list
        loot.Add(commonLoot);
        loot.Add(uncommonLoot);
        loot.Add(rareLoot);

        // Set enemy health (hardcoded 2 for now) ----------------------------------
        health = 2;

        // Set player damage stats
        damageAmount = ConfigUtils.PlayerDamage + Mod.ActiveModifiers["DamageMod"];
        critChance = ConfigUtils.PlayerCritChance + Mod.ActiveModifiers["CritChanceMod"];

        // Add as listener for damage mod change and crit chance mod change
        EventManager.AddListener(EventName.DamageMod, HandleDamageModChanged);
        EventManager.AddListener(EventName.CritChanceMod, HandleCritChanceModChanged);

        // Set up death effect timer
        stepDeathDuration = 0.05f;
        stepDeathEffect = gameObject.AddComponent<Timer>();
        stepDeathEffect.AddTimerFinishedListener(HandleStep);
        stepDeathEffect.Duration = stepDeathDuration;

        // Get refernce to shader effect
        mat = gameObject.GetComponent<SpriteRenderer>().material;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (health <= 0)
        {
            if (!stepDeathEffect.Running)
            {
                GameObject shadow = gameObject.transform.GetChild(0).gameObject;
                if (shadow.name == "Shadow")
                {
                    Destroy(shadow);
                }
                gameObject.GetComponent<Collider2D>().enabled = false;
                stepDeathEffect.Run();
            }    
        }
    }

    private void HandleStep()
    {
        if (fade <= 0)
        {
            // Update kills
            Tracker.Kills += 1;
            SpawnRandomPickup();
            Destroy(gameObject);
        }
        else
        {
            fade -= 0.2f;
            mat.SetFloat("_Fade", fade);
            mat.SetColor("_Color", Color.red);
            stepDeathEffect.Stop();
        }
    }

    /// <summary>
    /// Handles damage when seed collides
    /// </summary>
    /// <param name="coll"></param>
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Seed"))
        {
            if (Random.Range(0f, 1f) <= critChance)
            {
                health -= damageAmount * 2;
            }
            else
            {
                health -= damageAmount;
            } 
        }

        if (coll.gameObject.CompareTag("BossWall"))
        {
            Destroy(gameObject);
        }
    }

    private void SpawnRandomPickup()
    {
        // Get loot or heart
        if (Random.Range(0f, 1f) >= 0.95f)
        {
            Instantiate(heart, transform.position, Quaternion.identity);
        }
        else
        {
            int pool = 0;
            // Get loot pool
            float poolChance = Random.Range(0f, 1f);
            if (poolChance <= 0.75)
            {
                pool = 0;
            }
            else if (poolChance <= 0.95)
            {
                pool = 1;
            }
            else if (poolChance <= 1.0)
            {
                pool = 2;
            }

            // Get loot object
            float typeChance = Random.Range(0f, 1f);
            if (typeChance <= 0.75)
            {
                Instantiate(loot[pool][0], transform.position, Quaternion.identity);
            }
            else if (typeChance <= 0.95)
            {
                Instantiate(loot[pool][1], transform.position, Quaternion.identity);
            }
            else if (typeChance <= 1.0)
            {
                Instantiate(loot[pool][2], transform.position, Quaternion.identity);
            }
        }  
    }

    /// <summary>
    /// Updates player crit chance
    /// </summary>
    private void HandleCritChanceModChanged()
    {
        critChance = ConfigUtils.PlayerCritChance + Mod.ActiveModifiers["CritChanceMod"];
    }

    /// <summary>
    /// Updates player damage
    /// </summary>
    private void HandleDamageModChanged()
    {
        damageAmount = ConfigUtils.PlayerDamage + Mod.ActiveModifiers["DamageMod"];
    }
}
