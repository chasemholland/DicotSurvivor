using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    ///
    /// </summary>
public class EndlessSpawner : MonoBehaviour
{
    private float xRangeMin;
    private float xRangeMax;
    private float yRangeMin;
    private float yRangeMax;
    Timer spawnTimer;
    [SerializeField]
    List<GameObject> enemies;
    [SerializeField]
    List<GameObject> horizontalWalls;
    [SerializeField]
    List<GameObject> verticalWalls;
    List<float> outerBounds;
    float innerBounds;
    float xCoord = 0;
    float yCoord = 0;
    GameObject player;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Set the spawn ranges
        SetRanges();

        // Get refernce to player
        player = GameObject.FindWithTag("Player");

        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.AddTimerFinishedListener(SpawnEnemies);
        spawnTimer.Duration = 1;
        spawnTimer.Run();

        // Add add listener for player death event
        EventManager.AddListener(EventName.PlayerDeathEvent, StopSpawning);
    }
    
    private void SpawnEnemies()
    {
        xCoord = Random.Range(xRangeMin + 0.5f, xRangeMax - 0.5f);
        yCoord = Random.Range(yRangeMin + 0.5f, yRangeMax - 0.5f);

        // Check if spawn point is out of the viewport
        if (xCoord >= player.transform.position.x - innerBounds && xCoord <= player.transform.position.x + innerBounds ||
            yCoord >= player.transform.position.y - innerBounds && yCoord <= player.transform.position.y + innerBounds)
        {
            // Try again
            SpawnEnemies();
        }
        else
        {
            // Spawn random enemy
            int type = Random.Range(0, enemies.Count);
            Instantiate(enemies[type], new Vector3(yCoord, xCoord, 0), Quaternion.identity);

            // Reset timer
            spawnTimer.Duration = 1;
            spawnTimer.Run();
        }
    }

    /// <summary>
    /// Stops enemy spawn
    /// </summary>
    /// <param name="n">unused</param>
    private void StopSpawning(float n)
    {
        // Destroy spawn timer
        Destroy(GetComponent<Timer>());
    }

    /// <summary>
    /// Sets spawn ranges
    /// </summary>
    private void SetRanges()
    {
        // Set inner bounds
        innerBounds = GameObject.FindWithTag("Follower").GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize / 2;

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
