using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

    /// <summary>
    ///
    /// </summary>
public class ChestRewards : MonoBehaviour
{
    private Animator anim;

    [SerializeField]
    List<GameObject> expOrbDecoys;
    [SerializeField]
    List<GameObject> expOrbs;
    [SerializeField]
    Transform spawnPoint;
    // Orb impulse force
    Vector2 force;
    List<List<GameObject>> toBeSpawned;
    List<GameObject> spawnedOrbs;
    bool open = false;

    Timer spawnTimer;
    float duration = 0.1f;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        anim = GetComponent<Animator>();
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = duration;
        spawnTimer.AddTimerFinishedListener(SpawnOrbs);
    }

    /// <summary>
    /// Handle player opening chest
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && !open)
        {
            string[] splitChestName = gameObject.name.Split("(");
            GetOrbs(splitChestName[0]);
            anim.SetBool("isOpen", true);
            open = true;

            // Disable collider
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
    }

    private void GetOrbs(string chestName)
    {
        // Get new list
        toBeSpawned = new List<List<GameObject>>();
        spawnedOrbs = new List<GameObject>();
        List<GameObject> decoy = new List<GameObject>();
        List<GameObject> real = new List<GameObject>();

        // Get number of orbs
        int num;
        switch (chestName)
        {
            case "Bronze_Chest":
                num = Random.Range(10, 16);
                break;
            case "Silver_Chest":
                num = Random.Range(15, 21);
                break;
            case "Gold_Chest":
                num = Random.Range(20, 26);
                break;
            case "Diamond_Chest":
                num = Random.Range(25, 30);
                break;
            default:
                num = 10;
                break;
        }

        // Spawn num amount of orbs with chance for different sizes
        for (int i = 1; i <= num; i++)
        {
            float sizeChance = Random.Range(0f, 1f);
            {
                // 40% chance for small orb
                if (sizeChance <= 0.40)
                {
                    decoy.Add(expOrbDecoys[0]);
                    real.Add(expOrbs[0]);
                }
                // 28% chance for medium orb
                else if (sizeChance <= 0.68)
                {
                    decoy.Add(expOrbDecoys[1]);
                    real.Add(expOrbs[1]);
                }
                // 18% chance for large orb
                else if (sizeChance <= 0.86)
                {
                    decoy.Add(expOrbDecoys[2]);
                    real.Add(expOrbs[2]);
                }
                // 10% chance for xlarge orb
                else if (sizeChance <= 0.96)
                {
                    decoy.Add(expOrbDecoys[3]);
                    real.Add(expOrbs[3]);
                }
                // 4% chance for xxlarge orb
                else if (sizeChance <= 0.98)
                {
                    decoy.Add(expOrbDecoys[4]);
                    real.Add(expOrbs[4]);
                }
                // 2% chance for xxxlargeorb
                else if (sizeChance <= 1.0)
                {
                    decoy.Add(expOrbDecoys[5]);
                    real.Add(expOrbs[5]);
                }
            }
        }

        // tobeSpawned[0]
        toBeSpawned.Add(decoy);
        // toBeSpawned[1]
        toBeSpawned.Add(real);

        spawnTimer.Run();
    }

    private void SpawnOrbs()
    {
        if (toBeSpawned[0].Count > 0)
        {
            // Get random force for movement
            force = new Vector2(Random.Range(-8f, 8f), Random.Range(-8f, 8f));

            // Spawn decoy orb and get moving
            GameObject orb = Instantiate(toBeSpawned[0][0], spawnPoint.position, Quaternion.identity);
            orb.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            spawnedOrbs.Add(orb);
            toBeSpawned[0].Remove(toBeSpawned[0][0]);

            // Reset spawn timer
            spawnTimer.Duration = duration;
            spawnTimer.Run();
        }
        else
        {
            foreach(GameObject orb in spawnedOrbs)
            {
                Vector3 velocity = orb.GetComponent<Rigidbody2D>().velocity;
                GameObject newOrb = Instantiate(toBeSpawned[1][0], orb.transform.position, Quaternion.identity);
                newOrb.GetComponent<Rigidbody2D>().velocity = velocity;
                Destroy(orb);
                toBeSpawned[1].Remove(toBeSpawned[1][0]);
            }
            Destroy(gameObject);
        }  
    }
}
