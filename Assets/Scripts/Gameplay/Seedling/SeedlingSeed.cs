using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Seedling seed behaviour
    /// </summary>
public class SeedlingSeed : MonoBehaviour
{
    [SerializeField]
    GameObject explosion;

    /// <summary>
    /// Handles collision with any enemys
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject coll = collision.gameObject;
        if (coll.CompareTag("Enemy") || coll.CompareTag("RedBoss") || coll.CompareTag("Wall") || coll.CompareTag("BossWall") || coll.CompareTag("Player"))
        {
            // Spawn seed explosion animation
            Instantiate(explosion, transform.position, Quaternion.identity);

            // Destroy the game object
            Destroy(gameObject);
        }
    }
}
