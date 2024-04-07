using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the flower (seed shooter) behavior
/// </summary>
public class SeedShooter : FloatEventInvoker
{
    private Camera mainCamera;
    private Vector3 mousePosition;
    [SerializeField]
    GameObject seed;
    [SerializeField]
    Transform seedTransform;
    private bool singleShot = true;
    Timer shootTimer;
    float cooldown;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Set defaults
        cooldown = ConfigUtils.PlayerShootCooldown;

        // Get reference to main camera
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

        // Set up shoot timer
        shootTimer = gameObject.AddComponent<Timer>();
        shootTimer.Duration = cooldown;
        shootTimer.AddTimerFinishedListener(HandleShootTimerFinished);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // Dont track mouse if game paused
        if (Time.timeScale != 0)
        {
            // Get mouse position
            mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

            // Get direction of rotation
            Vector3 rotation = mousePosition - transform.position;

            // Get rotation in degrees
            float rotationZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

            // Set rotation
            transform.rotation = Quaternion.Euler(0, 0, rotationZ);

            // Check for single click fires
            if (singleShot && Input.GetMouseButton(0))
            {
                Instantiate(seed, seedTransform.position, Quaternion.identity);
                singleShot = false;
                shootTimer.Run();
            }
        }
    }

    private void HandleShootTimerFinished()
    {
        // Check for full auto fire
        if (Input.GetMouseButton(0))
        {
            Instantiate(seed, seedTransform.position, Quaternion.identity);
        }
        else
        {
            singleShot = true;
        }
        shootTimer.Duration = cooldown;
        shootTimer.Run();
    }
}
