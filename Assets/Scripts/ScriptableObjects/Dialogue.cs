using UnityEngine;

// Scriptable object for storing dialogue "scripts"
[CreateAssetMenu(fileName = "New Dialogue")]
public class Dialogue : ScriptableObject
{
    [SerializeField] private string[] dialogue;


    public string[] GetDialogue()
    {
        return dialogue;
    }
}