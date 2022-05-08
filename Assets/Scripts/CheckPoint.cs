using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField]
    private Branch[] Branches;

    [SerializeField]
    private Dialogue DialogueInThisCP;

    [SerializeField]
    private OptionsManager optionManager;

    private bool PlayerIsHere = false;

    private void Start()
    {
        DialogueManager.SubscibeToDialogueEnds(WhenDialogueEnds);
    }

    private void OnTriggerEnter(Collider other)
    {
        CheckPointManager.SetCurrentCheckPoint(this.gameObject.GetComponent<CheckPoint>());
        ObjectLookingAt.SetCurrentObject(this.gameObject);
        if (DialogueInThisCP)
        {
            DialogueManager.SetDialogues(DialogueInThisCP);
            PlayerIsHere = true;
            DialogueManager.StartDialogue();
        }
    }

    private void WhenDialogueEnds()
    {
        if (PlayerIsHere)
        {
            foreach (Branch b in Branches)
            {
                optionManager.CreateButton(b);
            }
            optionManager.FinishSettingUpButtons();
            PlayerIsHere = false;
        }
    }
}
