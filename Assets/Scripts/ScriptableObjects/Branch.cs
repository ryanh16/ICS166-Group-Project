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
                foreach (Branch b in MainBranch.GetOptions())
                {
                    optionsManager.CreateButton(b);
                }
                optionsManager.FinishSettingUpButtons();
            }
        }
        else
        {
            foreach (Branch b in Options)
            {
                optionsManager.CreateButton(b);
            }
            optionsManager.FinishSettingUpButtons();
        }
    }

    public void WhenDialogueEnds()
    {
        if (PlayerIsInThisBranch)
        {
            foreach (Branch b in MainBranch.GetOptions())
            {
                optionsManager.CreateButton(b);
            }
            optionsManager.FinishSettingUpButtons();
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
