using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Branch", menuName = "Branches/Branch")]
public class Branch : ScriptableObject
{
    [SerializeField]
    private string Name;
    [SerializeField]
    private Branch[] Options;
    [SerializeField]
    private Dialogue Dia;
    [SerializeField]
    protected OptionsManager optionsManager;
    [SerializeField]
    private Branch MainBranch;

    private bool PlayerIsInThisBranch = false;

    [SerializeField]
    private bool LeadingToComplete;

    private void Awake()
    {
        DialogueManager.SubscibeToDialogueEnds(WhenDialogueEnds);
        optionsManager = GameObject.Find("Options").GetComponent<OptionsManager>();
    }

    public string GetName()
    {
        return name;
    }

    public virtual void OnClickOnThisBranch()
    {
        Debug.Log("click on this branch is called");
        if (Options.Length == 0)
        {
            if (Dia)
            {
                optionsManager.ClearAllButtons();
                DialogueManager.SetDialogues(Dia);
                DialogueManager.StartDialogue();
                PlayerIsInThisBranch = true;
            }
            else
            {
                optionsManager.CreateButtons(MainBranch.GetOptions());
            }
        }
    }

    public void WhenDialogueEnds()
    {
        if (PlayerIsInThisBranch)
        {
            optionsManager.CreateButtons(MainBranch.GetOptions());
            PlayerIsInThisBranch = false;
        }
    }

    public bool LeadingToCompleteThisItem()
    {
        return LeadingToComplete;
    }

    public Branch[] GetOptions()
    {
        return Options;
    }
}
