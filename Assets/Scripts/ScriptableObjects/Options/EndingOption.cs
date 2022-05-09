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
        DialogueManager.DesubscribeFromDialogueEnds(OnDialogueEnds);

        EndingManager EM = GameObject.Find("EndingManager").GetComponent<EndingManager>();
        EM.Ending();

        OptionsManager.ClearAllCurrentButtons();
        OptionsManager.EndOnThisBranch();
        DialogueManager.EndDialogue();
    }
}
