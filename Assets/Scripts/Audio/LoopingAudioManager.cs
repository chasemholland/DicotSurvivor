using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    ///
    /// </summary>
public static class LoopingAudioManager
{
    #region Fields

    static bool initialized = false;
    static AudioName playing;
    static AudioSource audioSource;
    static Dictionary<AudioName, AudioClip> audioClips =
        new Dictionary<AudioName, AudioClip>();

    #endregion

    #region Properties

    /// <summary>
    /// Returns true if initialized, else false
    /// </summary>
    public static bool Initialized
    {
        get { return initialized; }
    }

    /// <summary>
    /// Returns the current song playing
    /// </summary>
    public static AudioName Playing
    {
        get { return playing; }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Initailizes the audio manager
    /// </summary>
    /// <param name="source"></param>
    public static void Initialize(AudioSource source)
    {
        initialized = true;
        audioSource = source;
        audioSource.volume = 0.5f;
        audioClips.Add(AudioName.Main, Resources.Load<AudioClip>("Main"));
    }

    /// <summary>
    /// Plays the provided audio clip
    /// </summary>
    /// <param name="clipName"></param>
    public static void Switch(AudioName clipName)
    {
        audioSource.clip = audioClips[clipName];
        audioSource.Play();
        playing = clipName;
    }

    #endregion
}
