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
        TimerManager.Instance.SetTimer(9, 50);
    }
}
