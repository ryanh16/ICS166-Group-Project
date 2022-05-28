using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

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
    private int minutesToDisplay;
    private int secondsToDisplay;
    private bool hasDisplayedSeconds = false;
    private float flashTimer = 0f;
    private float flashDuration = 1f;
    private Action On745;
    private Action On735;
    private Action On755;
    private Action On8am;
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

        ResetTimer(); //To start, Timer should be at 7:10am.
        ShowTimer(false); //And the UI shouldn't be displayed yet.
    }
    private void Update()
    {
        if (timerIsActive)  //If the Timer is currently active,
        {
            currTime += Time.deltaTime; //Increment by time since
            if (currTime <= 0)
            {
                currTime = 0;
            }
            displayTimer(currTime); //and display it. currTime should hold the Time
        } 
        if (minutesToDisplay == 8 && secondsToDisplay == 0)
        {
            FlashTimer();
        }   
    }
    private void OnDestroy()
    {
        Instance = null;
    }
    #endregion

    #region Helper Functions
    public void ActivateTimer(bool enabled) //Setting the timer to active or not active.
    {
        timerIsActive = enabled;
    }
    public void ResetTimer() //Resets the Timer text to display 7 minutes and 10 minutes, but deactivates the Timer.
    {                        //When using ResetTimer, do not forget to ActivateTimer(true) to get it running again.
        currTime = SECONDS_IN_MIN * 7;
        minutesToDisplay = 7;
        secondsToDisplay = 10;
        timerText.text = string.Format(stringFormat, minutesToDisplay, secondsToDisplay);
        timerIsActive = false;
    }
    public void displayTimer(float time)
    {
        //float minutes = Mathf.RoundToInt(time / SECONDS_IN_MIN);
        //Debug.Log(minutes);
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
        if (secondsToDisplay == SECONDS_IN_MIN)
        {
            //ResetTimer();
            minutesToDisplay += 1;
            secondsToDisplay = 0;
        }
        timerText.text = string.Format(stringFormat, minutesToDisplay, secondsToDisplay);

        if (timerText.text == "07:12am")
        {
            On745?.Invoke();
        }

        if (timerText.text == "09:50am")
        {
            if (!EventManager.Instance.HasReachedTheEnd())
            {
                EventManager.Instance.AdvanceToNextEvent();
            }
        }
    }
    public void ShowTimer(bool enabled) //This is setting the UI to be shown or not.
    {
        timerUI.SetActive(enabled);
    }
    private void FlashTimer() //This is to Flash the Timer, right now it only flashes when Timer is 8:00am.
    {
        if (flashTimer <= 0)
        {
            flashTimer = flashDuration;
        }
        else if (flashTimer >= flashDuration / 2)
        {
            flashTimer -= Time.deltaTime;
            ShowTimer(false);
        }
        else
        {
            flashTimer -= Time.deltaTime;
            ShowTimer(true);
        }
    }

    public void SetTimer(int minutes, int seconds) //Sets the Timer to specified time in minutes and seconds.
    {
        minutesToDisplay = minutes;
        secondsToDisplay = seconds;
        timerText.text = string.Format(stringFormat, minutesToDisplay, secondsToDisplay);
    }

    public void SubscribeTo745(Action action)
    {
        On745 += action;
    }

    public void UnsubscribeFrom745(Action action)
    {
        On745 -= action;
    }

    public string GetTimeInText()
    {
        return timerText.text;
    }
    #endregion
}
