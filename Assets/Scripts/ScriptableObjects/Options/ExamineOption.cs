using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// The overall idea of this option is to allow player to examine 
// some objects (i.e. bus pass).

// This one seems to be a mess, I don't mean to not leave any comments
// but it is hard to leave comments in a mess. At least I can understand
// what this script does ......
[CreateAssetMenu(fileName = "NewOption", menuName = "Options/ExamineOption")]
public class ExamineOption : Option
{
    [SerializeField]
    private Dialogue DialogueToTriggerAfterExamine;

    private ExamineManager EM;
    private GameObject Player;

    private GameObject ExamineBusPass;

    public override void OnClickOnThisOption()
    {
        OptionsManager.ClearAllCurrentButtons();
        OptionsManager.EndOnThisBranch();

        ExamineBusPass = GameObject.Find("ExamineBusPass");

        Player = GameObject.Find("Player");
        Player.GetComponent<Hertzole.GoldPlayer.GoldPlayerController>().enabled = false;

        Camera camera = GameObject.Find("Player Camera").GetComponent<Camera>();

        ExamineBusPass.transform.position = camera.transform.position + camera.transform.forward * 0.5f;
        ExamineBusPass.transform.LookAt(camera.transform);
        ExamineBusPass.transform.Rotate(new Vector3(90, 0, 0));
        ExamineBusPass.transform.Rotate(new Vector3(0, 180, 0));


        EM = GameObject.Find("ExamineManager").GetComponent<ExamineManager>();
        EM.enabled = true;

        ExamineBusPass.SetActive(true);

        EM.StartExamining(ExamineBusPass);
        EM.SubscribeToOnExamineEnds(OnExamineEnds);
    }

    private void OnExamineEnds()
    {
        EM.UnsubscribeFromOnExamineEnds(OnExamineEnds);
        DialogueManager.SetDialogues(DialogueToTriggerAfterExamine);
        DialogueManager.StartDialogue();
        DialogueManager.SubscribeToDialogueEnds(OnDialogueEnds);

        // advance the event chain
        EventManager.Instance.AdvanceToNextEvent();
    }

    public override void OnDialogueEnds()
    {
        DialogueManager.UnsubscribeFromDialogueEnds(OnDialogueEnds);
        ExamineBusPass.SetActive(false);
    }
}
