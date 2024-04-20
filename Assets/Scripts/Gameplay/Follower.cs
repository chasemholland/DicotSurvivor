using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Virtual cam behavior
    /// </summary>
public class Follower : EventInvoker
{
    CinemachineVirtualCamera cam;
    CinemachineConfiner confiner;
    Timer killTween;
    float duration = 0.01f;
    Transform pTransform;
    bool stopChecking = false;
    [SerializeField]
    GameObject worldBounds;
    [SerializeField]
    GameObject bossBounds;

    // Walls set up when boss spawns
    [SerializeField]
    GameObject bossWall;
    EdgeCollider2D bossCollider;
    List<Vector2> wallPoints;
    Vector3 offScreen;


    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Get reference to virtual cam and confiner
        cam = gameObject.GetComponent<CinemachineVirtualCamera>();
        confiner = gameObject.GetComponent<CinemachineConfiner>();

        // Get player transform before player dies
        pTransform = GameObject.FindWithTag("Player").transform;

        // Get boss wall collider
        bossCollider = bossWall.GetComponent<EdgeCollider2D>();

        // Get off screen position to store boss arena walls
        offScreen = new Vector3(-80f, 80f, 0);

        // Set up timer
        killTween = gameObject.AddComponent<Timer>();
        killTween.AddTimerFinishedListener(HandleTween);
        killTween.Duration = duration;

        // Add as lsitener for boss spawned and boss death event
        EventManager.AddListener(EventName.BossSpawnedEvent, HandleBossSpawned);
        EventManager.AddListener(EventName.BossDeathEvent, HandleBossDeath);
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (!stopChecking)
        {
            if (cam.Follow != pTransform && cam.Follow != null && !killTween.Running)
            {
                killTween.Run();
            }
        }
    }

    /// <summary>
    /// Tween camera to the new game object (killer)
    /// </summary>
    private void HandleTween()
    {
        if (cam.m_Lens.OrthographicSize > 6)
        {
            cam.m_Lens.OrthographicSize -= 0.05f;
            killTween.Duration = duration;
            killTween.Run();
        }
        else
        {
            stopChecking = true;
            Destroy(killTween);
        }
    }


    private void HandleBossDeath()
    {
        // Dissable boss arena walls 
        bossCollider.isTrigger = true;

        // Move collider off screen
        bossWall.transform.position = offScreen;

        // Reset follow to player
        cam.m_Follow = pTransform;
    }

    private void HandleBossSpawned()
    {
        // Set follower null to confine player in boss arena
        cam.m_Follow = null;

        // Set up boss arena walls
        SetWallColliders();
    }

    private void SetWallColliders()
    {
        // Set transform to middle of map
        bossWall.transform.position = Vector3.zero;

        // Create new list
        wallPoints = new List<Vector2>();

        // Get camera bounds
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        Vector3 topLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, 0));
        Vector3 bottomRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, 0));
        Vector3 topRight = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, 0));

        // Add points to point list
        wallPoints.Add(new Vector2 (bottomLeft.x, bottomLeft.y)); // wallPoints[0] bottom left
        wallPoints.Add(new Vector2 (topLeft.x, topLeft.y)); // wallPoints[1] top left
        wallPoints.Add(new Vector2 (topRight.x, topRight.y)); // wallPoints[2] top right
        wallPoints.Add(new Vector2 (bottomRight.x, bottomRight.y)); // wallPoints[3] bottom right
        wallPoints.Add(new Vector2(bottomLeft.x, bottomLeft.y)); // wallPoints[0] bottom left

        // Set edge collider points
        bossCollider.SetPoints(wallPoints);

        // Enable boss arena walls
        bossCollider.isTrigger = false;
    }
}
