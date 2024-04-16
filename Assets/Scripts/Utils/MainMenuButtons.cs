using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///
/// </summary>
public class MainMenuButtons : MonoBehaviour 
{
    [SerializeField]
    GameObject settingsMenu;

    public void Start()
    {
        settingsMenu.SetActive(false);
    }

    public void OnPlayClick()
    {
        SceneManager.LoadScene("Gameplay");
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    public void OnSettingsClick()
    {
        settingsMenu.SetActive(true);
    }

    public void OnBackClick()
    {
        settingsMenu.SetActive(false);
    }
}
