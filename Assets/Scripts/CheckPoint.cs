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

    [SerializeField]
    [Tooltip("This distance determines how close should player be to this GameObject in order to start the branch.\n" +
        "If you don't want to allow maunal activate on this GameObject then don't assign this field.")]
    private float Distance = 10;

    [SerializeField]
    [Tooltip("If you don't want to allow maunal activate on this GameObject then don't assign this field.")]
    private GameObject Player;

    // Auto activate checkpoint when player touches the checkpoint
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

    // This method is just used to manually activate the checkpoint
    // used in gameobjects like info person, etc.
    public void ManuallyActivateCheckPoint()
    {
        if (Player == null)
        {
            Debug.LogError($"CheckPoint GameObject {gameObject.name} cannot be manully activated!");
        }

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

    public bool CanManullyActivate()
    {
        if (Player == null || Vector3.Distance(Player.transform.position, this.transform.position) > Distance)
        {
            return false;
        }
        else
        {
            return true;
        }
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
