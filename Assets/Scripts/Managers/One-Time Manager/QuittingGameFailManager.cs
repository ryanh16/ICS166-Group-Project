using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        DialogueManager.DesubscribeFromDialogueEnds(OnDialogueEnds);
        this.enabled = false;
    }
}
