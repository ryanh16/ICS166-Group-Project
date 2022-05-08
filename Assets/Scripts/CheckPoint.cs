using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    private Branch BranchInThisCP;

    [SerializeField]
    private Dialogue DialogueInThisCP;

    private bool PlayerIsHere = false;

    private void OnTriggerEnter(Collider other)
    {
        CheckPointManager.SetCurrentCheckPoint(this.gameObject.GetComponent<CheckPoint>());
        ObjectLookingAt.SetCurrentObject(this.gameObject);
        if (DialogueInThisCP)
        {
            // if there is any dialogue, play the dialogue first
            DialogueManager.SubscribeToDialogueEnds(WhenDialogueEnds);

            DialogueManager.SetDialogues(DialogueInThisCP);
            PlayerIsHere = true;
            DialogueManager.StartDialogue();
        }
        else
        {
            BranchInThisCP.SetUp();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerIsHere = false;
    }

    private void WhenDialogueEnds()
    {
        if (PlayerIsHere)
        {
            PlayerIsHere = false;
            DialogueManager.DesubscribeFromDialogueEnds(WhenDialogueEnds);
            BranchInThisCP.SetUp();
        }
    }
}
