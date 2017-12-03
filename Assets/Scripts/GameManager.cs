using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public void StartTimer()
    {  
        if (m_timerStopped)
        {
            ResetTimer();
            m_timerStopped = false;
            m_elapsedSeconds = 0.0f;
        }
    }

    public void StopTimer()
    {
        m_timerStopped = true;
    }

    public void ResetTimer()
    {
        m_secondsLeft = m_timer;

        if (OnTimerChanged != null)
        {
            OnTimerChanged(m_secondsLeft);
        }
    }

    public void EndGame()
    {
        StopTimer();
        Time.timeScale = 0.0f;
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(Instance);
        }
        else
        {
            Instance = this;
        }

        Time.timeScale = 1.0f;
    }

    private void Start()
    {
        ResetTimer();
    }

    private void Update()
    {
        if (!m_timerStopped)
        {
            m_elapsedSeconds += Time.deltaTime;
            if (m_elapsedSeconds > 1)
            {
                m_elapsedSeconds = 0.0f;
                m_secondsLeft -= 1;

                if (OnTimerChanged != null)
                {
                    OnTimerChanged(m_secondsLeft);
                }

                if (m_secondsLeft <= 0)
                {
                    EndGame();
                }
            }
        }
    }

    public int m_timer;

    private int m_secondsLeft;
    private float m_elapsedSeconds = 0.0f;
    private bool m_timerStopped = true;

    public delegate void OnTimerChangedDelegate(int secondsLeft);
    public static OnTimerChangedDelegate OnTimerChanged;

    public static GameManager Instance { get; private set; }
}
