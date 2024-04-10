using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Boss attributes other than movement
    /// </summary>
public class Boss : FloatEventInvoker
{
    private float health;
    private float damageAmount;
    float critChance;
    Timer stepDeathEffect;
    float stepDeathDuration;
    Material mat;
    float fade = 1f;

    [SerializeField]
    List<GameObject> chests;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {

        // Set enemy health (hardcoded 10 for now) ----------------------------------
        health = 10;
        damageAmount = ConfigUtils.PlayerDamage + Mod.ActiveModifiers["DamageMod"];
        critChance = ConfigUtils.PlayerCritChance + Mod.ActiveModifiers["CritChanceMod"];

        // Add as listener for damage mod change and crit chance mod change
        EventManager.AddFloatListener(FloatEventName.DamageMod, HandleDamageModChanged);
        EventManager.AddFloatListener(FloatEventName.CritChanceMod, HandleCritChanceModChanged);

        // Set up death effect timer
        stepDeathDuration = 0.05f;
        stepDeathEffect = gameObject.AddComponent<Timer>();
        stepDeathEffect.AddTimerFinishedListener(HandleStep);
        stepDeathEffect.Duration = stepDeathDuration;

        // Add as invoker for boss death event
        unityFloatEvents.Add(FloatEventName.BossDeathEvent, new BossDeathEvent());
        EventManager.AddFloatInvoker(FloatEventName.BossDeathEvent, this);

        // Get refernce to shader effect
        mat = gameObject.GetComponent<SpriteRenderer>().material;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (health <= 0)
        {
            if (!stepDeathEffect.Running)
            {
                stepDeathEffect.Run();
            }
        }
    }

    private void HandleStep()
    {
        if (fade <= 0)
        {
            // Update kills
            Tracker.Kills += 1;
            SpawnRandomPickup();
            unityFloatEvents[FloatEventName.BossDeathEvent].Invoke(0);
            Destroy(gameObject);
        }
        else
        {
            fade -= 0.2f;
            mat.SetFloat("_Fade", fade);
            mat.SetColor("_Color", Color.red);
            stepDeathEffect.Stop();
        }
    }

    /// <summary>
    /// Handles damage when seed collides
    /// </summary>
    /// <param name="coll"></param>
    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Seed")
        {
            if (Random.Range(0f, 1f) <= critChance)
            {
                health -= damageAmount * 2;
            }
            else
            {
                health -= damageAmount;
            }
        }
    }

    private void SpawnRandomPickup()
    {
        // Get loot pool
        float chance = Random.Range(0f, 1.0f);

        if (chance <= 0.5)
        {
            Instantiate(chests[0], transform.position, Quaternion.identity);
        }
        else if (chance <= 0.80)
        {
            Instantiate(chests[1], transform.position, Quaternion.identity);
        }
        else if (chance <= 0.95)
        {
            Instantiate(chests[2], transform.position, Quaternion.identity);
        }
        else if (chance <= 1.0)
        {
            Instantiate(chests[3], transform.position, Quaternion.identity);
        }
    }

    /// <summary>
    /// Updates player crit chance
    /// </summary>
    /// <param name="n"></param>
    private void HandleCritChanceModChanged(float n)
    {
        critChance = ConfigUtils.PlayerCritChance + Mod.ActiveModifiers["CritChanceMod"];
    }

    /// <summary>
    /// Updates player damage
    /// </summary>
    /// <param name="n"></param>
    private void HandleDamageModChanged(float n)
    {
        damageAmount = ConfigUtils.PlayerDamage + Mod.ActiveModifiers["DamageMod"];
    }
}
