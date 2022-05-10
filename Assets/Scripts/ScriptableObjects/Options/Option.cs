using UnityEngine;

[CreateAssetMenu(fileName = "New Option", menuName = "Options/Option")]
public class Option : ScriptableObject
{
    [SerializeField]
    [Tooltip("This is the name of this the option/button.")]
    private string Name;

    [SerializeField]
    [Tooltip("This is the dialogues you want to start when player clicks on this option/button.")]
    protected Dialogue DialogueToStart;

    [SerializeField]
    [Tooltip("This is the following branch that will show after the dialogue ends.")]
    private Branch FollowingBranch;

    [SerializeField]
    [Tooltip("This is the starting branch within the checkpoint, i.e. the branch will return to this mainbranch if there is no FollowingBranch.\n" +
        "For more information of how to set up, please refer to Option.cs script.")]
    private Branch MainBranch;

    [SerializeField]
    [Tooltip("I set this to SerializeField just to make life easier!")]
    protected bool PlayerIsInThisBranch = false;

    public string GetName()
    {
        return Name;
    }

    // OnClickOnThisOption() is added as a listener to cooresponding buttons
    // When button is clicked, the cooresponding ScriptableObject's this
    // method will be called 
    public virtual void OnClickOnThisOption()
    {
        if (DialogueToStart)
        {
            // if this option contains any dialogue, the dialogue will be played first
            // and then the behavior after will be decided in OnDialogueEnds() method
            OptionsManager.ClearAllCurrentButtons();
            DialogueManager.SetDialogues(DialogueToStart);
            DialogueManager.StartDialogue();
            PlayerIsInThisBranch = true;
            DialogueManager.SubscribeToDialogueEnds(OnDialogueEnds);
        }
        else
        {
            // if this option does not contain dialogue
            if (FollowingBranch && !MainBranch)
            {
                // if the option has following branch, it will start the following branch
                FollowingBranch.SetUp();
            }
            else if (MainBranch && !FollowingBranch)
            {
                // if the option has no following branch, but has a main branch that this
                // current branch is derived from, then it will go back to the main branch
                MainBranch.SetUp();
            }
            else
            {
                // if he option has neither following branch not main branch, then this
                // option is considered as an exit option, and it will exit the branch
                OptionsManager.ClearAllCurrentButtons();
                OptionsManager.EndOnThisBranch();
            }
        }
    }

    // OnDialogueEnds() will decide the what will happen after the dialogue ends
    // I copied and pasted all comments in this method from OnClickOnThisOption()
    public virtual void OnDialogueEnds()
    {
        if (PlayerIsInThisBranch)
        {
            PlayerIsInThisBranch = false;
            DialogueManager.DesubscribeFromDialogueEnds(OnDialogueEnds);

            if (FollowingBranch && !MainBranch)
            {
                // if the option has following branch, it will start the following branch
                FollowingBranch.SetUp();
            }
            else if (MainBranch && !FollowingBranch)
            {
                // if the option has no following branch, but has a main branch that this
                // current branch is derived from, then it will go back to the main branch
                MainBranch.SetUp();
            }
            else
            {
                // if he option has neither following branch not main branch, then this
                // option is considered as an exit option, and it will exit the branch
                OptionsManager.ClearAllCurrentButtons();
                OptionsManager.EndOnThisBranch();
            }
        }
    }
}
