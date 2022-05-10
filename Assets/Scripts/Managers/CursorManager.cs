using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// This class just deals with the cursor according to the game state
public class CursorManager : MonoBehaviour
{
    [SerializeField]
    private FlashbackUIManager FM;

    // Update is called once per frame
    void LateUpdate()
    {
       if (OptionsManager.HasButtonsOnScreen())
       {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            return;
       }

       if (DialogueManager.IsInDialogue())
       {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            return;
       }

       if (FM.IsDuringTeleport())
       {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            return;
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
}
