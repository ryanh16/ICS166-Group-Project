using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    public DialogueManager dm;
   
    public void onClick()
    {
        string[] d = { "Hello", "This is Test1!" };
        dm.setDialogues(d, "test1");
        dm.startDialogue();
    }
}
