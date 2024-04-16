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

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Set defaults
        force = ConfigUtils.PlayerSeedSpeed;

        // Get refence to player
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Wall") || collision.gameObject.CompareTag("BossWall"))
        {
            // Spawn projectile explosion animation
            Instantiate(explosion, transform.position, Quaternion.identity);

            // Destroy the game object
            Destroy(gameObject);
        }
    }

}
