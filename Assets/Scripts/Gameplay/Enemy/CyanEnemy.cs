using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Cyan enemy child class of enemy
    /// </summary>
public class CyanEnemy : Enemy
{
    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    protected override void Start()
    {
        base.Start();
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
        base.HandleStep();
    }

    /// <summary>
    /// Attacks player when called by animator
    /// </summary>
    public override void AttackPlayer()
    {
        // Get camera bounds
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(bottomLeftCam);
        Vector3 topRight = Camera.main.ViewportToWorldPoint(topRightCam);

        // Check if enemy is in view before shooting
        if (gameObject.transform.position.y > bottomLeft.y && gameObject.transform.position.y < topRight.y)
        {
            if (gameObject.transform.position.x > bottomLeft.x && gameObject.transform.position.x < topRight.x)
            {
                // Done in try block if player dies while attacking
                try
                {
                    // Get direction to player
                    player = GameObject.FindGameObjectWithTag("Player");
                    Vector3 directionToPlayer = (player.transform.position - projectileTransform.position).normalized;
                    Vector2 direction = new Vector2(directionToPlayer.x, directionToPlayer.y);

                    GameObject proj = Instantiate(projectile, projectileTransform.position, Quaternion.identity);
                    proj.GetComponent<Rigidbody2D>().velocity = direction * projForce;
                }
                catch (System.Exception)
                {
                    return;
                }
            }
        }
    }

    /// <summary>
    /// Destroys enemy when they hit the boss wall
    /// </summary>
    /// <param name="coll"></param>
    protected override void OnCollisionEnter2D(Collision2D coll)
    {
        base.OnCollisionEnter2D(coll);
    }

    /// <summary>
    /// Handles damage when seed collides
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    /// <summary>
    /// Spawns random sizes and amounts of pickups
    /// </summary>
    protected override void SpawnRandomPickup()
    {
        base.SpawnRandomPickup();
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
