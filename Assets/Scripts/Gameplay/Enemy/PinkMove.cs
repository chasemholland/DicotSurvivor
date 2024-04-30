using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;

    /// <summary>
    /// Pink move child class of enemy move
    /// </summary>
public class PinkMove : EnemyMove
{

    // Spawning flag for small and mini slime
    bool spawning = false;
    bool archLeft = false;
    bool archRight = false;
    Vector3 startPos;
    Vector3 endPosRight;
    Vector3 endPosLeft;
    float elapsedTime = 0;
    float duration = 0.5f;
    float height = 1.5f;


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

        // Get faster if small pink slime
        if (gameObject.name.StartsWith("Small"))
        {
            speed += Random.Range(0.1f, 0.6f);
            spawning = true;
            startPos = gameObject.transform.position;
            endPosLeft = new Vector3(gameObject.transform.position.x - 2f, gameObject.transform.position.y, 0);
            endPosRight = new Vector3(gameObject.transform.position.x + 2f, gameObject.transform.position.y, 0);
        }

        // Get even faster if mini pink slime
        if (gameObject.name.StartsWith("Mini"))
        {
            speed += Random.Range(0.6f, 1.0f);
            spawning = true;
            startPos = gameObject.transform.position;
            endPosLeft = new Vector3(gameObject.transform.position.x - 1f, gameObject.transform.position.y, 0);
            endPosRight = new Vector3(gameObject.transform.position.x + 1f, gameObject.transform.position.y, 0);
        }

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    protected override void Update()
    {
        // Perform spawn arch
        if (spawning)
        {
            if (archLeft)
            {
                ArchLeft();
            }
            else if (archRight)
            {
                ArchRight();
            }
        }

        // Stop walk movement if attacking
        if (animator.GetBool("isAttacking"))
        {
            direction = Vector2.zero;
            return;
        }

        if (playerDead)
        {
            // Dont update movement on player death -- Random movement is on a timer
            return;
        }

        if (!playerDead && !bossActive && !spawning)
        {
            // Move towards player
            MoveToPlayer();
        }

        if (bossActive)
        {
            MoveAwayFromPlayer();
        }
    }


    protected override void FixedUpdate()
    {
        if (spawning)
        {
            return;
        }
        else if (direction.x != 0 || direction.y != 0)
        {
            rb.velocity = direction * speed;
        }
        else
        {
            rb.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Set direction to arch in
    /// </summary>
    /// <param name="dir"></param>
    public void SetArchDirection(string dir)
    {
        switch (dir)
        {
            case "left":
                archLeft = true;
                break;
            case "right": 
                archRight = true;
                break;
            default:
                break;
        }

    }

    /// <summary>
    /// Spawn arch going to the right
    /// </summary>
    public void ArchRight()
    {
        if (elapsedTime < duration)
        {
            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate t value for movement along the arch
            float t = elapsedTime / duration;

            // Calculate sine wave for smooth arch movement
            float archMovement = Mathf.Sin(t * Mathf.PI);

            // Interpolate between start and end positions with the sine wave
            Vector3 archPosition = Vector3.Lerp(startPos, endPosRight, t) +
                                   Vector3.up * archMovement * height;

            // Update object's position
            transform.position = archPosition;
        }
        else
        {
            // Ensure the object ends up at the exact end position when the duration is reached
            transform.position = endPosRight;
            spawning = false;
        }
    }

    /// <summary>
    /// Spawn arch goig to the left
    /// </summary>
    public void ArchLeft()
    {
        if (elapsedTime < duration)
        {
            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            // Calculate t value for movement along the arch
            float t = elapsedTime / duration;

            // Calculate sine wave for smooth arch movement
            float archMovement = Mathf.Sin(t * Mathf.PI);

            // Interpolate between start and end positions with the sine wave
            Vector3 archPosition = Vector3.Lerp(startPos, endPosLeft, t) +
                                   Vector3.up * archMovement * height;

            // Update object's position
            transform.position = archPosition;
        }
        else
        {
            // Ensure the object ends up at the exact end position when the duration is reached
            transform.position = endPosLeft;
            spawning = false;
        }
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
