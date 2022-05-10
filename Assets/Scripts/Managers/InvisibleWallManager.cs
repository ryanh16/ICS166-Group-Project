using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWallManager : MonoBehaviour
{
    [SerializeField]
    private Dialogue[] DialoguePool;

    [SerializeField]
    private QuittingGameFailManager QM;
    private int RegularIndex = 0;

    private void OnTriggerEnter(Collider other)
    {
        // the wall will plays out all dialogue in order first
        // then it will plays out dialogues randomly
        int i = 0;
        if (RegularIndex < DialoguePool.Length)
        {
            i = RegularIndex;
            RegularIndex += 1;
        }
        else
        {
            i = Random.Range(0, DialoguePool.Length);
        }

        DialogueManager.SetDialogues(DialoguePool[i]);
        DialogueManager.StartDialogue();
        QM.enabled = true;
    }
}
