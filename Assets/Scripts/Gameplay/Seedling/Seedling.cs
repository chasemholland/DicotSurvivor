using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

    /// <summary>
    ///
    /// </summary>
public class Seedling : MonoBehaviour
{
    float health;
    bool vulnerable = true;
    Timer damageCooldown;
    float cooldown = 1;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Set health
        health = ConfigUtils.SeedlingHealth + Mod.ActiveModifiers["SeedlingHealthMod"];

        // Set up damage cooldown timer
        damageCooldown = gameObject.AddComponent<Timer>();
        damageCooldown.AddTimerFinishedListener(UpdateVulnerability);
        damageCooldown.Duration = cooldown;
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (vulnerable)
        {
            if (coll.gameObject.CompareTag("Enemy") || coll.gameObject.CompareTag("RedBoss"))
            {
                HandleDamage(coll.gameObject);
                return;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Get name of game object
        string t = collision.gameObject.tag;

        // Ignor triggers
        if (collision.gameObject.CompareTag("CollectionField") || collision.gameObject.CompareTag("DetectionField") ||
            collision.gameObject.CompareTag("Seed") || collision.gameObject.CompareTag("Thorn"))
        {
            return;
        }

        if (collision.gameObject.CompareTag("Projectile"))
        {
            HandleDamage(collision.gameObject);
            return;
        }
    }

    // Handles taking damage
    private void HandleDamage(GameObject collision)
    {
        // Reduce health
        health -= 1f;

        // Go invulnerable for some time
        vulnerable = false;
        damageCooldown.Run();

        // Check if dead
        if (health <= 0)
        {
            // Destroy the seedling
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Makes player vulnerable after cooldown
    /// </summary>
    private void UpdateVulnerability()
    {
        vulnerable = true;
        damageCooldown.Duration = cooldown;
    }
}
