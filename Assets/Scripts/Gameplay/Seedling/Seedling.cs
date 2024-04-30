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
    Timer damageFlash;
    float flashDuration = 0.1f;
    int flashes = 0;
    Color transparent;
    Color nonTransparent;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Set health
        health = ConfigUtils.SeedlingHealth + Mod.ActiveModifiers["SeedlingHealthMod"];

        // Set up damage cooldown timer
        damageFlash = gameObject.AddComponent<Timer>();
        damageFlash.AddTimerFinishedListener(FlashEffect);
        damageFlash.Duration = flashDuration;

        transparent = new Color(1, 1, 1, 0.5f);
        nonTransparent = new Color(1, 1, 1, 1);
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

        if (vulnerable)
        {
            if (collision.gameObject.CompareTag("Projectile"))
            {
                HandleDamage(collision.gameObject);
                return;
            }
        }
    }

    // Handles taking damage
    private void HandleDamage(GameObject collision)
    {
        // Reduce health
        health -= 1f;

        // Go invulnerable for some time
        vulnerable = false;
        damageFlash.Run();

        // Check if dead
        if (health <= 0)
        {
            // Destroy the seedling
            Destroy(gameObject);
        }
    }

    private void FlashEffect()
    {
        if (gameObject.GetComponent<SpriteRenderer>().color == nonTransparent)
        {
            gameObject.GetComponent<SpriteRenderer>().color = transparent;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = nonTransparent;
        }

        flashes++;

        if (flashes > 8 && gameObject.GetComponent<SpriteRenderer>().color == nonTransparent)
        {
            flashes = 0;
            vulnerable = true;
            damageFlash.Stop();
        }
        else
        {
            damageFlash.Duration = flashDuration;
            damageFlash.Run();
        }

    }
}
