using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

    /// <summary>
    /// Boss attributes other than movement
    /// </summary>
public class Boss : EventInvoker
{
    private float health;
    private float damageAmount;
    float critChance;
    Timer stepDeathEffect;
    float stepDeathDuration;
    Material mat;
    float fade = 1f;

    // Loot
    [SerializeField]
    List<GameObject> chests;

    // Projectile object
    [SerializeField]
    GameObject projectile;
    [SerializeField]
    Transform projectileTransform;
    // Projectile force
    float force = 12f;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {

        // Get enemy health
        health = ConfigUtils.BossHealth + Tracker.EnemyHealthMod;

        // Get player damage and crit chance
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

        // Add as invoker for boss death event
        unityEvents.Add(EventName.BossDeathEvent, new BossDeathEvent());
        EventManager.AddInvoker(EventName.BossDeathEvent, this);

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
            unityEvents[EventName.BossDeathEvent].Invoke();
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

    // Called by boss animator
    public void AttackPlayer(int num, float angleStep, float radius)
    {
        // Calculate position based on angle
        float angle = num * angleStep;

        // Spawn position, offset on the y to account for rotation point being at the bottom of the boss object
        Transform boss = gameObject.transform;
        Vector3 spawnPosition = projectileTransform.position + Quaternion.Euler(0, 0, angle) * Vector3.right * radius;

        // Instantiate the projectile
        GameObject proj = Instantiate(projectile, spawnPosition, Quaternion.identity);

        // Calculate direction to shoot
        Vector3 shootDirection = (proj.transform.position - projectileTransform.position).normalized;
        Vector2 direction = new Vector2(shootDirection.x, shootDirection.y);

        proj.GetComponent<Rigidbody2D>().velocity = direction * force;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
    }

    /// <summary>
    /// Handles damage when seed collides
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Seed")
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
        // Get loot pool
        float chance = Random.Range(0f, 1.0f);

        if (chance <= 0.5)
        {
            Instantiate(chests[0], transform.position, Quaternion.identity);
        }
        else if (chance <= 0.80)
        {
            Instantiate(chests[1], transform.position, Quaternion.identity);
        }
        else if (chance <= 0.95)
        {
            Instantiate(chests[2], transform.position, Quaternion.identity);
        }
        else if (chance <= 1.0)
        {
            Instantiate(chests[3], transform.position, Quaternion.identity);
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
