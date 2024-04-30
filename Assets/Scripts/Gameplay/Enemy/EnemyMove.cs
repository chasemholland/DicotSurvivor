using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Handles enemy movement
/// </summary>
public class EnemyMove : MonoBehaviour
{
    // Direction of moevement
    protected Vector2 direction;
    // Player rigid body
    protected Rigidbody2D rb;
    // Movement speed
    protected float speed = 1;
    // Player animator
    protected Animator animator;
    // Player reference
    protected GameObject player;
    // Bool to change movement on player death
    protected bool playerDead = false;
    // Bool to change movement on boss spawn
    protected bool bossActive = false;
    // Random movement timer when player dies
    protected Timer randMove;
    protected float killTime;


    protected virtual void Awake()
    {
        // Get reference to player and player animator
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    protected virtual void Start()
    {
        // Get player reference
        player = GameObject.FindWithTag("Player");

        // Add as listener for player death event
        EventManager.AddListener(EventName.PlayerDeathEvent, PlayerDeathBehavior);

        // Add as listener for boss spawned and boss death event
        EventManager.AddListener(EventName.BossSpawnedEvent, HandleBossSpawned);
        EventManager.AddListener(EventName.BossDeathEvent, HandleBossDeath);

        // Add random movement timer
        randMove = gameObject.AddComponent<Timer>();
        randMove.Duration = 0.5f;
        randMove.AddTimerFinishedListener(RandomMovement);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    protected virtual void Update()
    {
        if (playerDead)
        {
            // Dont update movement on player death -- Random movement is on a timer
            return;
        }

        if (!playerDead && !bossActive)
        {
            // Move towards player
            MoveToPlayer();          
        }
 
        if (bossActive)
        {
            killTime += Time.deltaTime;
            if (killTime >= 4)
            {
                // Destroy enemy to avoid enemy stacking from boss encounters
                Destroy(gameObject);
            }
            else
            {
                MoveAwayFromPlayer();
            }   
        }
    }

    protected virtual void FixedUpdate()
    {
        if (direction.x != 0 || direction.y != 0)
        {
            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Move in a random direction
    /// </summary>
    protected virtual void RandomMovement()
    {
        float x;
        float y;

        // Decide if walking --> Get direction if yes
        if (Random.Range(0.0f, 1.0f) > 0.5f)
        {
            x = Random.Range(-1.0f, 1.0f);
            y = Random.Range(-1.0f, 1.0f);
        }
        else
        {
            x = 0;
            y = 0;
        }
        direction = new Vector2(x, y);

        // Send values to animator to update corect animation
        if (direction.x != 0 || direction.y != 0)
        {
            animator.SetFloat("X", direction.x);
            animator.SetFloat("Y", direction.y);

            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        // Reset timer
        randMove.Duration = 0.5f;
        randMove.Run();
    }

    /// <summary>
    /// Move towards the player
    /// </summary>
    protected virtual void MoveToPlayer()
    {
        // If try fails, player is dead -> force random movement
        try
        {
            Vector3 directionToPlayer = (player.transform.position - gameObject.transform.position).normalized;

            direction.x = directionToPlayer.x;
            direction.y = directionToPlayer.y;

            // Send values to animator to update corect animation
            if (direction.x != 0 || direction.y != 0)
            {
                animator.SetFloat("X", direction.x);
                animator.SetFloat("Y", direction.y);

                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        catch (System.Exception)
        {
            playerDead = true;
            randMove.Run();
        }
        
    }

    /// <summary>
    /// Move away from the player
    /// </summary>
    protected virtual void MoveAwayFromPlayer()
    {
        // If try fails, player is dead -> force random movement
        try
        {
            Vector3 directionToPlayer = (player.transform.position - gameObject.transform.position).normalized;

            // Set direction negative
            direction.x = -directionToPlayer.x;
            direction.y = -directionToPlayer.y;

            // Send values to animator to update corect animation
            if (direction.x != 0 || direction.y != 0)
            {
                animator.SetFloat("X", direction.x);
                animator.SetFloat("Y", direction.y);

                animator.SetBool("isWalking", true);
            }
            else
            {
                animator.SetBool("isWalking", false);
            }
        }
        catch (System.Exception)
        {
            playerDead = true;
            randMove.Run();
        }
        
    }

    /// <summary>
    /// Change to random movement on player death
    /// </summary>
    protected virtual void PlayerDeathBehavior()
    {
        playerDead = true;
        randMove.Run();
    }

    /// <summary>
    /// Handles behavior when boss spawns
    /// </summary>
    protected virtual void HandleBossSpawned()
    {
        bossActive = true;
        speed = 6;
    }

    /// <summary>
    /// Handle behavior when boss dies
    /// </summary>
    protected virtual void HandleBossDeath()
    {
        bossActive = false;
        speed  = 1;
    }
}
