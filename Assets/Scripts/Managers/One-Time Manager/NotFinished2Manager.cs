using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotFinished2Manager : MonoBehaviour
{
    [SerializeField]
    private Dialogue DialogueAfterTeleporting;

    private void OnTriggerEnter(Collider other)
    {
        EventManager.Instance.AdvanceToNextEvent();
        FlashbackUIManager.Instance.Teleport(other.gameObject, other.transform);
        FlashbackUIManager.Instance.SubscribeToTeleportEnds(OnTeleportEnds);
        TimerManager.Instance.ShowTimer(false);

        Destroy(gameObject);
    }

    private void OnTeleportEnds()
    {
        FlashbackUIManager.Instance.DesubscribeFromTeleportEnds(OnTeleportEnds);
        TimerManager.Instance.ShowTimer(true);
        TimerManager.Instance.SetTimer(9, 33);

        DialogueManager.SetDialogues(DialogueAfterTeleporting);
        DialogueManager.StartDialogue();
    }
}
