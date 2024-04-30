using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Pink enemy child class of enemy
    /// </summary>
public class PinkEnemy : Enemy
{
    // Small pink enemy
    [SerializeField]
    GameObject smallPink;

    // Mini pink enemy
    [SerializeField]
    GameObject miniPink;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    protected override void Start()
    {
        base.Start();

        // Half health for small pink slimes
        if (gameObject.name.StartsWith("Small"))
        {
            health /= 2;
        }

        // Quarter health for mini pink slimes
        if (gameObject.name.StartsWith("Mini"))
        {
            health /= 4;
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    protected override void Update()
    {
        base.Update();
    }

    /// <summary>
    /// Fade death effect
    /// </summary>
    protected override void HandleStep()
    {
        base.HandleStep();
    }

    /// <summary>
    /// Attacks player when called by animator
    /// </summary>
    public override bool Attack()
    {
        // Get camera bounds
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(bottomLeftCam);
        Vector3 topRight = Camera.main.ViewportToWorldPoint(topRightCam);

        if (gameObject.transform.position.y > bottomLeft.y && gameObject.transform.position.y < topRight.y)
        {
            if (gameObject.transform.position.x > bottomLeft.x && gameObject.transform.position.x < topRight.x)
            {
                if (gameObject.name.StartsWith("Pink"))
                {
                    Vector3 pos1 = new Vector3(gameObject.transform.position.x + 0.25f, gameObject.transform.position.y, 0);
                    Vector3 pos2 = new Vector3(gameObject.transform.position.x - 0.25f, gameObject.transform.position.y, 0);

                    GameObject slime1 = Instantiate(smallPink, pos1, Quaternion.identity);
                    slime1.GetComponent<PinkMove>().SetArchDirection("right");
                    GameObject slime2 = Instantiate(smallPink, pos2, Quaternion.identity);
                    slime2.GetComponent<PinkMove>().SetArchDirection("left");

                    return true;
                }
                else if (gameObject.name.StartsWith("Small"))
                {
                    Vector3 pos1 = new Vector3(gameObject.transform.position.x + 0.25f, gameObject.transform.position.y, 0);
                    Vector3 pos2 = new Vector3(gameObject.transform.position.x - 0.25f, gameObject.transform.position.y, 0);

                    GameObject slime1 = Instantiate(miniPink, pos1, Quaternion.identity);
                    slime1.GetComponent<PinkMove>().SetArchDirection("right");
                    GameObject slime2 = Instantiate(miniPink, pos2, Quaternion.identity);
                    slime2.GetComponent<PinkMove>().SetArchDirection("left");

                    return true;
                }
            }
        }

        return false;
        
    }

    /// <summary>
    /// Destroys enemy when they hit the boss wall
    /// </summary>
    /// <param name="coll"></param>
    protected override void OnCollisionEnter2D(Collision2D coll)
    {
        base.OnCollisionEnter2D(coll);
    }

    /// <summary>
    /// Handles damage when seed collides
    /// </summary>
    /// <param name="collision"></param>
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }

    /// <summary>
    /// Spawns random sizes and amounts of pickups
    /// </summary>
    protected override void SpawnRandomPickup()
    {
        base.SpawnRandomPickup();
    }

    /// <summary>
    /// Updates player crit chance
    /// </summary>
    protected override void HandleCritChanceModChanged()
    {
        base.HandleCritChanceModChanged();
    }

    /// <summary>
    /// Updates player damage
    /// </summary>
    protected override void HandleDamageModChanged()
    {
        base.HandleDamageModChanged();
    }

    /// <summary>
    /// Updates seedling damage
    /// </summary>
    protected override void HandleSeedlingDamageModChanged()
    {
        base.HandleSeedlingDamageModChanged();
    }

    /// <summary>
    /// Updates thorn damage
    /// </summary>
    protected override void HandleThornDamageModChanged()
    {
        base.HandleThornDamageModChanged();
    }
}
