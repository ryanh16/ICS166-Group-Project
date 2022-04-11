using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test1 : MonoBehaviour
{
    public void onClick()
    {
        string[] d = { "Hello", "this is Test1!", "vniuewbviuerhuiv4uihviurehbivherubhieuhbiuehuehiurbheiuhviowjfpoqjfoijweiovhoiwn oijvoiwn iuwpofjw09fh98hfwhvujoiefjwoihfuiehvoijvuhsudbvhsoibhvsoioiwhfo8weeoigheoiugiehgoishvhe9gheubhierbi" };
        DialogueManager.setDialogues(d, "test1");
        DialogueManager.startDialogue();
    }
}
