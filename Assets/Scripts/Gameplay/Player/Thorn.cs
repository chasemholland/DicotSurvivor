using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    ///
    /// </summary>
public class Thorn : MonoBehaviour
{
    [SerializeField]
    GameObject explosion;
    ObjectPool pool;

    private void Start()
    {
        pool = GameObject.FindGameObjectWithTag("SeedBank").GetComponent<ObjectPool>();
    }

    /// <summary>
    /// Handles collision with any enemys
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject coll = collision.gameObject;

        // Try and spawn seedling if mutation active
        if (coll.CompareTag("Enemy") || coll.CompareTag("RedBoss") || coll.CompareTag("Wall") || coll.CompareTag("BossWall"))
        {
            // Get thorn explosion from pool
            GameObject exp = pool.GetThornExplosion();
            exp.transform.position = transform.position;
            exp.SetActive(true);

            // Return thorn to pool
            pool.ReturnThorn(gameObject);
        }
    }
}
