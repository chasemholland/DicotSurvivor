using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.InputSystem;

/// <summary>
/// Handle player movement
/// </summary>
public class PlayerMove : MonoBehaviour
{
    // Direction of moevement
    public Vector2 direction;
    // Player rigid body
    private Rigidbody2D rb;
    // Movement speed
    private float speed;
    // Player animator
    private Animator animator;


    private void Awake()
    {
        // Set defaults
        speed = ConfigUtils.PlayerMoveSpeed;

        // Get reference to player and player animator
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Input manager method uses "On" plus Action name "Movement" to reference the action
    private void OnMovement(InputValue value)
    {
        // Don't change animation if game paused
        if (Time.timeScale != 0)
        {
            // Get direction
            direction = value.Get<Vector2>();
        }

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

    private void FixedUpdate()
    {
        //rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
        if (direction.x != 0 || direction.y != 0)
        {
            rb.velocity = direction * speed;
        }
    }
}
