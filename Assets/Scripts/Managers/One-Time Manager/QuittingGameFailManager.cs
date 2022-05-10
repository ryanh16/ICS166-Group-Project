using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is one time manager that deals with the game behavior when player
// jumps off the road but chooses not to quit the game. This script will
// not be enabled until player chooses not to quit the game and after being 
// teleported and collided with a certain game object with EnableQuittingGameFailManager
// script attached to it.
// This class simply starts a dialogue if we supply any and allows player
// to advance the dialogue (because in this case, there is no Interactable or
// CheckPoint object selected, the normal way to handle input in InputManager
// will not work here).
public class QuittingGameFailManager : MonoBehaviour
{
    private void OnEnable()
    {
        DialogueManager.SubscribeToDialogueEnds(OnDialogueEnds);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            DialogueManager.OnContinueButtonClick();
        }

        if (Input.GetMouseButtonUp(0))
        {
            DialogueManager.OnContinueButtonClick();
        }
    }

    public void OnDialogueEnds()
    {
        DialogueManager.UnsubscribeFromDialogueEnds(OnDialogueEnds);
        this.enabled = false;
    }
}
