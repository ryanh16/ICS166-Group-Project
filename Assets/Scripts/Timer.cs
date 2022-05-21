using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    #region Private Fields
    [SerializeField] private int numOfMinutes = 1;
    [SerializeField] private Text timerText;
    private float currTime;
    private const int SECONDS_IN_MIN = 60;
    private string stringFormat = "{0:00}:{1:00}"; //This should format the Timer to 00:00.
    private bool timerIsActive = false;
    #endregion

    #region Monobehaviour Callbacks
    private void Start()
    {
        currTime = numOfMinutes * SECONDS_IN_MIN; //To start, currTime should hold the float value of minutes set in the Inspector.
    }
    private void Update()
    {
        timerIsActive = true;
        if (timerIsActive)
        {
            currTime -= Time.deltaTime;
            if (currTime <= 0)
            {
                currTime = 0;
            }
        }
        displayTimer(currTime);
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
    public void displayTimer(float time)
    {
        float minutes = Mathf.FloorToInt(time / SECONDS_IN_MIN);
        float seconds = Mathf.FloorToInt(time % SECONDS_IN_MIN);

        timerText.text = string.Format(stringFormat, minutes, seconds); //Format the timer to 00:00.
    }
    #endregion
}
