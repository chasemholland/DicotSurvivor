using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    ///
    /// </summary>
public class ProjectileMove : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
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

        // Get reference to main camera and rigidbody2d
        rb = GetComponent<Rigidbody2D>();

        // Get refence to player
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Destroy(gameObject);
        }
        else
        {
            // Get direction of movement
            Vector3 direction = player.transform.position - transform.position;
            direction = new Vector3(Random.Range(direction.x - 2f, direction.x + 2f), Random.Range(direction.y - 2f, direction.y + 2f), 0);
            rb.velocity = new Vector2(direction.x, direction.y).normalized * force;
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
