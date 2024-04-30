using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

    /// <summary>
    /// Audio Manager
    /// </summary>
public static class AudioManager
{
    #region Fields

    static bool initialized = false;
    static float volume = 0.5f;
    //static AudioSource audioSource;
    static AudioSource[] audioSources;
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
    /// Master volume
    /// </summary>
    public static float Volume
    {
        get { return volume; }
        set { volume = value; }
    }

    /// <summary>
    /// Half master volume
    /// </summary>
    public static float HalfVolume
    {
        get { return volume / 2; }
    }

    #endregion

    #region Methods

    /// <summary>
    /// Initailizes the audio manager
    /// </summary>
    /// <param name="source"></param>
    public static void Initialize(AudioSource[] sources)
    {
        initialized = true;
        //audioSource = source;
        //audioSource.volume = 0.5f;
        audioSources = sources;
        foreach (AudioSource source in audioSources)
        {
            source.volume = volume;
        }
        audioClips.Add(AudioName.Select, Resources.Load<AudioClip>("Select"));
        audioClips.Add(AudioName.Shoot, Resources.Load<AudioClip>("Shoot"));
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
        foreach (AudioSource source in audioSources)
        {
            if (!source.isPlaying)
            {
                if (clipName.Equals(AudioName.EnemyHurt) || clipName.Equals(AudioName.Shoot))
                {
                    //source.PlayOneShot(audioClips[clipName], volume / 2);
                    source.clip = audioClips[clipName];
                    source.volume = HalfVolume;
                    source.Play();
                }
                else
                {
                    //source.PlayOneShot(audioClips[clipName], volume);
                    source.clip = audioClips[clipName];
                    source.volume = Volume;
                    source.Play();
                }

                break;
            }
        }



        /*
        if (clipName.Equals(AudioName.EnemyHurt) || clipName.Equals(AudioName.Shoot))
        {
            audioSource.PlayOneShot(audioClips[clipName], audioSource.volume / 2);
        }
        else
        {
            audioSource.PlayOneShot(audioClips[clipName], audioSource.volume);
        }
        */
    }

    #endregion
}
