using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Rendering;

/// <summary>
/// Boss movement child class of enemy move
/// </summary>
public class BossMove : EnemyMove
{
    // Boss specific field to track when intro animation is done
    bool introDone = false;

    protected override void Awake()
    {
        base.Awake();
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    protected override void Start()
    {
        // Get player reference
        player = GameObject.FindWithTag("Player");

        // Add as listener for player death event
        EventManager.AddListener(EventName.PlayerDeathEvent, PlayerDeathBehavior);

        // Add random movement timer
        randMove = gameObject.AddComponent<Timer>();
        randMove.Duration = 0.5f;
        randMove.AddTimerFinishedListener(RandomMovement);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    protected override void Update()
    {
        if (!introDone)
        {
            introDone = animator.GetBool("introDone");
        }
        
        if (animator.GetBool("isAttacking"))
        {
            direction = Vector2.zero;
            return;
        }

        if (!playerDead && introDone)
        {
            // Move towards player
            MoveToPlayer();
        }
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
    /// Change to random movement on player death
    /// </summary>
    protected override void PlayerDeathBehavior()
    {
        base.PlayerDeathBehavior();
    }
}
