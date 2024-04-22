using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Audio Manager
    /// </summary>
public static class AudioManager
{
    #region Fields

    static bool initialized = false;
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
        audioClips.Add(AudioName.Select, Resources.Load<AudioClip>("Select"));
        audioClips.Add(AudioName.PlayerHurt, Resources.Load<AudioClip>("Player_Hurt"));
        audioClips.Add(AudioName.EnemyHurt, Resources.Load<AudioClip>("Enemy_Hurt"));
        audioClips.Add(AudioName.OrbCollect, Resources.Load<AudioClip>("Orb_Collect"));
        audioClips.Add(AudioName.LevelUp, Resources.Load<AudioClip>("Level_Up_Mix"));
    }

    /// <summary>
    /// Plays the provided audio clip
    /// </summary>
    /// <param name="clipName"></param>
    public static void Play(AudioName clipName)
    {
        //if (clipName.Equals(AudioName.ShipLaser) || clipName.Equals(AudioName.Pickup))
        //{
            //audioSource.PlayOneShot(audioClips[clipName]); //, 0.5f);
        //}
        //else
        //{

        audioSource.PlayOneShot(audioClips[clipName]);

        //}

    }

    #endregion
}
