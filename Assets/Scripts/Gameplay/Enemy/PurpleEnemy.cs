using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Purple enemy child class of enemy
    /// </summary>
public class PurpleEnemy : Enemy
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
        // Destroy particle system trail
        Destroy(gameObject.GetComponentInChildren<ParticleSystem>());

        base.HandleStep();
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
