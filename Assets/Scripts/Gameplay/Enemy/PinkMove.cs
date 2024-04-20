using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Pink move child class of enemy move
    /// </summary>
public class PinkMove : EnemyMove
{

    protected override void Awake()
    {
        base.Awake();
    }

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


    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    /// <summary>
    /// Move in a random direction
    /// </summary>
    protected override void RandomMovement()
    {
        base.RandomMovement();
    }

    /// <summary>
    /// Move towards the player
    /// </summary>
    protected override void MoveToPlayer()
    {
        base.MoveToPlayer();
    }

    /// <summary>
    /// Move away from the player
    /// </summary>
    protected override void MoveAwayFromPlayer()
    {
        base.MoveAwayFromPlayer();
    }

    /// <summary>
    /// Change to random movement on player death
    /// </summary>
    protected override void PlayerDeathBehavior()
    {
        base.PlayerDeathBehavior();
    }

    /// <summary>
    /// Handles behavior when boss spawns
    /// </summary>
    protected override void HandleBossSpawned()
    {
        base.HandleBossSpawned();
    }

    /// <summary>
    /// Handle behavior when boss dies
    /// </summary>
    protected override void HandleBossDeath()
    {
        base.HandleBossDeath();
    }
}
