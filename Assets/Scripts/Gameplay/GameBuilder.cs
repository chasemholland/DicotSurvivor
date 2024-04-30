using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    ///
    /// </summary>
public class GameBuilder : MonoBehaviour
{
    [SerializeField]
    GameObject prefabGrass;
    [SerializeField]
    GameObject prefabDirt;
    float xSize = 0;
    float ySize = 0;
    Dictionary<Vector3, GameObject> Grid = new Dictionary<Vector3, GameObject>();
    Vector3 crawler;
    Vector3 up;
    Vector3 down;
    Vector3 left;
    Vector3 right;

    private void Awake()
    {
        Vector3 size = prefabGrass.GetComponent<SpriteRenderer>().bounds.size;
        xSize = size.x;
        ySize = size.y;
        crawler = new Vector3(0, 0, 0);
        up = new Vector3(0, 10, 0);
        down = new Vector3(0, -10, 0);
        left = new Vector3(-10, 0, 0);
        right = new Vector3(10, 0, 0);

        GenerateGround();
    }

    /*
    private void Start()
    {
        Vector3 size = prefabGrass.GetComponent<SpriteRenderer>().bounds.size;
        xSize = size.x;
        ySize = size.y;
        crawler = new Vector3(0, 0, 0);
        up = new Vector3(0, 10, 0);
        down = new Vector3(0, -10, 0);
        left = new Vector3(-10, 0, 0);
        right = new Vector3(10, 0, 0);

        GenerateGround();
    }
    */

    void GenerateGround()
    {
        Grid.Clear();

        // Add center floor piece
        Grid.Add(crawler, prefabGrass);

        // Start moving the crawler
        while (Grid.Count < 20) 
        {
            // Pick a direction
            int dir = Random.Range(0, 4);
            var direction = dir switch
            {
                0 => up,
                1 => down,
                2 => left,
                3 => right,
                _ => up,
            };

            // Check if grid already has a piece at that coordinate
            if (Grid.ContainsKey(crawler + direction))
            {
                // Try again if it does
                continue;
            }
            else
            {
                // Fill the spot if not
                int typeChance = Random.Range(0, 2);
                GameObject type = typeChance switch
                {
                    0 => prefabGrass,
                    1 => prefabDirt,
                    _ => prefabGrass,
                };
                crawler += direction;
                Grid.Add(crawler, type);
            }

        }

        foreach (KeyValuePair<Vector3,GameObject> obj in Grid)
        {
            GameObject floor = Instantiate(obj.Value, obj.Key, Quaternion.identity);
            floor.transform.SetParent(transform);
        }

    }
}
