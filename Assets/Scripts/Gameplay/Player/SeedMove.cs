using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

    /// <summary>
    /// Seed (bullet) bahavior
    /// </summary>
public class SeedMove : EventInvoker
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
            if (gameObject.name.StartsWith("Small"))
            {
                elapsedSeconds = 0;
                pool.ReturnSmallSeed(gameObject);
            }
            else
            {
                elapsedSeconds = 0;
                pool.ReturnSeed(gameObject);
            }
        }
    }

    /// <summary>
    /// Handles collision with any enemys
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject coll = collision.gameObject;

        // Try and spawn seedling if mutation active
        if (coll.CompareTag("Enemy") || coll.CompareTag("RedBoss"))
        {
            // Get explosion from pool
            GameObject explosion = pool.GetExplosion();
            explosion.transform.position = gameObject.transform.position;
            explosion.SetActive(true);

            // Return seed to pool
            if (gameObject.name.StartsWith("Small"))
            {
                pool.ReturnSmallSeed(gameObject);
            }
            else
            {
                pool.ReturnSeed(gameObject);
            }
        }

        if (coll.CompareTag("Wall") || coll.CompareTag("BossWall") || coll.CompareTag("Player"))
        {
            // Return seed to pool
            if (gameObject.name.StartsWith("Small"))
            {
                pool.ReturnSmallSeed(gameObject);
            }
            else
            {
                pool.ReturnSeed(gameObject);
            }
        }
    }
}
