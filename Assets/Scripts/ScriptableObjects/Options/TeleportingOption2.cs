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
            FlashbackUIManager FM = GameObject.Find("FlashbackUIManager").GetComponent<FlashbackUIManager>();
            FM.Teleport(GameObject.Find("Player"), Destination);
            FM.SubscribeToTeleportEnds(OnTeleportEnds);
        }
    }

    public override void OnDialogueEnds()
    {
        Destination = GameObject.Find("NotQuittingTelePointInBusArea").transform;
        DialogueManager.UnsubscribeFromDialogueEnds(OnDialogueEnds);
        FlashbackUIManager FM = GameObject.Find("FlashbackUIManager").GetComponent<FlashbackUIManager>();
        FM.Teleport(GameObject.Find("Player"), Destination);
        FM.SubscribeToTeleportEnds(OnTeleportEnds);
    }

    public void OnTeleportEnds()
    {
        FlashbackUIManager FM = GameObject.Find("FlashbackUIManager").GetComponent<FlashbackUIManager>();
        FM.DesubscribeFromTeleportEnds(OnTeleportEnds);
        if (DialogueAfterTeleporting)
        {
            DialogueManager.SetDialogues(DialogueAfterTeleporting);
            DialogueManager.StartDialogue();
        }
    }
}
