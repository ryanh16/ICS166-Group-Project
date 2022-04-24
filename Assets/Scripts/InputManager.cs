using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (DialogueManager.IsInDialogue())
            {
                DialogueManager.OnContinueButtonClick();
            }
        }
    }
}
