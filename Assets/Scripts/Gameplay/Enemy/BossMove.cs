using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Boss movement
    /// </summary>
public class BossMove : FloatEventInvoker
{
    // Direction of moevement
    public Vector2 direction;
    // Player rigid body
    private Rigidbody2D rb;
    // Movement speed
    private int speed = 1;
    // Player animator
    private Animator animator;
    // Player reference
    GameObject player;
    // Bool to change movement on player death
    bool playerDead = false;
    // Bool for intro animation
    bool introDone = false;
    // Random movement timer when player dies
    Timer randMove;


    private void Awake()
    {
        // Get reference to player and player animator
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Get player reference
        player = GameObject.FindWithTag("Player");

        // Add as listener for player death event
        EventManager.AddFloatListener(FloatEventName.PlayerDeathEvent, PlayerDeathBehavior);

        // Add random movement timer
        randMove = gameObject.AddComponent<Timer>();
        randMove.Duration = 0.5f;
        randMove.AddTimerFinishedListener(RandomMovement);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (!introDone)
        {
            introDone = animator.GetBool("introDone");
        }

        if (!playerDead && introDone)
        {
            // Move towards player
            MoveToPlayer();
        }
    }

    private void FixedUpdate()
    {
        if (direction.x != 0 || direction.y != 0)
        {
            rb.velocity = direction * (speed * Tracker.EnemyMoveMod);
        }
    }

    /// <summary>
    /// Move in a random direction
    /// </summary>
    private void RandomMovement()
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
    private void MoveToPlayer()
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

    /// <summary>
    /// Change to random movement on player death
    /// </summary>
    /// <param name="n">unused</param>
    private void PlayerDeathBehavior(float n)
    {
        playerDead = true;
        randMove.Run();
    }
}
