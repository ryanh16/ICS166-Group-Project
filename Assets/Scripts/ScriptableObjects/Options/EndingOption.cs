using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewOption", menuName = "Options/EndingOption")]
public class EndingOption : Option
{
    public override void OnClickOnThisOption()
    {
        base.OnClickOnThisOption();
    }

    public override void OnDialogueEnds()
    {
        PlayerIsInThisBranch = false;
        DialogueManager.UnsubscribeFromDialogueEnds(OnDialogueEnds);

        OptionsManager.ClearAllCurrentButtons();
        OptionsManager.EndOnThisBranch();
        DialogueManager.EndDialogue();


        EndingManager EM = GameObject.Find("EndingManager").GetComponent<EndingManager>();
        EM.enabled = true;
    }
}
