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
    [SerializeField]
    GameObject mainMenu;
    [SerializeField]
    GameObject levelLoader;

    public void Start()
    {
        settingsMenu.SetActive(false);
    }

    public void OnPlayClick()
    {
        levelLoader.GetComponent<LevelLoader>().LoadNextScene("Gameplay");
        //SceneManager.LoadScene("Gameplay");
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }

    public void OnSettingsClick()
    {
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
    }

    public void OnBackClick()
    {
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
}
