using UnityEngine;

// Scriptable object for storing dialogue "scripts"
[CreateAssetMenu(fileName = "New Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField] private string[] speakers; // Stores array of speakers, their indices will be used in dialogue
    [SerializeField] private string[] dialogue;



    public string GetSpeaker(int spkrIdx)
    {
        return speakers[spkrIdx];
    }


    public string[] GetDialogue()
    {
        return dialogue;
    }
}