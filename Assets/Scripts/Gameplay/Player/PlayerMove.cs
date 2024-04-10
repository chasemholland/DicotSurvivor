using System;
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
        speed = ConfigUtils.PlayerMoveSpeed + Mod.ActiveModifiers["MoveSpeedMod"];

        // Add as listener for move speed mod changed
        EventManager.AddFloatListener(FloatEventName.MoveSpeedMod, HandleMoveSpeedModChanged);

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
        if (direction.x != 0 || direction.y != 0)
        {
            rb.velocity = direction * speed;
        }
    }

    /// <summary>
    /// Updates speed on move speed mod change
    /// </summary>
    /// <param name="n">unused</param>
    private void HandleMoveSpeedModChanged(float n)
    {
        speed = ConfigUtils.PlayerMoveSpeed + Mod.ActiveModifiers["MoveSpeedMod"];
    }
}
