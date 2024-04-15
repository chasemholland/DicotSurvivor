using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Player item collector 
    /// </summary>
public class CollectionField : MonoBehaviour
{
    float speed = 10f;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.EndsWith("Orb") || collision.gameObject.CompareTag("Heart"))
        {
            if (collision.gameObject.GetComponent<Collider2D>().enabled == true)
            {
                //collecting = true;
                StartCoroutine(CollectItem(collision.gameObject));
            }
            else
            {
                return;
            }
        }
    }


    IEnumerator CollectItem(GameObject obj)
    {
        while (obj != null)
        {
            // Get direction to move
            Vector3 directionToPlayer = (gameObject.transform.position - obj.transform.position).normalized;
            Vector2 direction = new Vector2(directionToPlayer.x, directionToPlayer.y);
            obj.GetComponent<Rigidbody2D>().velocity = direction * speed;
            if (obj.GetComponent<Collider2D>().enabled == false)
            {
                obj = null;
            }

            yield return null;
        }  


    }
}
