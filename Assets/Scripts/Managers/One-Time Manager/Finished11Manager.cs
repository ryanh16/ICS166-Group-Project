using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finished11Manager : MonoBehaviour
{
    [SerializeField]
    private Transform TeleportDestination;

    [SerializeField]
    private Dialogue Before8;

    [SerializeField]
    private Dialogue After8;

    [SerializeField]
    private Dialogue PostBefore8;

    [SerializeField]
    private Dialogue PostAfter8;

    private Dialogue DiaAfterTeleporting;

    private void OnTriggerEnter(Collider other)
    {
        if (TimerManager.Instance.GetTimeInText().ToCharArray()[1] != '8')
        {
            DialogueManager.SetDialogues(Before8);
            DialogueManager.StartDialogue();
            DiaAfterTeleporting = PostBefore8;
        }
        else
        {
            DialogueManager.SetDialogues(After8);
            DialogueManager.StartDialogue();
            DiaAfterTeleporting = PostAfter8;
        }
        DialogueManager.SubscribeToDialogueEnds(OnDialogueEnds);
    }

    private void OnDialogueEnds()
    {
        DialogueManager.UnsubscribeFromDialogueEnds(OnDialogueEnds);
        FlashbackUIManager.Instance.Teleport(GameObject.Find("Player"), TeleportDestination);
        FlashbackUIManager.Instance.SubscribeToTeleportEnds(OnTeleportEnds);
        // dont if this is the right way to do so
        TimerManager.Instance.ShowTimer(false);
        TimerManager.Instance.ResetTimer();
    }

    private void OnTeleportEnds()
    {
        FlashbackUIManager.Instance.DesubscribeFromTeleportEnds(OnTeleportEnds);
        DialogueManager.SetDialogues(DiaAfterTeleporting);
        DialogueManager.StartDialogue();
    }
}
