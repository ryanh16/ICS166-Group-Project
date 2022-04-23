using UnityEngine;

// Script to be placed on interactive objects, gives access to starting dialogue
[DisallowMultipleComponent]
[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;



    public void StartDialogue()
    {
        DialogueManager.setDialogues(dialogue.GetDialogue());
        DialogueManager.startDialogue();
    }
}