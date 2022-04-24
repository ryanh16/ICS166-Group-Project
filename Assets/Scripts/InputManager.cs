using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private GameObject currentObject;

    // Update is called once per frame
    void Update()
    {
        currentObject = ObjectLookingAt.GetCurrentObject();

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (DialogueManager.IsInDialogue())
            {
                DialogueManager.OnContinueButtonClick();
            }
        }

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
                    currentObject.GetComponent<Interactable>().StartDialogue();
                }
            }
            else
            {
                DialogueManager.EndDialogue();
            }
        }
    }
}
