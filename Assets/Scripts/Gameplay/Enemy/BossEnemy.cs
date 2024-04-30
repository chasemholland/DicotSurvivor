using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

    /// <summary>
    /// Boss attributes other than movement
    /// </summary>
public class BossEnemy : Enemy
{
    // Loot
    [SerializeField]
    GameObject orbSpawner;

    // Object Pool
    ObjectPool pool;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    protected override void Start()
    {
        base.Start();
        
        //-------- Boss Related ----------------------------------------------------

        // Get reference to object pool
        pool = GameObject.FindGameObjectWithTag("SeedBank").GetComponent<ObjectPool>();

        // Get boss projectile force
        projForce = 12f;

        // Add as invoker for boss death event
        unityEvents.Add(EventName.BossDeathEvent, new BossDeathEvent());
        EventManager.AddInvoker(EventName.BossDeathEvent, this);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// Fade death effect
    /// </summary>
    protected override void HandleStep()
    {
        if (fade <= 0)
        {
            // Update kills
            Tracker.Kills += 1;
            SpawnRandomPickup();
            unityEvents[EventName.BossDeathEvent].Invoke();
            unityEvents[EventName.EnemyDeath].Invoke();
            EventManager.RemoveInvoker(EventName.BossDeathEvent, this);
            EventManager.RemoveInvoker(EventName.EnemyDeath, this);
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
    /// Boss attacks player called by boss animator
    /// </summary>
    /// <param name="num">projectile being fired</param>
    /// <param name="angleStep">angle from start point</param>
    /// <param name="radius">radius of spawn circle</param>
    /// <param name="startPos">start position of circle</param>
    public override void AttackPlayer(int num, float angleStep, float radius, Vector3 startPos)
    {
        // Calculate position based on angle
        float angle = num * angleStep;

        // Spawn position
        Vector3 spawnPosition = projectileTransform.position + Quaternion.Euler(0, 0, angle) * startPos * radius;

        // Instantiate the projectile
        GameObject proj = pool.GetRedBossProjectile(); //Instantiate(projectile, spawnPosition, Quaternion.identity);
        proj.transform.position = spawnPosition;
        proj.SetActive(true);

        // Calculate direction to shoot
        Vector3 shootDirection = (proj.transform.position - projectileTransform.position).normalized;
        Vector2 direction = new Vector2(shootDirection.x, shootDirection.y);

        proj.GetComponent<Rigidbody2D>().velocity = direction * projForce;
    }

    /// <summary>
    /// Destroys enemy when they hit the boss wall
    /// </summary>
    /// <param name="coll"></param>
    protected override void OnCollisionEnter2D(Collision2D coll)
    {
        return;
    }

    /// <summary>
    /// Handles damage when seed collides
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D (collision);
    }

    /// <summary>
    /// Spawns a random chest on death
    /// </summary>
    protected override void SpawnRandomPickup()
    {
        // Instantiate orb spawner
        Instantiate(orbSpawner, transform.position, Quaternion.identity);

        /*
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
        

        // Chance to drop a heart
        if (Random.Range(0f, 1f) >= 0.95f)
        {
            force = new Vector2(Random.Range(-2f, 4f), Random.Range(-2f, 2f));
            GameObject heartObj = Instantiate(heart, transform.position, Quaternion.identity);
            heartObj.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
        }


        // Get number or experience orbs
        int num = 0;
        // Get loot pool
        float numChance = Random.Range(0f, 1f);
        // 50% chance for one orb
        if (numChance <= 0.50)
        {
            num = 14;
        }
        // 26% chance for two orbs
        else if (numChance <= 0.76)
        {
            num = 18;
        }
        // 16% chance for three orbs
        else if (numChance <= 0.92)
        {
            num = 22;
        }
        // 6% chance for four orbs
        else if (numChance <= 0.98)
        {
            num = 26;
        }
        // 2% chance for 5 orbs
        else if (numChance <= 1.0)
        {
            num = 30;
        }

        // Spawn num amount of orbs with chance for different sizes
        for (int i = 1; i <= num; i++)
        {
            // Get random force for movement
            force = new Vector2(Random.Range(-2f, 4f), Random.Range(-2f, 2f));

            float sizeChance = Random.Range(0f, 1f);
            {
                // 30% chance for small orb
                if (sizeChance <= 0.30)
                {
                    GameObject orb = Instantiate(expOrbs[0], transform.position, Quaternion.identity);
                    orb.GetComponent<Rigidbody2D>().AddForce(force * 2, ForceMode2D.Impulse);
                }
                // 26% chance for medium orb
                else if (sizeChance <= 0.56)
                {
                    GameObject orb = Instantiate(expOrbs[1], transform.position, Quaternion.identity);
                    orb.GetComponent<Rigidbody2D>().AddForce(force * 2, ForceMode2D.Impulse);
                }
                // 18% chance for large orb
                else if (sizeChance <= 0.74)
                {
                    GameObject orb = Instantiate(expOrbs[2], transform.position, Quaternion.identity);
                    orb.GetComponent<Rigidbody2D>().AddForce(force * 2, ForceMode2D.Impulse);
                }
                // 12% chance for xlarge orb
                else if (sizeChance <= 0.86)
                {
                    GameObject orb = Instantiate(expOrbs[3], transform.position, Quaternion.identity);
                    orb.GetComponent<Rigidbody2D>().AddForce(force * 2, ForceMode2D.Impulse);
                }
                // 8% chance for xxlarge orb
                else if (sizeChance <= 0.94)
                {
                    GameObject orb = Instantiate(expOrbs[4], transform.position, Quaternion.identity);
                    orb.GetComponent<Rigidbody2D>().AddForce(force * 2, ForceMode2D.Impulse);
                }
                // 6% chance for xxxlargeorb
                else if (sizeChance <= 1.0)
                {
                    GameObject orb = Instantiate(expOrbs[5], transform.position, Quaternion.identity);
                    orb.GetComponent<Rigidbody2D>().AddForce(force * 2, ForceMode2D.Impulse);
                }
            }
        }
        */

    }

    /// <summary>
    /// Updates player crit chance
    /// </summary>
    protected override void HandleCritChanceModChanged()
    {
        base.HandleCritChanceModChanged();
    }

    /// <summary>
    /// Updates player damage
    /// </summary>
    protected override void HandleDamageModChanged()
    {
        base.HandleDamageModChanged();
    }

    /// <summary>
    /// Updates seedling damage
    /// </summary>
    protected override void HandleSeedlingDamageModChanged()
    {
        base.HandleSeedlingDamageModChanged();
    }

    /// <summary>
    /// Updates thorn damage
    /// </summary>
    protected override void HandleThornDamageModChanged()
    {
        base.HandleThornDamageModChanged();
    }
}
