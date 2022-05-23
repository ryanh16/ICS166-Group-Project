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
    //[SerializeField] private int numOfMinutes = 1;
    [SerializeField] private GameObject timerUI;
    [SerializeField] private int secondsToMinuteConversion = 10;
    private Text timerText;
    private float currTime;
    private const int SECONDS_IN_MIN = 60;
    private string stringFormat = "{0:00}:{1:00}am"; //This should format the Timer to 00:00.
    private bool timerIsActive = false;
    private int secondsToDisplay = 10;
    private bool hasDisplayedSeconds = false;
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
            currTime += Time.deltaTime;
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
        timerIsActive = true;
    }
    public void StopTimer()
    {
        timerIsActive = false;
    }
    public void ResetTimer()
    {
        currTime = SECONDS_IN_MIN * 7;
        timerIsActive = false;
    }
    public void displayTimer(float time)
    {
        //float minutes = Mathf.RoundToInt(time / SECONDS_IN_MIN);
        //Debug.Log(minutes);
        int minutes = 7;
        float seconds = Mathf.RoundToInt(time % SECONDS_IN_MIN);
        if (seconds % secondsToMinuteConversion == 0 && seconds != 0 && !hasDisplayedSeconds)
        {
            secondsToDisplay += 1;
            hasDisplayedSeconds = true;
        }
        if (seconds % secondsToMinuteConversion != 0 && hasDisplayedSeconds)
        {
            hasDisplayedSeconds = false;
        }
        if (secondsToDisplay == 60)
        {
            ResetTimer();
            minutes = 8;
            secondsToDisplay = 0;
        }
        timerText.text = string.Format(stringFormat, minutes, secondsToDisplay);
    }
    public void HideTimer()
    {
        timerUI.SetActive(false);
    }
    public void ShowTimer()
    {
        timerUI.SetActive(true);
    }
    #endregion
}
