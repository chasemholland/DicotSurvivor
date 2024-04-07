using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    ///
    /// </summary>
public class ChestRewards : MonoBehaviour
{
    private Animator anim;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        anim = GetComponent<Animator>();

        switch (gameObject.name)
        {
            case "Bronze_Chest":
                break;
            case "Silver_Chest":
                break;
            case "Gold_Chest":
                break;
            case "Diamond_Chest":
                break;
            default:
                break;
        }
        
    }

    /// <summary>
    /// Handle player opening chest
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            anim.SetBool("isOpen", true);
        }
    }
}
