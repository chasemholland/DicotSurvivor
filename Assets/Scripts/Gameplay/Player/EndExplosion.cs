using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Checks if explosion animation is complete
    /// </summary>
public class EndExplosion : MonoBehaviour
{
    [SerializeField]
    Sprite end;

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (gameObject.GetComponent<SpriteRenderer>().sprite == end)
        {
            Destroy(gameObject);
        }
    }
}
