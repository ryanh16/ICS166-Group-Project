using UnityEngine;


// This class deals with the users' input
// Right now there are two ways for users to advance the dialogue:
// 1. press the space bar key; 2. click on the object again

public class InputManager : MonoBehaviour
{
    private GameObject currentObject;



    // Update is called once per frame
    private void Update()
    {
        currentObject = ObjectLookingAt.GetCurrentObject();

        // press the space bar to advance the dialogue
        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (DialogueManager.IsInDialogue())
            {
                DialogueManager.OnContinueButtonClick();
            }
        }

        // left mouse button click to start or advance the dialogue
        if (Input.GetMouseButtonUp(0))
        {
            if (currentObject)
            {
                if (DialogueManager.IsInDialogue())
                {
                    DialogueManager.OnContinueButtonClick();
                }

                else
                {
                    Interactable curInteractable = currentObject.GetComponent<Interactable>();

                    if (curInteractable.HasDialogue())
                    {
                        curInteractable.StartDialogue();
                    }

                    else if (curInteractable.HasTargetScene())
                    {
                        curInteractable.ChangeScene();
                    }
                }
            }

            // if there is an ongoing dialogue, and user clicks on
            // places other than the object, the dialogue will end
            else
            {
                DialogueManager.EndDialogue();
            }
        }
    }
}
