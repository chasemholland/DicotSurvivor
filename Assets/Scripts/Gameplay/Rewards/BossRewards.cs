using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

    /// <summary>
    ///
    /// </summary>
public class BossRewards : MonoBehaviour
{
    [SerializeField]
    GameObject heart;
    [SerializeField]
    List<GameObject> expOrbDecoys;
    [SerializeField]
    List<GameObject> expOrbs;
    // Orb impulse force
    Vector2 force;
    List<List<GameObject>> toBeSpawned;
    List<GameObject> spawnedOrbs;

    Timer spawnTimer;
    float duration = 0.05f;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        spawnTimer = gameObject.AddComponent<Timer>();
        spawnTimer.Duration = duration;
        spawnTimer.AddTimerFinishedListener(SpawnOrbs);

        // Get orbs to spawn
        GetOrbs();
    }

    private void GetOrbs()
    {
        // Get new list
        toBeSpawned = new List<List<GameObject>>();
        spawnedOrbs = new List<GameObject>();
        List<GameObject> decoy = new List<GameObject>();
        List<GameObject> real = new List<GameObject>();

        // Get number or experience orbs
        int num = 0;
        // Get loot pool
        float numChance = Random.Range(0f, 1f);
        // 50% chance for one orb
        if (numChance <= 0.50)
        {
            num = 14;
        }
        // 26% chance for two orbs
        else if (numChance <= 0.76)
        {
            num = 18;
        }
        // 16% chance for three orbs
        else if (numChance <= 0.92)
        {
            num = 22;
        }
        // 6% chance for four orbs
        else if (numChance <= 0.98)
        {
            num = 26;
        }
        // 2% chance for 5 orbs
        else if (numChance <= 1.0)
        {
            num = 30;
        }

        // Spawn num amount of orbs with chance for different sizes
        for (int i = 1; i <= num; i++)
        {
            float sizeChance = Random.Range(0f, 1f);
            {
                // 30% chance for small orb
                if (sizeChance <= 0.30)
                {
                    decoy.Add(expOrbDecoys[0]);
                    real.Add(expOrbs[0]);
                }
                // 26% chance for medium orb
                else if (sizeChance <= 0.56)
                {
                    decoy.Add(expOrbDecoys[1]);
                    real.Add(expOrbs[1]);
                }
                // 18% chance for large orb
                else if (sizeChance <= 0.74)
                {
                    decoy.Add(expOrbDecoys[2]);
                    real.Add(expOrbs[2]);
                }
                // 12% chance for xlarge orb
                else if (sizeChance <= 0.86)
                {
                    decoy.Add(expOrbDecoys[3]);
                    real.Add(expOrbs[3]);
                }
                // 8% chance for xxlarge orb
                else if (sizeChance <= 0.94)
                {
                    decoy.Add(expOrbDecoys[4]);
                    real.Add(expOrbs[4]);
                }
                // 6% chance for xxxlargeorb
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
            GameObject orb = Instantiate(toBeSpawned[0][0], transform.position, Quaternion.identity);
            orb.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            spawnedOrbs.Add(orb);
            toBeSpawned[0].Remove(toBeSpawned[0][0]);

            // Reset spawn timer
            spawnTimer.Duration = duration;
            spawnTimer.Run();
        }
        else
        {
            // Chance to drop a heart
            if (Random.Range(0f, 1f) >= 0.95f)
            {
                force = new Vector2(Random.Range(-8f, 8f), Random.Range(-8f, 8f));
                GameObject heartObj = Instantiate(heart, transform.position, Quaternion.identity);
                heartObj.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
            }

            foreach (GameObject orb in spawnedOrbs)
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
