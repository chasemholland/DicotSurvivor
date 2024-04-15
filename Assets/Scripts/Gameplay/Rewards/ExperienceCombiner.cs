using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// A sweeper to declutter the screen from experience orbs
    /// </summary>
public class ExperienceCombiner : MonoBehaviour
{
    // Tag of the objects to combine
    private string objectTag;
    // Threshold for the range within which objects should be combined
    private float rangeThreshold;
    // Central location to move the combined objects
    private Vector3 centralLocation;
    // Speed at which objects move to the central location
    private float collectionSpeed;

    private List<GameObject> combinedObjects = new List<GameObject>();
    private bool isCombining = false;

    private string[] possibleTags = { "SmallOrb", "MediumOrb", "LargeOrb", "XLargeOrb", "XXLargeOrb" };

    [SerializeField]
    GameObject prefabMediumOrb;
    [SerializeField]
    GameObject prefabLargeOrb;
    [SerializeField]
    GameObject prefabXLargeOrb;
    [SerializeField]
    GameObject prefabXXLargeOrb;
    [SerializeField]
    GameObject prefabXXXLargeOrb;

    // Variables to give new objects some movement
    GameObject orb;
    Vector2 force;

    private void Start()
    {
        rangeThreshold = 5f;
        collectionSpeed = 10f;
    }

    void Update()
    {
        // Check if already combining objects, then return
        if (!isCombining)
        {
            // Combine all objects with the specified tag
            foreach (string tag in possibleTags)
            {
                objectTag = tag;
                Combine(objectTag);
                if (isCombining)
                {
                    return;
                }
            }
        }
    }

    void Combine(string tag)
    {
        // Find all objects with the specified tag
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        // Clear the list of combined objects
        combinedObjects.Clear();

        // Check which objects are within the specified range
        foreach (GameObject chosenOne in objectsWithTag)
        {
            foreach (GameObject obj in objectsWithTag)
            {
                if (Vector2.Distance(obj.transform.position, chosenOne.transform.position) <= rangeThreshold && combinedObjects.Count < 2 && obj != chosenOne)
                {
                    // Add the object to the list of combined objects
                    combinedObjects.Add(obj);
                }
            }

            if (combinedObjects.Count == 2)
            {
                // Set central location
                centralLocation = chosenOne.transform.position;
                // Add refernce object to list
                combinedObjects.Add(chosenOne);
                // Disable collider to prevent player picking up object
                foreach (GameObject obj in combinedObjects)
                {
                    obj.GetComponent<Collider2D>().enabled = false;
                }

                // Start moving the combined objects to the central location
                isCombining = true;
                StartCoroutine(MoveObjectsToCentralLocation());
                break;
            }
            else
            {
                combinedObjects.Clear();
            }
        }
    }

    IEnumerator MoveObjectsToCentralLocation()
    {
        // Move the combined objects towards the central location
        while (combinedObjects.Count > 0)
        {
            foreach (GameObject obj in combinedObjects)
            {

                Transform objTransform = obj.transform;
                Vector2 direction = (centralLocation - objTransform.position).normalized;

                // Move the object with velocity
                obj.GetComponent<Rigidbody2D>().velocity = direction * collectionSpeed;

                // Check if object is close enough to central location, then remove it
                if (Vector2.Distance(objTransform.position, centralLocation) < 0.1f)
                {
                    Destroy(obj);
                    combinedObjects.Remove(obj);
                    break;
                }
            }

            yield return null;
        }

        // Create the coresponding new object
        CreateNewObject(objectTag);

        // Combining finished, reset the flag
        isCombining = false;
    }

    private void CreateNewObject(string t)
    {
        force = new Vector2(Random.Range(-2f, 2f), Random.Range(-2f, 2f));

        switch (t)
        {
            case "SmallOrb":
                orb = Instantiate(prefabMediumOrb, centralLocation, Quaternion.identity);
                orb.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                break;
            case "MediumOrb":
                orb = Instantiate(prefabLargeOrb, centralLocation, Quaternion.identity);
                orb.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                break;
            case "LargeOrb":
                orb = Instantiate(prefabXLargeOrb, centralLocation, Quaternion.identity);
                orb.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                break;
            case "XLargeOrb":
                orb = Instantiate(prefabXXLargeOrb, centralLocation, Quaternion.identity);
                orb.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                break;
            case "XXLargeOrb":
                orb =Instantiate(prefabXXXLargeOrb, centralLocation, Quaternion.identity);
                orb.GetComponent<Rigidbody2D>().AddForce(force, ForceMode2D.Impulse);
                break;
            default:
                break;
        }
    }
}
