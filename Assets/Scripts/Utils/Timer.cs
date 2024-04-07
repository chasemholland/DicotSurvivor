using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
///
/// </summary>
public class Timer : MonoBehaviour
{
    #region Fields

    // duration
    float totalSeconds = 0;

    // execution
    float elapsedSeconds = 0;
    bool running = false;

    // support for Finished
    bool started = false;

    // event support
    TimerFinishedEvent timerFinishedEvent = new TimerFinishedEvent();

    #endregion

    #region Properties

    /// <summary>
    /// Set the duration of the timer
    /// </summary>
    public float Duration
    {
        set
        {
            if (!running)
            {
                totalSeconds = value;
            }
        }
    }

    /// <summary>
    /// Gets whether timer is finished or not
    /// Always false if timer never started
    /// </summary>
    public bool Finished
    {
        get { return started && !running; }
    }

    /// <summary>
    /// Gets whether timer is running or not
    /// </summary>
    public bool Running
    {
        get { return running; }
    }

    /// <summary>
    /// Gets time remianing on the timer
    /// </summary>
    public float SecondsLeft
    {
        get
        {
            if (running)
            {
                return totalSeconds - elapsedSeconds;
            }
            else
            {
                return 0;
            }
        }
    }

    #endregion

    #region Unity Methods

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        // update timer and check for Finished
        if (running)
        {
            elapsedSeconds += Time.deltaTime;
            if (elapsedSeconds >= totalSeconds)
            {
                running = false;
                timerFinishedEvent.Invoke();
            }
        }
    }

    #endregion

    #region Public Methods

    /// <summary>
    /// Starts the timer running
    /// </summary>
    public void Run()
    {
        // only run with valid duration (positive integer)
        if (totalSeconds > 0)
        {
            started = true;
            running = true;
            elapsedSeconds = 0;
        }
    }

    /// <summary>
    /// Stops the timer
    /// </summary>
    public void Stop()
    {
        started = false;
        running = false;
    }

    /// <summary>
    /// Add time to the timer
    /// </summary>
    /// <param name="seconds"></param>
    public void AddTime(float seconds)
    {
        totalSeconds += seconds;
    }

    /// <summary>
    /// Adds the timer as listener for finished event set in
    /// any other class that has methods to be called when timer
    /// is finished
    /// </summary>
    /// <param name="listener"></param>
    public void AddTimerFinishedListener(UnityAction listener)
    {
        timerFinishedEvent.AddListener(listener);
    }

    #endregion
}
