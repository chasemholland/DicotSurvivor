using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

    /// <summary>
    /// Enemy spawner
    /// </summary>
public class EndlessSpawner : EventInvoker
{
    private float xRangeMin;
    private float xRangeMax;
    private float yRangeMin;
    private float yRangeMax;
    Timer spawnTimer;
    float spawnTime = 1;
    float retrySpawnTime = 0.1f;
    [SerializeField]
    List<GameObject> enemies;
    [SerializeField]
    List<GameObject> bosses;
    int bossIndex;
    float middleOffset = 6f;
    int currentBoss = 1;

    // Walls outer bound variables
    [SerializeField]
    List<GameObject> horizontalWalls;
    [SerializeField]
    List<GameObject> verticalWalls;
    List<float> outerBounds;
    float xCoord = 0;
    float yCoord = 0;

    // Camera inner bound variables
    Vector3 bottomLeftCam;
    Vector3 topRightCam;
    Vector3 bottomLeft;
    Vector3 topRight;
    float xMin;
    float xMax;
    float yMin;
    float yMax;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Set the spawn ranges
        SetRanges();

        // Enemy spawn timer
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.AddTimerFinishedListener(SpawnEnemies);
        spawnTimer.Duration = spawnTime;
        spawnTimer.Run();

        // Add as invoker for boss spawned event
        unityEvents.Add(EventName.BossSpawnedEvent, new BossSpawnedEvent());
        EventManager.AddInvoker(EventName.BossSpawnedEvent, this);

        // Add as listener for boss death event
        EventManager.AddListener(EventName.BossDeathEvent, RestartSpawning);

        // Add add listener for player death event
        EventManager.AddListener(EventName.PlayerDeathEvent, StopSpawning);

        // Set defaults
        bottomLeftCam = new Vector3(0, 0, 0);
        topRightCam = new Vector3(1, 1, 0);
    }

    private void SpawnEnemies()
    {
        // Spawn boss every 50 kills
        if (Tracker.Kills >= 50 * currentBoss)
        {
            // Set current boss
            currentBoss++;

            // Stop enemy spawner
            spawnTimer.Stop();

            // Start boss spawner provided with index
            bossIndex = 0;
            SpawnBoss(bossIndex);

            return;
        }

        /*
        xCoord = Random.Range(xRangeMin + 2f, xRangeMax - 2f);
        yCoord = Random.Range(yRangeMin + 2f, yRangeMax - 2f);
        bottomLeft = Camera.main.ViewportToWorldPoint(bottomLeftCam);
        topRight = Camera.main.ViewportToWorldPoint(topRightCam);
        xMin = bottomLeft.x;
        xMax = topRight.x;
        yMin = bottomLeft.y;
        yMax = topRight.y;
        */

        bottomLeft = Camera.main.ViewportToWorldPoint(bottomLeftCam);
        topRight = Camera.main.ViewportToWorldPoint(topRightCam);
        xMin = bottomLeft.x;
        xMax = topRight.x;
        yMin = bottomLeft.y;
        yMax = topRight.y;

        // Pick a side to spawn on and set ranges
        int side = Random.Range(1, 5);
        switch (side)
        {
            case 1:
                // Left spawn
                xCoord = Random.Range(xMin - 4f, xMin - 2f);
                yCoord = Random.Range(yMin, yMax);
                break;
            case 2:
                // Right spawn
                xCoord = Random.Range(xMax + 2f, xMax + 4f);
                yCoord = Random.Range(yMin, yMax);
                break;
            case 3:
                // Top spawn
                xCoord = Random.Range(xMin, xMin);
                yCoord = Random.Range(yMax + 2f, yMax + 4f);
                break;
            case 4:
                // Bottom spawn
                xCoord = Random.Range(xMin, xMin);
                yCoord = Random.Range(yMin - 2f, yMin - 4f);
                break;
            default:
                // Top spawn
                xCoord = Random.Range(xMin, xMin);
                yCoord = Random.Range(yMax + 2f, yMax + 4f);
                break;
        }

        // Check if spawn point is in map bounds
        if (xCoord >= xRangeMin + 2f && xCoord <= xRangeMax - 2f)
        {
            if (yCoord >= yRangeMin + 2f && yCoord <= yRangeMax - 2f)
            {
                // Spawn random enemy
                int type = Random.Range(0, enemies.Count);
                Instantiate(enemies[type], new Vector3(xCoord, yCoord, 0), Quaternion.identity);

                // Reset timer             
                spawnTimer.Duration = Mathf.Clamp(spawnTime - Tracker.EnemySpawnRateMod, 0.1f, 1);
                spawnTimer.Run();
            }
            else
            {
                // Try again
                spawnTimer.Duration = retrySpawnTime;
                spawnTimer.Run();
            }
        }
        else
        {
            // Try again
            spawnTimer.Duration = retrySpawnTime;
            spawnTimer.Run();
        }


        /*
        // Check if spawn point is out of the viewport
        if (xCoord <= xMin || xCoord >= xMax)
        {
            if (yCoord <= yMin || yCoord >= yMax)
            {
                // Spawn random enemy
                int type = Random.Range(0, enemies.Count);
                Instantiate(enemies[type], new Vector3(xCoord, yCoord, 0), Quaternion.identity);

                // Reset timer             
                spawnTimer.Duration = Mathf.Clamp(spawnTime - Tracker.EnemySpawnRateMod, 0.1f, 1);
                spawnTimer.Run();
            }
            else
            {
                // Try again
                spawnTimer.Duration = retrySpawnTime;
                spawnTimer.Run();
            }
        }
        else
        {
            // Try again
            spawnTimer.Duration = retrySpawnTime;
            spawnTimer.Run();
        }
        */
    }

    private void SpawnBoss(int index)
    {
        // Get the camera edges
        bottomLeft = Camera.main.ViewportToWorldPoint(bottomLeftCam);
        topRight = Camera.main.ViewportToWorldPoint(topRightCam);

        // Get cam half with and half length
        float xHalfLength = Vector3.Distance(new Vector3(topRight.x, 0, 0), new Vector3(bottomLeft.x, 0, 0)) / 2;
        float yHalfLength = Vector3.Distance(new Vector3(0, topRight.y, 0), new Vector3(0, bottomLeft.y, 0)) / 2;

        // Spawn boss in the middle of the camera slightly offset to the top
        Instantiate(bosses[index], new Vector3(topRight.x - xHalfLength, topRight.y - yHalfLength + middleOffset, 0), Quaternion.identity);

        // Invoke boss spawned event
        unityEvents[EventName.BossSpawnedEvent].Invoke();

    }

    /// <summary>
    /// Starts spawning enemies on boss death
    /// </summary>
    private void RestartSpawning()
    {
        // Reset spawn timer
        spawnTimer.Duration = Mathf.Clamp(spawnTime - Tracker.EnemySpawnRateMod, 0.1f, 1);
        spawnTimer.Run();
    }

    /// <summary>
    /// Stops enemy spawn
    /// </summary>
    private void StopSpawning()
    {
        // Destroy spawn timer
        Destroy(GetComponent<Timer>());
    }

    /// <summary>
    /// Sets spawn ranges
    /// </summary>
    private void SetRanges()
    {
        // Initialize list
        outerBounds = new List<float>();

        // Get vertical wall bounds
        foreach (GameObject vwall in verticalWalls)
        {
            outerBounds.Add(vwall.GetComponent<EdgeCollider2D>().bounds.center.x);
        }

        // Get horizontal wall bounds
        foreach (GameObject hwall in horizontalWalls)
        {
            outerBounds.Add(hwall.GetComponent<EdgeCollider2D>().bounds.center.y);
        }

        xRangeMin = outerBounds[0];
        xRangeMax = outerBounds[1];
        yRangeMax = outerBounds[2];
        yRangeMin = outerBounds[3];

    }
}
