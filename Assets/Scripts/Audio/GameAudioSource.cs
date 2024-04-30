using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Game audio source
    /// </summary>
public class GameAudioSource : MonoBehaviour
{
    [SerializeField]
    GameObject source;

    int maxSources = 60;

    Stack<GameObject> sources = new Stack<GameObject>();

    /// <summary>
    /// Awake is called before start
    /// </summary>
    private void Awake()
    {       
        // make sure there is only one audio source game object
        if (!AudioManager.Initialized)
        {
            AudioSource[] audioSources = new AudioSource[maxSources];

            for (int i = 0; i < maxSources; i++)
            {
                GameObject s = Instantiate(source);
                s.transform.SetParent(transform);
                sources.Push(s);
                audioSources[i] = s.GetComponent<AudioSource>();
            }

            // initialize audio manager and persist throughout game
            //AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            //AudioManager.Initialize(audioSource);
            AudioManager.Initialize(audioSources);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }    
    }
}
