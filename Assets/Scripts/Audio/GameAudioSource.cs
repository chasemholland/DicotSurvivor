using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Game audio source
    /// </summary>
public class GameAudioSource : MonoBehaviour
{
    /// <summary>
    /// Awake is called before start
    /// </summary>
    private void Awake()
    {
        // make sure there is only one audio source game object
        if (!AudioManager.Initialized)
        {
            // initialize audio manager and persist throughout game
            AudioSource audioSource = gameObject.GetComponent<AudioSource>();
            AudioManager.Initialize(audioSource);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
