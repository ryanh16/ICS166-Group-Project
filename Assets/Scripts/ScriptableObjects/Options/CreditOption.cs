using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewOption", menuName = "Options/CreditOption")]
public class CreditOption : Option
{
    public override void OnDialogueEnds()
    {
        base.OnDialogueEnds();
        if (!EventManager.Instance.HasReachedTheEnd())
        {
            EventManager.Instance.AdvanceToNextEvent();
        }
        // Set timer to 9:50 here
        string curTime = TimerManager.Instance.GetTimeInText();

        int hour = curTime[0]*10 + curTime[1];
        int min = curTime[3] * 10 + curTime[4];
        // if the time is bigger than 9:50 already
        bool BiggerThan950 = ((hour > 9) || ((hour == 9) && (min > 50)));
        

        if (!BiggerThan950)
        {
            TimerManager.Instance.SetTimer(9, 50);
        }
    }
}
