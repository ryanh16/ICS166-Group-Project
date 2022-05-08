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

    public void SetOptionsManager(OptionsManager om)
    {
        optionsManager = om;
        // since Awake is not working properly, I will need to put the subscribe method here
        DialogueManager.SubscibeToDialogueEnds(WhenDialogueEnds);
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
                Debug.Log("this should be called by test branch2");
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
                if (optionsManager == null)
                {
                    Debug.Log("optionsManager is null");
                }
                optionsManager.CreateButton(b);
            }
            optionsManager.FinishSettingUpButtons();
        }
    }


    public void WhenDialogueEnds()
    {
        Debug.Log("this should be called when the dia in test branch2 finishes");
        Debug.Log($"and playerisinDia is {PlayerIsInThisBranch}");
        if (PlayerIsInThisBranch)
        {
            Debug.Log("this should be called when the dia in test branch2 finishes");
            optionsManager.CreateButton(MainBranch);
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