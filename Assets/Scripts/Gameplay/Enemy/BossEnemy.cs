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
    List<GameObject> chests;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    protected override void Start()
    {
        base.Start();
        
        //-------- Boss Related ----------------------------------------------------

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
        GameObject proj = Instantiate(projectile, spawnPosition, Quaternion.identity);

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
