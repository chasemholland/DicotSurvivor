using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

    /// <summary>
    /// Parent class for enemies 
    /// </summary>
public class Enemy : EventInvoker
{
    // Enemy health
    protected float health;

    // Player damage and crit chance
    protected float seedDamageAmount;
    protected float seedlingSeedDamageAmount;
    protected float critChance;

    // Death effect
    protected Timer stepDeathEffect;
    protected float stepDeathDuration;
    protected Material mat;
    protected float fade = 1f;

    // Experience
    [SerializeField]
    protected List<GameObject> expOrbs;
    [SerializeField]
    protected GameObject heart;

    // Feilds used for enemies that attack (Only Cyan for now)
    protected GameObject player;
    [SerializeField]
    protected GameObject projectile;
    [SerializeField]
    protected Transform projectileTransform;
    protected float projForce = 8f;

    // Orb impulse force
    protected Vector2 force;

    // Camera coords
    protected Vector3 bottomLeftCam;
    protected Vector3 topRightCam;

    // Thorn damage
    protected float thornDamageAmount;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    protected virtual void Start()
    {
        //------------ Muatator Related --------------------------------------------------------------------

        // Set seedling damage amount
        seedlingSeedDamageAmount = ConfigUtils.SeedlingDamage + Mod.ActiveModifiers["SeedlingDamageMod"];

        // Get thorn damage
        thornDamageAmount = ConfigUtils.ThornDamage + Mod.ActiveModifiers["ThornDamageMod"];

        // Add as listener for seedling seed damage mod change
        EventManager.AddListener(EventName.SeedlingDamageMod, HandleSeedlingDamageModChanged);

        // Add as lsitener for thorn damage mod changed
        EventManager.AddListener(EventName.ThornDamageMod, HandleThornDamageModChanged);

        //------------- Player Related -------------------------------------------------------------------------

        // Get player damage stats
        seedDamageAmount = ConfigUtils.PlayerDamage + Mod.ActiveModifiers["DamageMod"];
        critChance = ConfigUtils.PlayerCritChance + Mod.ActiveModifiers["CritChanceMod"]; 

        // Add as listener for damage mod change and crit chance mod change
        EventManager.AddListener(EventName.DamageMod, HandleDamageModChanged);
        EventManager.AddListener(EventName.CritChanceMod, HandleCritChanceModChanged);

        //-------------- Enemy Related ---------------------------------------------------------------------------

        // Get enemy health
        if (gameObject.CompareTag("Enemy"))
        {
            // Get regular enemy health
            health = ConfigUtils.EnemyHealth + Tracker.EnemyHealthMod;
        }
        else if (gameObject.CompareTag("RedBoss"))
        {
            // Get boss health
            health = ConfigUtils.BossHealth + (Tracker.EnemyHealthMod * 50);
        }
        
        // Set up death effect timer
        stepDeathDuration = 0.05f;
        stepDeathEffect = gameObject.AddComponent<Timer>();
        stepDeathEffect.AddTimerFinishedListener(HandleStep);
        stepDeathEffect.Duration = stepDeathDuration;

        // Get refernce to shader effect
        mat = gameObject.GetComponent<SpriteRenderer>().material;

        //-------------- Camera Related ---------------------------------------------------------------------------

        // Set camera points
        bottomLeftCam = new Vector3(0, 0, 0);
        topRightCam = new Vector3(1, 1, 0);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    protected virtual void Update()
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

    /// <summary>
    /// Fade death effect
    /// </summary>
    protected virtual void HandleStep()
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
    /// Attacks player when called by animator
    /// </summary>
    public virtual void AttackPlayer()
    {    
    }

    /// <summary>
    /// Boss attacks player called by boss animator
    /// </summary>
    /// <param name="num">projectile being fired</param>
    /// <param name="angleStep">angle from start point</param>
    /// <param name="radius">radius of spawn circle</param>
    public virtual void AttackPlayer(int num, float angleStep, float radius)
    {
    }

    /// <summary>
    /// Destroys enemy when they hit the boss wall
    /// </summary>
    /// <param name="coll"></param>
    protected virtual void OnCollisionEnter2D(Collision2D coll)
    {
        // This gets rid of any other enemies during boss fight
        if (coll.gameObject.CompareTag("BossWall") || coll.gameObject.CompareTag("RedBoss"))
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Handles damage when seed collides
    /// </summary>
    /// <param name="collision"></param>
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Seed"))
        {
            if (Random.Range(0f, 1f) <= critChance)
            {
                health -= seedDamageAmount * 2;
            }
            else
            {
                health -= seedDamageAmount;
            }
        }

        if (collision.gameObject.CompareTag("SeedlingSeed"))
        {
            health -= seedlingSeedDamageAmount;
        }

        if (collision.gameObject.CompareTag("Thorn"))
        {
            health -= thornDamageAmount;
        }
    }

    /// <summary>
    /// Spawns random sizes and amounts of pickups
    /// </summary>
    protected virtual void SpawnRandomPickup()
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
    protected virtual void HandleCritChanceModChanged()
    {
        critChance = Mathf.Clamp(ConfigUtils.PlayerCritChance + Mod.ActiveModifiers["CritChanceMod"], 0, 1);
    }

    /// <summary>
    /// Updates player damage
    /// </summary>
    protected virtual void HandleDamageModChanged()
    {
        seedDamageAmount = ConfigUtils.PlayerDamage + Mod.ActiveModifiers["DamageMod"];
    }

    /// <summary>
    /// Updates seedling damage
    /// </summary>
    protected virtual void HandleSeedlingDamageModChanged()
    {
        seedlingSeedDamageAmount = ConfigUtils.SeedlingDamage + Mod.ActiveModifiers["SeedlingDamageMod"];
    }

    /// <summary>
    /// Updates thorn damage
    /// </summary>
    protected virtual void HandleThornDamageModChanged()
    {
        thornDamageAmount = ConfigUtils.ThornDamage + Mod.ActiveModifiers["ThornDamageMod"];
    }
}
