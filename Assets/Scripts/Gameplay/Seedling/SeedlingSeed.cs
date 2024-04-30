using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Seedling seed behaviour
    /// </summary>
public class SeedlingSeed : MonoBehaviour
{
    // Object pool for seeds and explosions
    ObjectPool pool;

    // Refernce to cinemachine
    GameObject cam;
    float elapsedSeconds = 0;

    private void Start()
    {
        pool = GameObject.FindGameObjectWithTag("SeedBank").GetComponent<ObjectPool>();

        // Get reference to cinemachine
        cam = GameObject.FindGameObjectWithTag("Follower");

    }

    private void Update()
    {
        elapsedSeconds += Time.deltaTime;
        if (elapsedSeconds >= 2)
        {
            CheckForReturn();
            elapsedSeconds = 0;
        }
    }

    private void CheckForReturn()
    {
        if (Vector2.Distance(cam.transform.position, gameObject.transform.position) > 25)
        {
            // Return seed to pool
            elapsedSeconds = 0;
            pool.ReturnSeedlingSeed(gameObject);
        }
    }

    /// <summary>
    /// Handles collision with any enemys
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject coll = collision.gameObject;
        if (coll.CompareTag("Enemy") || coll.CompareTag("RedBoss"))
        {
            // Get seed explosion from pool
            GameObject explosion = pool.GetExplosion();
            explosion.transform.position = gameObject.transform.position;
            explosion.SetActive(true);

            // Return seed to pool
            pool.ReturnSeedlingSeed(gameObject);
        }

        if (coll.CompareTag("Wall") || coll.CompareTag("BossWall") || coll.CompareTag("Player"))
        {
            // Return seed to pool
            pool.ReturnSeedlingSeed(gameObject);
        }
    }
}
