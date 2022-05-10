using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleWallManager : MonoBehaviour
{
    [SerializeField]
    private Dialogue[] DialoguePool;

    [SerializeField]
    private QuittingGameFailManager QM;

    private void OnTriggerEnter(Collider other)
    {
        int randowIndex = Random.Range(0, DialoguePool.Length);
        DialogueManager.SetDialogues(DialoguePool[randowIndex]);
        DialogueManager.StartDialogue();
        QM.enabled = true;
    }
}
