using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// this will assign teleport player to the busspawn location
[CreateAssetMenu(fileName = "NewOption", menuName = "Options/TeleportingOption2")]
public class TeleportingOption2 : Option
{
    [SerializeField]
    [Tooltip("Somehow this is not assignable, I don't know why!!!")]
    private Transform Destination;

    [SerializeField]
    private Dialogue DialogueAfterTeleporting;

    public override void OnClickOnThisOption()
    {
        OptionsManager.ClearAllCurrentButtons();
        if (DialogueToStart)
        {
            DialogueManager.SetDialogues(DialogueToStart);
            DialogueManager.StartDialogue();
            DialogueManager.SubscribeToDialogueEnds(OnDialogueEnds);
        }
        else
        {
            Destination = GameObject.Find("BusSpawn").transform;
            FlashbackUIManager.Instance.Teleport(GameObject.Find("Player"), Destination);
            FlashbackUIManager.Instance.SubscribeToTeleportEnds(OnTeleportEnds);
        }
    }

    public override void OnDialogueEnds()
    {
        Destination = GameObject.Find("BusSpawn").transform;
        DialogueManager.UnsubscribeFromDialogueEnds(OnDialogueEnds);
        FlashbackUIManager.Instance.Teleport(GameObject.Find("Player"), Destination);
        FlashbackUIManager.Instance.SubscribeToTeleportEnds(OnTeleportEnds);

        OptionsManager.ClearAllCurrentButtons();
        OptionsManager.EndOnThisBranch();

        EventManager.Instance.AdvanceToNextEvent();
    }

    public void OnTeleportEnds()
    {
        FlashbackUIManager.Instance.DesubscribeFromTeleportEnds(OnTeleportEnds);
        if (DialogueAfterTeleporting)
        {
            DialogueManager.SetDialogues(DialogueAfterTeleporting);
            DialogueManager.StartDialogue();
        }
    }
}
