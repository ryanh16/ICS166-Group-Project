using UnityEngine;

// Script to be placed on interactive objects, gives access to starting dialogue
[DisallowMultipleComponent]
[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private Transform targetLocation;



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
            CharacterController playerController = player.GetComponent<CharacterController>();

            // TODO: Add code to do fade-out transition
            playerController.enabled = false;
            player.transform.position = new Vector3(targetLocation.position.x, targetLocation.position.y + 1, targetLocation.position.z);
            playerController.enabled = true;
            // TODO: Add code to do fade-in transition
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