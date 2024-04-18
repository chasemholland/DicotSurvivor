using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Controls the flower (seed shooter) behavior
/// </summary>
public class SeedShooter : EventInvoker
{
    private Camera mainCamera;
    private Vector3 mousePosition;
    [SerializeField]
    GameObject seed;
    [SerializeField]
    GameObject smallSeed;
    float seedForce;
    [SerializeField]
    Transform seedTransform;
    private bool singleShot = true;
    Timer shootTimer;
    float cooldown;
    float seedNum = 1;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Set defaults
        cooldown = ConfigUtils.PlayerShootCooldown;
        seedForce = ConfigUtils.PlayerSeedSpeed + Mod.ActiveModifiers["SeedSpeedMod"];

        // Add as listener for choot cooldown and seed speed mod change
        EventManager.AddListener(EventName.ShootCooldownMod, HandleShootCooldownModChanged);
        EventManager.AddListener(EventName.SeedSpeedMod, HandleSeedSpeedModChanged);

        // Add as listener for multiseed unlocked event
        EventManager.AddListener(EventName.Multiseed, HandleMultiseedUnlocked);

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
                switch (seedNum)
                {
                    case 1:
                        ShootOne();
                        break;
                    case 2:
                        ShootTwo();
                        break;
                    case 3:
                        ShootThree();
                        break;
                    case 4:
                        ShootFour();
                        break;
                    default:
                        ShootOne();
                        break;
                }
                
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
            switch (seedNum)
            {
                case 1:
                    ShootOne();
                    break;
                case 2:
                    ShootTwo();
                    break;
                case 3:
                    ShootThree();
                    break;
                case 4:
                    ShootFour();
                    break;
                default:
                    ShootOne();
                    break;
            }
        }
        else
        {
            singleShot = true;
        }
        shootTimer.Duration = cooldown;
        shootTimer.Run();
    }

    public void ShootOne()
    {
        // Calculate direction towards the mouse position
        Vector3 directionToMouse = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - seedTransform.position);

        // Calculate velocity towards the mouse position
        Vector2 velocityTowardsMouse = new Vector2(directionToMouse.x, directionToMouse.y).normalized * seedForce;

        // Instantiate objects and set velocities
        GameObject objTowardsMouse = Instantiate(seed, seedTransform.position, Quaternion.identity);
        Rigidbody2D rbTowardsMouse = objTowardsMouse.GetComponent<Rigidbody2D>();
        if (rbTowardsMouse != null)
        {
            // Rotate the seed in direction of travel
            Vector3 rotation = transform.position - mousePosition;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            rbTowardsMouse.transform.rotation = Quaternion.Euler(0, 0, rot + 90);

            rbTowardsMouse.velocity = velocityTowardsMouse;
        }
    }

    public void ShootTwo()
    {
        // Calculate direction towards the mouse position
        Vector3 directionToMouse = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - seedTransform.position);

        // Calculate positive and negative angles
        Vector2 positiveOffsetDirection = Quaternion.Euler(0, 0, 2.5f) * new Vector2(directionToMouse.x, directionToMouse.y).normalized;
        Vector2 negativeOffsetDirection = Quaternion.Euler(0, 0, -2.5f) * new Vector2(directionToMouse.x, directionToMouse.y).normalized;

        // Calculate velocities with positive and negative angles
        Vector2 velocityPositiveAngle = positiveOffsetDirection * seedForce;
        Vector2 velocityNegativeAngle = negativeOffsetDirection * seedForce;

        // Instantiate objects and set velocities
        GameObject objPositiveAngle = Instantiate(smallSeed, seedTransform.position, Quaternion.identity);
        Rigidbody2D rbPositiveAngle = objPositiveAngle.GetComponent<Rigidbody2D>();
        if (rbPositiveAngle != null)
        {
            // Rotate the seed in direction of travel
            Vector3 rotation = transform.position - mousePosition;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            rbPositiveAngle.transform.rotation = Quaternion.Euler(0, 0, rot + 90 + 2.5f);

            rbPositiveAngle.velocity = velocityPositiveAngle;
        }

        GameObject objNegativeAngle = Instantiate(smallSeed, seedTransform.position, Quaternion.identity);
        Rigidbody2D rbNegativeAngle = objNegativeAngle.GetComponent<Rigidbody2D>();
        if (rbNegativeAngle != null)
        {
            // Rotate the seed in direction of travel
            Vector3 rotation = transform.position - mousePosition;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            rbNegativeAngle.transform.rotation = Quaternion.Euler(0, 0, rot + 90 - 2.5f);

            rbNegativeAngle.velocity = velocityNegativeAngle;
        }
    }

    public void ShootThree()
    {
        // Calculate direction towards the mouse position
        Vector3 directionToMouse = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - seedTransform.position);

        // Calculate velocity towards the mouse position
        Vector2 velocityTowardsMouse = new Vector2(directionToMouse.x, directionToMouse.y).normalized * seedForce;

        // Calculate positive and negative angles
        Vector2 positiveOffsetDirection = Quaternion.Euler(0, 0, 5f) * new Vector2(directionToMouse.x, directionToMouse.y).normalized;
        Vector2 negativeOffsetDirection = Quaternion.Euler(0, 0, -5f) * new Vector2(directionToMouse.x, directionToMouse.y).normalized;

        // Calculate velocities with positive and negative angles
        Vector2 velocityPositiveAngle = positiveOffsetDirection * seedForce;
        Vector2 velocityNegativeAngle = negativeOffsetDirection * seedForce;

        // Instantiate objects and set velocities
        GameObject objTowardsMouse = Instantiate(smallSeed, seedTransform.position, Quaternion.identity);
        Rigidbody2D rbTowardsMouse = objTowardsMouse.GetComponent<Rigidbody2D>();
        if (rbTowardsMouse != null)
        {
            // Rotate the seed in direction of travel
            Vector3 rotation = transform.position - mousePosition;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            rbTowardsMouse.transform.rotation = Quaternion.Euler(0, 0, rot + 90);

            rbTowardsMouse.velocity = velocityTowardsMouse;
        }

        GameObject objPositiveAngle = Instantiate(smallSeed, seedTransform.position, Quaternion.identity);
        Rigidbody2D rbPositiveAngle = objPositiveAngle.GetComponent<Rigidbody2D>();
        if (rbPositiveAngle != null)
        {
            // Rotate the seed in direction of travel
            Vector3 rotation = transform.position - mousePosition;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            rbPositiveAngle.transform.rotation = Quaternion.Euler(0, 0, rot + 90 + 5);

            rbPositiveAngle.velocity = velocityPositiveAngle;
        }

        GameObject objNegativeAngle = Instantiate(smallSeed, seedTransform.position, Quaternion.identity);
        Rigidbody2D rbNegativeAngle = objNegativeAngle.GetComponent<Rigidbody2D>();
        if (rbNegativeAngle != null)
        {
            // Rotate the seed in direction of travel
            Vector3 rotation = transform.position - mousePosition;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            rbNegativeAngle.transform.rotation = Quaternion.Euler(0, 0, rot + 90 - 5);

            rbNegativeAngle.velocity = velocityNegativeAngle;
        }
    }

    public void ShootFour()
    {
        // Calculate direction towards the mouse position
        Vector3 directionToMouse = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - seedTransform.position);

        // Calculate positive and negative angles
        Vector2 positiveOffsetDirectionOne = Quaternion.Euler(0, 0, 2.5f) * new Vector2(directionToMouse.x, directionToMouse.y).normalized;
        Vector2 positiveOffsetDirectionTwo = Quaternion.Euler(0, 0, 7.5f) * new Vector2(directionToMouse.x, directionToMouse.y).normalized;
        Vector2 negativeOffsetDirectionOne = Quaternion.Euler(0, 0, -2.5f) * new Vector2(directionToMouse.x, directionToMouse.y).normalized;
        Vector2 negativeOffsetDirectionTwo = Quaternion.Euler(0, 0, -7.5f) * new Vector2(directionToMouse.x, directionToMouse.y).normalized;

        // Calculate velocities with positive and negative angles
        Vector2 velocityPositiveAngleOne = positiveOffsetDirectionOne * seedForce;
        Vector2 velocityPositiveAngleTwo = positiveOffsetDirectionTwo * seedForce;
        Vector2 velocityNegativeAngleOne = negativeOffsetDirectionOne * seedForce;
        Vector2 velocityNegativeAngleTwo = negativeOffsetDirectionTwo * seedForce;


        // Instantiate objects and set velocities
        GameObject objPositiveAngleOne = Instantiate(smallSeed, seedTransform.position, Quaternion.identity);
        Rigidbody2D rbPositiveAngleOne = objPositiveAngleOne.GetComponent<Rigidbody2D>();
        if (rbPositiveAngleOne != null)
        {
            // Rotate the seed in direction of travel
            Vector3 rotation = transform.position - mousePosition;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            rbPositiveAngleOne.transform.rotation = Quaternion.Euler(0, 0, rot + 90 + 2.5f);

            rbPositiveAngleOne.velocity = velocityPositiveAngleOne;
        }

        GameObject objPositiveAngleTwo = Instantiate(smallSeed, seedTransform.position, Quaternion.identity);
        Rigidbody2D rbPositiveAngleTwo = objPositiveAngleTwo.GetComponent<Rigidbody2D>();
        if (rbPositiveAngleTwo != null)
        {
            // Rotate the seed in direction of travel
            Vector3 rotation = transform.position - mousePosition;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            rbPositiveAngleTwo.transform.rotation = Quaternion.Euler(0, 0, rot + 90 + 7.5f);

            rbPositiveAngleTwo.velocity = velocityPositiveAngleTwo;
        }

        GameObject objNegativeAngleOne = Instantiate(smallSeed, seedTransform.position, Quaternion.identity);
        Rigidbody2D rbNegativeAngleOne = objNegativeAngleOne.GetComponent<Rigidbody2D>();
        if (rbNegativeAngleOne != null)
        {
            // Rotate the seed in direction of travel
            Vector3 rotation = transform.position - mousePosition;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            rbNegativeAngleOne.transform.rotation = Quaternion.Euler(0, 0, rot + 90 - 2.5f);

            rbNegativeAngleOne.velocity = velocityNegativeAngleOne;
        }

        GameObject objNegativeAngleTwo = Instantiate(smallSeed, seedTransform.position, Quaternion.identity);
        Rigidbody2D rbNegativeAngleTwo = objNegativeAngleTwo.GetComponent<Rigidbody2D>();
        if (rbNegativeAngleTwo != null)
        {
            // Rotate the seed in direction of travel
            Vector3 rotation = transform.position - mousePosition;
            float rot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            rbNegativeAngleTwo.transform.rotation = Quaternion.Euler(0, 0, rot + 90 - 7.5f);

            rbNegativeAngleTwo.velocity = velocityNegativeAngleTwo;
        }
    }

    /// <summary>
    /// Handles shoot cooldown mod chnage
    /// </summary>
    private void HandleShootCooldownModChanged()
    {
        cooldown = Mathf.Clamp(ConfigUtils.PlayerShootCooldown - Mod.ActiveModifiers["ShootCooldownMod"], 0.1f, ConfigUtils.PlayerShootCooldown);
    }

    /// <summary>
    /// Handles seed speed mod changed
    /// </summary>
    private void HandleSeedSpeedModChanged()
    {
        seedForce = ConfigUtils.PlayerSeedSpeed + Mod.ActiveModifiers["SeedSpeedMod"];
    }

    /// <summary>
    /// Unlcoks multiseed if not unlocked
    /// </summary>
    private void HandleMultiseedUnlocked()
    {
        seedNum = Mod.ActiveMutations["Multiseed"] + 1;
    }
}
