using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    /// <summary>
    /// Virtual cam behavior
    /// </summary>
public class Follower : MonoBehaviour
{
    CinemachineVirtualCamera cam;
    Timer killTween;
    float duration = 0.01f;
    Transform pTransform;
    bool stopChecking = false;

    /// <summary>
    /// Start is called before the first frame update
    /// </summary>
    void Start()
    {
        // Get reference to virtual cam
        cam = gameObject.GetComponent<CinemachineVirtualCamera>();

        // Get player transform before player dies
        pTransform = GameObject.FindWithTag("Player").transform;

        // Set up timer
        killTween = gameObject.AddComponent<Timer>();
        killTween.AddTimerFinishedListener(HandleTween);
        killTween.Duration = duration;
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        if (!stopChecking)
        {
            if (cam.Follow != pTransform && !killTween.Running)
            {
                killTween.Run();
            }
        }
    }

    /// <summary>
    /// Tween camera to the new game object (killer)
    /// </summary>
    private void HandleTween()
    {
        if (cam.m_Lens.OrthographicSize > 4)
        {
            cam.m_Lens.OrthographicSize -= 0.05f;
            killTween.Duration = duration;
            killTween.Run();
        }
        else
        {
            stopChecking = true;
            Destroy(killTween);
        }
    }
}
