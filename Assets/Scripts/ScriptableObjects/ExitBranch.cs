using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Branch", menuName = "Branches/ExitBranch")]
public class ExitBranch : Branch
{
    [SerializeField]
    [Tooltip("This is what you want to display when this dialogue is about to exit.")]
    private Dialogue DialogueWhenExiting;

    public override void OnClickOnThisBranch()
    {
        optionsManager.ClearAllButtons();
        DialogueManager.SetDialogues(DialogueWhenExiting);
    }
}
