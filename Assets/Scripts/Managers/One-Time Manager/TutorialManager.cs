using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    [SerializeField]
    private Dialogue TutorialDia;

    private void OnLevelWasLoaded(int level)
    {
        StartCoroutine(WaitForHalfSeconds());
    }

    IEnumerator WaitForHalfSeconds()
    {
        yield return new WaitForSeconds(0.5f);
        DialogueManager.SetDialogues(TutorialDia);
        DialogueManager.StartDialogue();
        DialogueManager.SubscribeToDialogueEnds(OnDialogueEnds);
    }

    void OnDialogueEnds()
    {
        DialogueManager.UnsubscribeFromDialogueEnds(OnDialogueEnds);
        this.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0)) && DialogueManager.IsInDialogue())
        {
            DialogueManager.OnContinueButtonClick();
        }
    }
}
