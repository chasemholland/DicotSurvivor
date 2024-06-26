using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
///
/// </summary>
public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1;


    public void LoadNextScene(string scene)
    {
        // load given level
        StartCoroutine(LoadLevel(scene));
    }

    IEnumerator LoadLevel(string scene)
    {
        // start fade
        transition.SetTrigger("Start");

        var newScene = SceneManager.LoadSceneAsync(scene);
        newScene.allowSceneActivation = false;

        // wait for fade to complete
        yield return new WaitForSeconds(transitionTime);

        newScene.allowSceneActivation = true;
        // load scene
        //SceneManager.LoadScene(scene);
    }
}
