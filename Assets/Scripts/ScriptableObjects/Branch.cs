using UnityEngine;

[CreateAssetMenu(fileName = "New Branch", menuName = "Branch")]
public class Branch : ScriptableObject
{
    [SerializeField]
    [Tooltip("What do you think this is? This is just all the options or buttons in this branch!")]
    private Option[] OptionsInThisBranch;

    public void SetUp()
    {
        OptionsManager.ClearAllCurrentButtons();
        foreach(Option option in OptionsInThisBranch)
        {
            OptionsManager.CreateButton(option);
        }
        OptionsManager.StartOnThisBranch();
        CheckPointManager.SetCurrentOngoingBranch(this);
    }

    public bool HasOptionsInThisBranch()
    {
        return (OptionsInThisBranch.Length != 0);
    }
}