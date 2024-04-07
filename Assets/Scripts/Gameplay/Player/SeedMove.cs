using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

    /// <summary>
    /// Seed (bullet) bahavior
    /// </summary>
public class SeedMove : FloatEventInvoker
{
    private Vector3 mousePosition;
    private Camera mainCamera;
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
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        rb = GetComponent<Rigidbody2D>();

        // Get mouse position
        mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Get direction of movement
        Vector3 direction = mousePosition - transform.position;
        Vector3 rotation = transform.position - mousePosition;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        // Rotate the seed in direction of travel
        float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    /// <summary>
    /// Handles collision with any colliders
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Spawn seed explosion animation
        Instantiate(explosion, transform.position, Quaternion.identity);

        // Destroy the game object
        Destroy(gameObject);
    }
}
