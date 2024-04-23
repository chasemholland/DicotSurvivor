using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Seedling seed shooter
    /// </summary>
public class SeedlingShooter : MonoBehaviour
{
    Animator animator;
    GameObject targetEnemy = null;
    [SerializeField]
    GameObject seed;
    float seedForce;
    [SerializeField]
    Transform seedTransform;
    Timer shootTimer;
    float cooldown;
    bool flowerVisible = false;
    

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Get refrence to seedling animator
        animator = gameObject.GetComponentInParent<Animator>();

        // Set flower invisible on start
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;

        // Set collider disabled on start
        animator.GetComponent<Collider2D>().enabled = false;

        // Set defaults
        cooldown = ConfigUtils.SeedlingROF - Mod.ActiveModifiers["SeedlingROFMod"];
        seedForce = ConfigUtils.PlayerSeedSpeed;

        // Add as listener for choot cooldown mod change
        EventManager.AddListener(EventName.SeedlingROFMod, HandleSeedlingROFModChanged);

        // Add as listener for seed speed changed
        EventManager.AddListener(EventName.SeedSpeedMod, HandleSeedSpeedModChanged);

        // Set up shoot timer
        shootTimer = gameObject.AddComponent<Timer>();
        shootTimer.Duration = cooldown;
        shootTimer.AddTimerFinishedListener(HandleShootTimerFinished);
        shootTimer.Run();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Check for transition from seedling grow to seedling idle
        if (!flowerVisible)
        {
            if (animator.IsInTransition(0))
            {
                // Set flower visible
                gameObject.GetComponentInChildren<SpriteRenderer>().enabled = true;

                // Set collider disabled on start
                animator.GetComponent<Collider2D>().enabled = true;

                flowerVisible = true;
            }
        }

        // Dont track mouse if game paused
        if (Time.timeScale != 0)
        {
            if (targetEnemy != null)
            {
                // Using a try block just in case player kills enemy during targeting
                try
                {
                    // Send values to animator to update corect animation
                    Vector3 faceDirection = targetEnemy.transform.position - transform.position;
                    animator.SetFloat("X", faceDirection.x);
                    animator.SetFloat("Y", faceDirection.y);

                    // Get direction of rotation
                    Vector3 rotation = targetEnemy.transform.position - seedTransform.position;

                    // Get rotation in degrees
                    float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

                    // Set rotation
                    transform.rotation = Quaternion.Euler(0, 0, rotationZ);
                }
                catch (System.Exception)
                {
                    targetEnemy = null;
                }
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        // If there is no target
        if (targetEnemy == null)
        { 
            // If trigger is an enemy
            if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("RedBoss"))
            {
                // Set target
                targetEnemy = collision.gameObject;
            }
        }
    }

    private void HandleShootTimerFinished()
    {
        if (targetEnemy != null)
        {
            // Shoot enemy
            GameObject seedObject = Instantiate(seed, seedTransform.position, Quaternion.identity);

            // Using a try block just in case player kills enemy during targeting
            try
            {
                // Get direction of movement
                Vector3 direction = targetEnemy.transform.position - transform.position;
                Vector3 rotation = transform.position - targetEnemy.transform.position;

                // Rotate the seed in direction of travel and set velocity
                float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
                seedObject.transform.rotation = Quaternion.Euler(0, 0, rot + 90);
                seedObject.GetComponent<Rigidbody2D>().velocity = new Vector2(direction.x, direction.y).normalized * seedForce;

                // Play shoot sound after succesfully shooting
                AudioManager.Play(AudioName.Shoot);
            }
            catch (System.Exception)
            {
                // If seed hasn't been detroyed by enemy then destroy the seed
                if (seedObject != null)
                {
                    Destroy(seedObject);
                }
            }
        }

        shootTimer.Duration = cooldown;
        shootTimer.Run();
    }

    /// <summary>
    /// Handles shoot cooldown mod chnage
    /// </summary>
    private void HandleSeedlingROFModChanged()
    {
        cooldown = Mathf.Clamp(ConfigUtils.SeedlingROF - Mod.ActiveModifiers["SeedlingROFMod"], 0.1f, ConfigUtils.SeedlingROF);
    }

    /// <summary>
    /// Handles seed speed mod changed
    /// </summary>
    private void HandleSeedSpeedModChanged()
    {
        seedForce = ConfigUtils.PlayerSeedSpeed + Mod.ActiveModifiers["SeedSpeedMod"];
    }
}
