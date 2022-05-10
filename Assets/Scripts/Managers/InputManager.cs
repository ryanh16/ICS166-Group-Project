using UnityEngine;


// This class deals with the users' input
// Right now there are two ways for users to advance the dialogue:
// 1. press the space bar key; 2. click on the object again

public class InputManager : MonoBehaviour
{
    private GameObject currentObject;
    [SerializeField]
    private FlashbackUIManager FM;

    // Update is called once per frame
    private void Update()
    {
        currentObject = ObjectLookingAt.GetCurrentObject();

        // press the space bar to advance the dialogue
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (currentObject)
            {
                Interactable curInteractable = currentObject.GetComponent<Interactable>();

                if (DialogueManager.IsInDialogue())
                {
                    DialogueManager.OnContinueButtonClick();

                    // If dialogue ended after advancing and has a target location, teleport
                    if (!DialogueManager.IsInDialogue() && curInteractable.HasTargetLocation())
                    {
                        curInteractable.ChangeLocation();
                    }
                }
            }
        }

        // left mouse button click to start or advance the dialogue
        if (Input.GetMouseButtonUp(0))
        {
            if (!OptionsManager.HasButtonsOnScreen() && !FM.IsDuringTeleport() && currentObject)
            {
                Interactable curInteractable = currentObject.GetComponent<Interactable>();
                CheckPoint curCheckPoint = currentObject.GetComponent<CheckPoint>();

                if (curCheckPoint)
                {
                    if (!OptionsManager.IsCurrentlyInBranch() && curCheckPoint.CanManullyActivate())
                    {
                        curCheckPoint.ManuallyActivateCheckPoint();
                        return;
                    }
                }

                if (DialogueManager.IsInDialogue())
                {
                    DialogueManager.OnContinueButtonClick();

                    // If dialogue ended after advancing and has a target location, teleport
                    if (!DialogueManager.IsInDialogue() && curInteractable.HasTargetLocation())
                    {
                        curInteractable.ChangeLocation();
                    }
                }

                else
                {
                    // If Interactable has dialogue, start dialogue
                    if (curInteractable.HasDialogue())
                    {
                        curInteractable.StartDialogue();
                    }

                    // If Interactable only has location, teleport to location immediately
                    else if (curInteractable.HasTargetLocation())
                    {
                        curInteractable.ChangeLocation();
                    }

                    // If Interactable has neither, object is misconfigured
                    else
                    {
                        Debug.LogError($"{currentObject.name} is Interactable but has neither a Dialogue nor TargetLocation attached, check Inspector values");
                    }
                }
            }
        }

        // Return/Enter key exits dialogue early
        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (DialogueManager.IsInDialogue())
            {
                DialogueManager.EndDialogue();
            }
            OptionsManager.ClearAllCurrentButtons();
            OptionsManager.EndOnThisBranch();
        }
    }
}