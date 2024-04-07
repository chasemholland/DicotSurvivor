using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///
/// </summary>
public class Buttons : MonoBehaviour 
{
    bool paused = false;

    public void OnPlayClick()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void OnMainMenuQuitClick()
    {
        Application.Quit();
    }

    public void OnPauseClick()
    {
        if(!paused)
        {
            Time.timeScale = 0;
            paused = true;
        }
        else
        {
            Time.timeScale = 1;
            paused = false;
        }
    }

    public void OnResumeClick()
    {
        Time.timeScale = 1;
        paused = false;
    }

    public void OnQuitClick()
    {
        SceneManager.LoadScene("MainMenu");
        if (paused)
        {
            Time.timeScale = 1;
            paused = false;
        }
    }
}
