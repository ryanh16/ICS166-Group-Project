using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerManager : MonoBehaviour
{
    #region Singleton
    public static TimerManager Instance {get; private set;}
    #endregion
    
    #region Private Fields
    [SerializeField] private int numOfMinutes = 1;
    [SerializeField] private GameObject timerUI;
    private Text timerText;
    private float currTime;
    private const int SECONDS_IN_MIN = 60;
    private string stringFormat = "{0:00}:{1:00}"; //This should format the Timer to 00:00.
    private bool timerIsActive = false;
    #endregion

    #region Monobehaviour Callbacks
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        timerText = timerUI.GetComponentInChildren<Text>();

        ResetTimer();
        HideTimer();
    }
    private void Update()
    {
        if (timerIsActive)
        {
            currTime -= Time.deltaTime;
            if (currTime <= 0)
            {
                currTime = 0;
            }
            displayTimer(currTime);
        }
       
    }

    private void OnDestroy()
    {
        Instance = null;
    }
    #endregion

    #region Helper Functions
    public void StartTimer()
    {
        Debug.Log("Starting the Timer");
        timerIsActive = true;
    }
    public void StopTimer()
    {
        Debug.Log("Stopping the Timer.");
        timerIsActive = false;
    }
    public void ResetTimer()
    {
        Debug.Log("Resetting Timer");
        currTime = numOfMinutes * SECONDS_IN_MIN;
        timerIsActive = false;
    }
    public void displayTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / SECONDS_IN_MIN);
        float seconds = Mathf.FloorToInt(time % SECONDS_IN_MIN);

        timerText.text = string.Format(stringFormat, minutes, seconds); //Format the timer to 00:00.
    }
    public void HideTimer()
    {
        Debug.Log("Hiding Timer UI.");
        timerUI.SetActive(false);
    }
    #endregion
}
