using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Checks if explosion animation is complete
    /// </summary>
public class EndExplosion : MonoBehaviour
{
    // End of animation sprite
    [SerializeField]
    Sprite end;

    // Start of animation sprite
    [SerializeField]
    Sprite start;

    // Object pool for explosion
    ObjectPool pool;

    private void Start()
    {
        pool = GameObject.FindGameObjectWithTag("SeedBank").GetComponent<ObjectPool>();
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (gameObject.GetComponent<SpriteRenderer>().sprite == end)
        {
            if (gameObject.CompareTag("SeedExplosion"))
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = start;
                pool.ReturnExplosion(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
