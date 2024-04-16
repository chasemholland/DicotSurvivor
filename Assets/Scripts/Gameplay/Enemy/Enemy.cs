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

    // Experience
    [SerializeField]
    List<GameObject> expOrbs;
    [SerializeField]
    GameObject heart;

    // Feilds used for enemies that attack (Only Cyan for now)
    GameObject player;
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    Transform projectileTransform;
    float projForce = 8f;

    // Orb impulse force
    Vector2 force;


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Get enemy health
        health = ConfigUtils.EnemyHealth + Tracker.EnemyHealthMod;

        // Get player damage stats
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

    // Attacks player when called by animator
    public void AttackPlayer()
    {
        // Get direction to player
        player = GameObject.FindGameObjectWithTag("Player");
        Vector3 directionToPlayer = (player.transform.position - projectileTransform.position).normalized;
        Vector2 direction = new Vector2(directionToPlayer.x, directionToPlayer.y);

        GameObject proj = Instantiate(projectile, projectileTransform.position, Quaternion.identity);
        proj.GetComponent<Rigidbody2D>().velocity = direction * projForce;      
    }

    /// <summary>
    /// Destroys enemy when they hit the boss wall
    /// </summary>
    /// <param name="coll"></param>
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("BossWall"))
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Handles damage when seed collides
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Seed"))
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
    }

    private void SpawnRandomPickup()
    {
        // Chance to drop a heart
        if (Random.Range(0f, 1f) >= 0.95f)
        {
            Instantiate(heart, transform.position, Quaternion.identity);
        }


        // Get number or experience orbs
        int num = 0;
        // Get loot pool
        float numChance = Random.Range(0f, 1f);
        // 50% chance for one orb
        if (numChance <= 0.50)
        {
            num = 1;
        }
        // 26% chance for two orbs
        else if (numChance <= 0.76)
        {
            num = 2;
        }
        // 16% chance for three orbs
        else if (numChance <= 0.92)
        {
            num = 3;
        }
        // 6% chance for four orbs
        else if (numChance <= 0.98)
        {
            num = 4;
        }
        // 2% chance for 5 orbs
        else if (numChance <= 1.0)
        {
            num = 5;
        }

        // Spawn num amount of orbs with chance for different sizes
        for (int i = 1; i <= num; i++)
        {
            // Get random force for movement
            force = new Vector2(Random.Range(-2f, 4f), Random.Range(-2f, 2f));

            float sizeChance = Random.Range(0f, 1f);
            {
                // 40% chance for small orb
                if (sizeChance <= 0.40)
                {
                    GameObject orb = Instantiate(expOrbs[0], transform.position, Quaternion.identity);
                    orb.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                }
                // 28% chance for medium orb
                else if (sizeChance <= 0.68)
                {
                    GameObject orb = Instantiate(expOrbs[1], transform.position, Quaternion.identity);
                    orb.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                }
                // 18% chance for large orb
                else if (sizeChance <= 0.86)
                {
                    GameObject orb = Instantiate(expOrbs[2], transform.position, Quaternion.identity);
                    orb.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                }
                // 10% chance for xlarge orb
                else if (sizeChance <= 0.96)
                {
                    GameObject orb = Instantiate(expOrbs[3], transform.position, Quaternion.identity);
                    orb.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                }
                // 4% chance for xxlarge orb
                else if (sizeChance <= 0.98)
                {
                    GameObject orb = Instantiate(expOrbs[4], transform.position, Quaternion.identity);
                    orb.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                }
                // 2% chance for xxxlargeorb
                else if (sizeChance <= 1.0)
                {
                    GameObject orb = Instantiate(expOrbs[5], transform.position, Quaternion.identity);
                    orb.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                }
            }
        }
    }

    /// <summary>
    /// Updates player crit chance
    /// </summary>
    private void HandleCritChanceModChanged()
    {
        critChance = Mathf.Clamp(ConfigUtils.PlayerCritChance + Mod.ActiveModifiers["CritChanceMod"], 0, 1);
    }

    /// <summary>
    /// Updates player damage
    /// </summary>
    private void HandleDamageModChanged()
    {
        damageAmount = ConfigUtils.PlayerDamage + Mod.ActiveModifiers["DamageMod"];
    }
}
