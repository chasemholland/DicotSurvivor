using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Game object pool
/// </summary>
public class ObjectPool : MonoBehaviour
{
    #region Fields

    //---- PLAYER RELATED ----------------//

    // Prefabs
    [SerializeField]
    GameObject prefabSeed;
    [SerializeField]
    GameObject prefabSmallSeed;
    [SerializeField]
    GameObject prefabSeedlingSeed;
    [SerializeField]
    GameObject prefabSeedExplosion;
    [SerializeField]
    GameObject prefabThorn;
    [SerializeField]
    GameObject prefabThornExplosion;

    // GameObject pool sizes
    private readonly int maxSeedPoolSize = 500;
    private readonly int maxSmallSeedPoolSize = 1000;
    private readonly int maxSeedlingSeedPoolSize = 400;
    private readonly int maxExplosionPoolSize = 1400;
    private readonly int maxThornPoolSize = 500;
    private readonly int maxThornExplosionPoolSize = 500;

    // Stacks of different objects
    private Stack<GameObject> innactiveSeeds = new Stack<GameObject>();
    private Stack<GameObject> innactiveSmallSeeds = new Stack<GameObject>();
    private Stack<GameObject> innactiveSeedlingSeeds = new Stack<GameObject>();
    private Stack<GameObject> innactiveSeedExplosions = new Stack<GameObject>();
    private Stack<GameObject> innactiveThorns = new Stack<GameObject>();
    private Stack<GameObject> innactiveThornExplosions = new Stack<GameObject>();

    //---- ENEMY RELATED ----------------//

    // Prefabs
    [SerializeField]
    GameObject prefabCyanProjectile;
    [SerializeField]
    GameObject prefabCyanProjectileExplosion;
    [SerializeField]
    GameObject prefabRedBossProjectile;
    [SerializeField]
    GameObject prefabRedBossProjectileExplosion;

    // GameObject pool sizes
    private readonly int maxCyanProjectilePoolSize = 200;
    private readonly int maxCyanProjectileExplosionPoolSize = 200;
    private readonly int maxRedBossProjectilePoolSize = 60;
    private readonly int maxRedBossProjectileExplosionPoolSize = 60;

    // Stacks of different objects
    private Stack<GameObject> innactiveCyanProjectiles = new Stack<GameObject>();
    private Stack<GameObject> innactiveCyanProjectileExplosions = new Stack<GameObject>();
    private Stack<GameObject> innactiveRedBossProjectiles = new Stack<GameObject>();
    private Stack<GameObject> innactiveRedBossProjectileExplosions = new Stack<GameObject>();

    #endregion

    #region Unity Methods

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        //---- PLAYER RELATED ----------------//

        // Fill seed stack
        if (prefabSeed != null)
        {
            for (int i = 0; i < maxSeedPoolSize; i++)
            {
                var seed = Instantiate(prefabSeed);
                seed.SetActive(false);
                innactiveSeeds.Push(seed);
            }
        }

        // Fill small seed stack
        if (prefabSmallSeed != null)
        {
            for (int i = 0; i < maxSmallSeedPoolSize; i++)
            {
                var seed = Instantiate(prefabSmallSeed);
                seed.SetActive(false);
                innactiveSmallSeeds.Push(seed);
            }
        }

        // Fill seedling seed stack
        if (prefabSeedlingSeed != null)
        {
            for (int i = 0; i < maxSeedlingSeedPoolSize; i++)
            {
                var seed = Instantiate(prefabSeedlingSeed);
                seed.SetActive(false);
                innactiveSeedlingSeeds.Push(seed);
            }
        }

        // Fill seed explosion stack
        if (prefabSeedExplosion != null)
        {
            for (int i = 0; i < maxExplosionPoolSize; i++)
            {
                var exp = Instantiate(prefabSeedExplosion);
                exp.SetActive(false);
                innactiveSeedExplosions.Push(exp);
            }
        }

        // Fill thorn stack
        if (prefabThorn != null)
        {
            for (int i = 0; i < maxThornPoolSize; i++)
            {
                var thorn = Instantiate(prefabThorn);
                thorn.SetActive(false);
                innactiveThorns.Push(thorn);
            }
        }

        // Fill thorn explosion stack
        if (prefabThornExplosion != null)
        {
            for (int i = 0; i < maxThornExplosionPoolSize; i++)
            {
                var exp = Instantiate(prefabThornExplosion);
                exp.SetActive(false);
                innactiveThornExplosions.Push(exp);
            }
        }

        //---- ENEMY RELATED ----------------//

        // Fill cyan projectile stack
        if (prefabCyanProjectile != null)
        {
            for (int i = 0; i < maxCyanProjectilePoolSize; i++)
            {
                var proj = Instantiate(prefabCyanProjectile);
                proj.SetActive(false);
                innactiveCyanProjectiles.Push(proj);
            }
        }

        // Fill cyan projectile explosion stack
        if (prefabCyanProjectileExplosion != null)
        {
            for (int i = 0; i < maxCyanProjectileExplosionPoolSize; i++)
            {
                var exp = Instantiate(prefabCyanProjectileExplosion);
                exp.SetActive(false);
                innactiveCyanProjectileExplosions.Push(exp);
            }
        }

        // Fill red boss projectile stack
        if (prefabRedBossProjectile != null)
        {
            for (int i = 0; i < maxRedBossProjectilePoolSize; i++)
            {
                var proj = Instantiate(prefabRedBossProjectile);
                proj.SetActive(false);
                innactiveRedBossProjectiles.Push(proj);
            }
        }

        // Fill red boss projectile explosion stack
        if (prefabRedBossProjectileExplosion != null)
        {
            for (int i = 0; i < maxRedBossProjectileExplosionPoolSize; i++)
            {
                var exp = Instantiate(prefabRedBossProjectileExplosion);
                exp.SetActive(false);
                innactiveRedBossProjectileExplosions.Push(exp);
            }
        }

    }

    #endregion

    #region Public Player Methods

    /// <summary>
    /// Get a seed to shoot
    /// </summary>
    /// <returns>GameObject seed</returns>
    public GameObject GetSeed()
    {
        while (innactiveSeeds.Count > 0)
        {
            var seed = innactiveSeeds.Pop();
            if (seed != null)
            {
                return seed;
            }
        }

        // If pool is used up spawn a new
        var newSeed = Instantiate(prefabSeed);
        newSeed.SetActive(false);
        return newSeed;
    }

    /// <summary>
    /// Get a small seed to shoot
    /// </summary>
    /// <returns>GameObject small seed</returns>
    public GameObject GetSmallSeed()
    {
        while (innactiveSmallSeeds.Count > 0)
        {
            var seed = innactiveSmallSeeds.Pop();
            if (seed != null)
            {
                return seed;
            }
        }

        // If pool is used up spawn a new
        var newSeed = Instantiate(prefabSmallSeed);
        newSeed.SetActive(false);
        return newSeed;
    }

    /// <summary>
    /// Get a seedling seed to shoot
    /// </summary>
    /// <returns>GameObject seedling seed</returns>
    public GameObject GetSeedlingSeed()
    {
        while (innactiveSeedlingSeeds.Count > 0)
        {
            var seed = innactiveSeedlingSeeds.Pop();
            if (seed != null)
            {
                return seed;
            }
        }

        // If pool is used up spawn a new
        var newSeed = Instantiate(prefabSeedlingSeed);
        newSeed.SetActive(false);
        return newSeed;
    }

    /// <summary>
    /// Get a seed explosion
    /// </summary>
    /// <returns>GameObject explosion</returns>
    public GameObject GetExplosion()
    {
        while (innactiveSeedExplosions.Count > 0)
        {
            var exp = innactiveSeedExplosions.Pop();
            if (exp != null)
            {
                return exp;
            }
        }

        // If pool is used up spawn a new
        var newExp = Instantiate(prefabSeedExplosion);
        newExp.SetActive(false);
        return newExp;
    }

    /// <summary>
    /// Get a thorn to shoot
    /// </summary>
    /// <returns>GameObject thorn</returns>
    public GameObject GetThorn()
    {
        while (innactiveThorns.Count > 0)
        {
            var thorn = innactiveThorns.Pop();
            if (thorn != null)
            {
                return thorn;
            }
        }

        // If pool is used up spawn a new
        var newThorn = Instantiate(prefabThorn);
        newThorn.SetActive(false);
        return newThorn;
    }

    /// <summary>
    /// Get a thorn explosion
    /// </summary>
    /// <returns>GameObject thorn explosion</returns>
    public GameObject GetThornExplosion()
    {
        while (innactiveThornExplosions.Count > 0)
        {
            var exp = innactiveThornExplosions.Pop();
            if (exp != null)
            {
                return exp;
            }
        }

        // If pool is used up spawn a new
        var newExp = Instantiate(prefabThornExplosion);
        newExp.SetActive(false);
        return newExp;
    }

    /// <summary>
    /// Returns seed to innactive stack
    /// </summary>
    /// <param name="seed"></param>
    public void ReturnSeed(GameObject seed)
    {
        if (seed != null)
        {
            seed.SetActive(false);
            innactiveSeeds.Push(seed);
        }
    }

    /// <summary>
    /// Returns small seed to innactive stack
    /// </summary>
    /// <param name="seed"></param>
    public void ReturnSmallSeed(GameObject seed)
    {
        if (seed != null)
        {
            seed.SetActive(false);
            innactiveSmallSeeds.Push(seed);
        }
    }

    /// <summary>
    /// Returns seedling seed to innactive stack
    /// </summary>
    /// <param name="seed"></param>
    public void ReturnSeedlingSeed(GameObject seed)
    {
        if (seed != null)
        {
            seed.SetActive(false);
            innactiveSeedlingSeeds.Push(seed);
        }
    }

    /// <summary>
    /// Returns explosion to innactive stack
    /// </summary>
    /// <param name="explosion">GameObject seed explosion</param>
    public void ReturnExplosion(GameObject explosion)
    {
        if (explosion != null)
        {
            explosion.SetActive(false);
            innactiveSeedExplosions.Push(explosion);
        }
    }

    /// <summary>
    /// Returns thorn to innactive stack
    /// </summary>
    /// <param name="thorn"></param>
    public void ReturnThorn(GameObject thorn)
    {
        if (thorn != null)
        {
            thorn.SetActive(false);
            innactiveThorns.Push(thorn);
        }
    }

    /// <summary>
    /// Returns thorn explosion to innactive stack
    /// </summary>
    /// <param name="explosion">GameObject thorn explosion</param>
    public void ReturnThornExplosion(GameObject explosion)
    {
        if (explosion != null)
        {
            explosion.SetActive(false);
            innactiveThornExplosions.Push(explosion);
        }
    }

    #endregion

    #region Public Enemy Methods

    /// <summary>
    /// Get a cyan projectile to shoot
    /// </summary>
    /// <returns>GameObject cyan projectile</returns>
    public GameObject GetCyanProjectile()
    {
        while (innactiveCyanProjectiles.Count > 0)
        {
            var proj = innactiveCyanProjectiles.Pop();
            if (proj != null)
            {
                return proj;
            }
        }

        // If pool is used up spawn a new
        var newProj = Instantiate(prefabCyanProjectile);
        newProj.SetActive(false);
        return newProj;
    }

    /// <summary>
    /// Get a cyan projectile explosion
    /// </summary>
    /// <returns>GameObject cyan projectile explosion</returns>
    public GameObject GetCyanProjectileExplosion()
    {
        while (innactiveCyanProjectileExplosions.Count > 0)
        {
            var exp = innactiveCyanProjectileExplosions.Pop();
            if (exp != null)
            {
                return exp;
            }
        }

        // If pool is used up spawn a new
        var newExp = Instantiate(prefabCyanProjectileExplosion);
        newExp.SetActive(false);
        return newExp;
    }

    /// <summary>
    /// Get a red boss projectile to shoot
    /// </summary>
    /// <returns>GameObject red boss projectile</returns>
    public GameObject GetRedBossProjectile()
    {
        while (innactiveRedBossProjectiles.Count > 0)
        {
            var proj = innactiveRedBossProjectiles.Pop();
            if (proj != null)
            {
                return proj;
            }
        }

        // If pool is used up spawn a new
        var newProj = Instantiate(prefabRedBossProjectile);
        newProj.SetActive(false);
        return newProj;
    }

    /// <summary>
    /// Get a red boss projectile explosion
    /// </summary>
    /// <returns>GameObject seed</returns>
    public GameObject GetRedBossProjectileExplosion()
    {
        while (innactiveRedBossProjectileExplosions.Count > 0)
        {
            var exp = innactiveRedBossProjectileExplosions.Pop();
            if (exp != null)
            {
                return exp;
            }
        }

        // If pool is used up spawn a new
        var newExp = Instantiate(prefabRedBossProjectileExplosion);
        newExp.SetActive(false);
        return newExp;
    }

    /// <summary>
    /// Returns cyan projectile to innactive stack
    /// </summary>
    /// <param name="proj"></param>
    public void ReturnCyanProjectile(GameObject proj)
    {
        if (proj != null)
        {
            proj.SetActive(false);
            innactiveCyanProjectiles.Push(proj);
        }
    }

    /// <summary>
    /// Returns cyan projectile explosion to innactive stack
    /// </summary>
    /// <param name="exp"></param>
    public void ReturnCyanProjectilExplosion(GameObject exp)
    {
        if (exp != null)
        {
            exp.SetActive(false);
            innactiveCyanProjectileExplosions.Push(exp);
        }
    }

    /// <summary>
    /// Returns red boss projectile to innactive stack
    /// </summary>
    /// <param name="proj"></param>
    public void ReturnRedBossProjectile(GameObject proj)
    {
        if (proj != null)
        {
            proj.SetActive(false);
            innactiveRedBossProjectiles.Push(proj);
        }
    }

    /// <summary>
    /// Returns red boss projectile explosion to innactive stack
    /// </summary>
    /// <param name="exp"></param>
    public void ReturnRedBossProjectileExplosion(GameObject exp)
    {
        if (exp != null)
        {
            exp.SetActive(false);
            innactiveRedBossProjectileExplosions.Push(exp);
        }
    }


    #endregion
}
