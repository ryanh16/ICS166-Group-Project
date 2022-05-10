using UnityEngine;

// Scriptable object for storing dialogue "scripts"
[CreateAssetMenu(fileName = "New Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField] private string[] speakers; // Stores array of speakers, their indices will be used in dialogue
    [TextArea(1, 3)] [SerializeField] private string[] dialogue;



    public string GetSpeaker(int spkrIdx)
    {
        return speakers[spkrIdx];
    }


    public string[] GetDialogue()
    {
        return dialogue;
    }


    public bool HasSpeakers()
    {
        return speakers.Length > 0;
    }


    public bool HasDialogue()
    {
        return dialogue.Length > 0;
    }
}