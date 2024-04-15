using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Check for same type pickups near each other and combines them to reduce the number of game objects
/// </summary>
public class PickupCombiner : MonoBehaviour
{
    // Tag of the objects to collect
    private string objectTag;
    // Threshold for the range within which objects should be collected
    private float rangeThreshold;
    // Central location to move the collected objects
    private Vector3 centralLocation;
    // Speed at which objects move to the central location
    private float collectionSpeed;

    private List<GameObject> collectedObjects = new List<GameObject>();
    private bool isCollecting = false;

    private string[] possibleTags = {"BronzeCoin", "BronzeCoinStack", "SilverCoin", "SilverCoinStack", "GoldCoin", "GoldCoinStack"};

    [SerializeField]
    GameObject prefabBronzeCoinStack;
    [SerializeField]
    GameObject prefabBronzeCoinBag;
    [SerializeField]
    GameObject prefabSilverCoinStack;
    [SerializeField]
    GameObject prefabSilverCoinBag;
    [SerializeField]
    GameObject prefabGoldCoinStack;
    [SerializeField]
    GameObject prefabGoldCoinBag;

    private void Start()
    {
        rangeThreshold = 10;
        collectionSpeed = 6f;
    }

    void Update()
    {
        // Check if already collecting objects, then return
        if (!isCollecting)
        {
            // Collect all objects with the specified tag
            foreach (string tag in possibleTags)
            {
                objectTag = tag;
                Collect(objectTag);
                if (isCollecting)
                {
                    return;
                }
            }
        }
    }

    void Collect(string tag)
    {
        // Find all objects with the specified tag
        GameObject[] objectsWithTag = GameObject.FindGameObjectsWithTag(tag);

        // Clear the list of collected objects
        collectedObjects.Clear();

        // Check which objects are within the specified range
        foreach (GameObject chosenOne in objectsWithTag)
        {
            foreach (GameObject obj in objectsWithTag)
            {
                if (Vector2.Distance(obj.transform.position, chosenOne.transform.position) <= rangeThreshold && collectedObjects.Count < 4 && obj != chosenOne)
                {
                    // Add the object to the list of collected objects
                    collectedObjects.Add(obj);
                }
            }

            if (collectedObjects.Count == 4)
            {
                // Set central location
                centralLocation = chosenOne.transform.position;
                // Add refernce object to list
                collectedObjects.Add(chosenOne);
                // Disable collider to prevent player picking up object
                foreach (GameObject obj in collectedObjects)
                {
                    obj.GetComponent<Collider2D>().enabled = false;
                }

                // Start moving the collected objects to the central location
                isCollecting = true;
                StartCoroutine(MoveObjectsToCentralLocation());
                break;
            }
            else
            {
                collectedObjects.Clear();
            }
        }
    }

    IEnumerator MoveObjectsToCentralLocation()
    {
        // Move the collected objects towards the central location
        while (collectedObjects.Count > 0)
        {
            foreach (GameObject obj in collectedObjects)
            {
                
                Transform objTransform = obj.transform;
                Vector2 direction = (centralLocation - objTransform.position).normalized;

                // Move the object with velocity
                //objTransform.Translate(direction * collectionSpeed * Time.deltaTime);
                obj.GetComponent<Rigidbody2D>().velocity = direction * collectionSpeed;

                // Check if object is close enough to central location, then remove it
                if (Vector2.Distance(objTransform.position, centralLocation) < 0.1f)
                {
                    Destroy(obj);
                    collectedObjects.Remove(obj);
                    break;
                }
            }

            yield return null;
        }

        // Create the coresponding new object
        CreateNewObject(objectTag);

        // Collection finished, reset the flag
        isCollecting = false;
    }

    private void CreateNewObject(string t)
    {
        switch (t)
        {
            case "BronzeCoin":
                Instantiate(prefabBronzeCoinStack, centralLocation, Quaternion.identity);
                break;
            case "BronzeCoinStack":
                Instantiate(prefabBronzeCoinBag, centralLocation, Quaternion.identity);
                break;
            case "SilverCoin":
                Instantiate(prefabSilverCoinStack, centralLocation, Quaternion.identity);
                break;
            case "SilverCoinStack":
                Instantiate(prefabSilverCoinBag, centralLocation, Quaternion.identity);
                break;
            case "GoldCoin":
                Instantiate(prefabGoldCoinStack, centralLocation, Quaternion.identity);
                break;
            case "GoldCoinStack":
                Instantiate(prefabGoldCoinBag, centralLocation, Quaternion.identity);
                break;
        }
    }
}
