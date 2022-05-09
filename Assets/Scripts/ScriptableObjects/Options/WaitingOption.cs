using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewOption", menuName = "Options/WaitingOption")]
public class WaitingOption : Option
{
    public override void OnClickOnThisOption()
    {
        base.OnClickOnThisOption();
    }

    public override void OnDialogueEnds()
    {
        PlayerIsInThisBranch = false;
        DialogueManager.DesubscribeFromDialogueEnds(OnDialogueEnds);

        WaitingManager WM = GameObject.Find("WaitingManager").GetComponent<WaitingManager>();
        WM.StartCounting();

        GameObject InfoBoard = GameObject.Find("Board");
        CheckPoint Cp = InfoBoard.GetComponentInChildren<CheckPoint>();
        Destroy(Cp);

        OptionsManager.ClearAllCurrentButtons();
        OptionsManager.EndOnThisBranch();
        DialogueManager.EndDialogue();
    }
}
