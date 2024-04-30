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
        if (!pSystem.isPlaying)
        {
            if (gameObject.name.StartsWith("Cyan"))
            {
                pool.ReturnCyanProjectilExplosion(gameObject);
            }
            else
            {
                pool.ReturnRedBossProjectileExplosion(gameObject);
            }
        }
    }
}
