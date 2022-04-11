using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test2 : MonoBehaviour
{
    public DialogueManager dm;

    public void onClick()
    {
        string[] d = { "Hello", "This is Test2!" };
        dm.setDialogues(d, "test2");
        dm.startDialogue();
    }
}
