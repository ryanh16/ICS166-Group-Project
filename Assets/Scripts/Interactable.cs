using UnityEngine;

// Script to be placed on interactive objects, gives access to starting dialogue
[DisallowMultipleComponent]
[RequireComponent(typeof(Outline))]
public class Interactable : MonoBehaviour
{
    [SerializeField] private Dialogue dialogue;
    [SerializeField] private SceneTypes.Scenes targetScene;



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


    public void ChangeScene()
    {
        if (HasTargetScene())
            SceneLoadManager.LoadScene(targetScene);

        else
            Debug.LogError($"Target scene on Interactable: {this.gameObject.name} set to NONE, should not be loading scene");
    }


    public bool HasDialogue()
    {
        if (dialogue == null || !dialogue.HasDialogue())
        {
            return false;
        }

        return true;
    }


    public bool HasTargetScene()
    {
        return targetScene != SceneTypes.Scenes.NONE;
    }
}