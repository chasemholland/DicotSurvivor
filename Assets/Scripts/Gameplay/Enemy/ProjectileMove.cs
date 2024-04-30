using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Boss projectile behaviour
    /// </summary>
public class ProjectileMove : MonoBehaviour
{
    private GameObject player;
    public float force;
    [SerializeField]
    GameObject explosion;
    ObjectPool pool;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Get reference to object pool
        pool = GameObject.FindGameObjectWithTag("SeedBank").GetComponent<ObjectPool>();

        // Set defaults
        force = ConfigUtils.PlayerSeedSpeed;

        // Get refence to player
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            if (gameObject.name.StartsWith("Cyan"))
            {
                pool.ReturnCyanProjectile(gameObject);
            }
            else
            {
                pool.ReturnRedBossProjectile(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall") || 
            collision.gameObject.CompareTag("BossWall") || collision.gameObject.CompareTag("Seedling"))
        {
            if (gameObject.name.StartsWith("Cyan"))
            {
                // Get projectile explosion
                GameObject exp = pool.GetCyanProjectileExplosion();
                exp.transform.position = transform.position;
                exp.SetActive(true);

                // Return projectile to pool
                pool.ReturnCyanProjectile(gameObject);
            }
            else
            {
                // Get projectile explosion
                GameObject exp = pool.GetRedBossProjectileExplosion();
                exp.transform.position = transform.position;
                exp.SetActive(true);

                // Return projectile to pool
                pool.ReturnRedBossProjectile(gameObject);
            }
        }
    }

}
