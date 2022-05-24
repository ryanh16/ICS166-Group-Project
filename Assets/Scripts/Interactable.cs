using UnityEngine;

// Script to be placed on interactive objects, gives access to starting dialogue
[DisallowMultipleComponent]
[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private Transform targetLocation;


    public void OnInteractingWith()
    {
        if (dialogue)
        {
            StartDialogue();
            DialogueManager.SubscribeToDialogueEnds(OnDialogueEnds);
        }
        else if (dialogue == null && targetLocation)
        {
            SubscriberToEventManager STEM = GetComponent<SubscriberToEventManager>();
            if (STEM && STEM.CanAdvanceEventOrNot())
            {
                EventManager.Instance.AdvanceToNextEvent();
            }
            ChangeLocation();
        }
    }

    private void OnDialogueEnds()
    {
        DialogueManager.UnsubscribeFromDialogueEnds(OnDialogueEnds);

        SubscriberToEventManager STEM = GetComponent<SubscriberToEventManager>();
        CheckPoint CP = GetComponent<CheckPoint>();

        if (!CP && STEM && STEM.CanAdvanceEventOrNot())
        {
            EventManager.Instance.AdvanceToNextEvent();
        }

        if (targetLocation)
        {
            ChangeLocation();
        }
    }

    public void StartDialogue()
    {
        if (dialogue.HasDialogue() && dialogue.HasSpeakers())
        {
            DialogueManager.SetDialogues(dialogue);
            DialogueManager.StartDialogue();
        }

        else
        {
            Debug.LogError($"Dialogue misconfiguration on Interactable: {this.gameObject.name}");
        }
    }


    public void ChangeLocation()
    {
        if (HasTargetLocation())
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");

            FlashbackUIManager.Instance.Teleport(player, targetLocation);
        }

        else
            Debug.LogError($"No target location on Interactable: {this.gameObject.name}");
    }


    public bool HasDialogue()
    {
        if (dialogue == null || !dialogue.HasDialogue())
        {
            return false;
        }

        return true;
    }


    public bool HasTargetLocation()
    {
        return targetLocation != null;
    }
}