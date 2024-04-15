using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// pickup behavior
/// </summary>
public class Pickup : MonoBehaviour
{
    Rigidbody2D rb;
    GameObject player;
    Vector2 direction;
    float speed = 5;
    bool playerNear;
    bool playerDead = false;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Get rigid body
        rb = GetComponent<Rigidbody2D>();

        // Using a try block for when a pickup spawns as the player is dying
        try
        {
            // Get player reference
            player = GameObject.FindWithTag("Player");
        }
        catch (System.Exception)
        {
            playerDead = true;
        }

        // Add as listener for player death
        EventManager.AddListener(EventName.PlayerDeathEvent, StopLookingForPlayer);

    }


    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Using try block for when player dies as pickup spawns
        try
        {
            if (!playerDead)
            {
                // Check if player is in range
                if (Vector3.Distance(player.transform.position, gameObject.transform.position) <= ConfigUtils.PickupRange)
                {
                    playerNear = true;
                }
                else
                {
                    playerNear = false;
                }
            }
        }
        catch (System.Exception)
        {
        }

    }

    private void FixedUpdate()
    {
        if (playerNear && !playerDead)
        {
            // Check if object is in the process of being collated
            if (gameObject.GetComponent<Collider2D>().enabled == true)
            {
                MoveToPlayer();
            }
        }
    }

    private void MoveToPlayer()
    {
        // Get direction to move
        Vector3 directionToPlayer = (player.transform.position - gameObject.transform.position).normalized;

        direction.x = directionToPlayer.x;
        direction.y = directionToPlayer.y;

        // Move to player
        if (direction.x != 0 || direction.y != 0)
        {
            rb.velocity = direction * speed;
        }
    }

    /// <summary>
    /// Stops checking if player near when player dies
    /// </summary>
    private void StopLookingForPlayer()
    {
        playerDead = true;
    }
}
