using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    ///
    /// </summary>
public class Explosion : MonoBehaviour
{
    [SerializeField]
    ParticleSystem pSystem;

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (!pSystem.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
