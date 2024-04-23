using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

    /// <summary>
    ///
    /// </summary>
public class SettingsMenu : MonoBehaviour
{
    Resolution[] resolutionsPossibleDuplicates;
    List<Resolution> resolutions;

    [SerializeField]
    GameObject resolutionDropdownObject;
    [SerializeField]
    TMP_Dropdown resolutionDropdown;
    [SerializeField]
    Toggle fullscreenToggle;

    public void SetFXVolume(float volume)
    {
        // Change volume once set up
        GameObject.Find("GameAudioSource").GetComponent<AudioSource>().volume = volume;
    }

    public void SetMusicVolume(float volume)
    {
        // Change volume once set up
        GameObject.Find("LoopingAudio").GetComponent<AudioSource>().volume = volume;
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Tracker.CurrentResolution = resolutionIndex;
    }

    public void SetupResolution()
    {
        // Set fullscreen toggle
        fullscreenToggle.isOn = Screen.fullScreen;

        // Get available resolutions (duplicates possible)
        resolutionsPossibleDuplicates = Screen.resolutions;

        // Make new list for no duplicate resolutions
        resolutions = new List<Resolution>();

        // Clear options
        resolutionDropdown.ClearOptions();

        // Make reslotion options in string form
        List<string> options = new List<string>();
        for (int i = 0; i < resolutionsPossibleDuplicates.Length; i++)
        {
            string option = resolutionsPossibleDuplicates[i].width + " X " + resolutionsPossibleDuplicates[i].height;
            if (!options.Contains(option))
            {
                resolutions.Add(resolutionsPossibleDuplicates[i]);
                options.Add(option);

                if (resolutionsPossibleDuplicates[i].width == Screen.currentResolution.width &&
                    resolutionsPossibleDuplicates[i].height == Screen.currentResolution.height &&
                    Tracker.CurrentResolution == -1)
                {
                    Tracker.CurrentResolution = i;
                }
            }
        }

        // Fill options
        resolutionDropdown.AddOptions(options);

        // Set current resolution
        resolutionDropdown.value = Tracker.CurrentResolution;

        // Refresh value
        resolutionDropdown.RefreshShownValue();
    }

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        SetupResolution();

        GameObject.Find("FXVolumeSlider").GetComponent<Slider>().value = GameObject.Find("GameAudioSource").GetComponent<AudioSource>().volume;
        GameObject.Find("MusicVolumeSlider").GetComponent<Slider>().value = GameObject.Find("LoopingAudio").GetComponent<AudioSource>().volume;

    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        
    }
}
