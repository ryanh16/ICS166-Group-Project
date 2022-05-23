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
    private float flashTimer = 0f;
    private float flashDuration = 1f;
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
        ShowTimer(false);
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
        if (secondsToDisplay == 0)
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
    public void ActivateTimer(bool enabled)
    {
        timerIsActive = enabled;
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
        if (secondsToDisplay == SECONDS_IN_MIN)
        {
            ResetTimer();
            minutes = 8;
            secondsToDisplay = 0;
        }
        timerText.text = string.Format(stringFormat, minutes, secondsToDisplay);
    }
    public void ShowTimer(bool enabled)
    {
        timerUI.SetActive(enabled);
    }
    private void FlashTimer()
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
    #endregion
}
