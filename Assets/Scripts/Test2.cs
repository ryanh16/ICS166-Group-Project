using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public void onClick()
    {
        string[] d = { "Hello", "This is Test2!" };
        DialogueManager.setDialogues(d, "test2");
        DialogueManager.startDialogue();
    }
}
