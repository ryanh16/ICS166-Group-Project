using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    [SerializeField]
    private static CheckPoint CurrentCheckPoint;
    [SerializeField]
    private static CheckPoint LastCheckPoint = null;
    [SerializeField]
    private static GameObject Player;
    [SerializeField]
    private static FlashbackUIManager FlashBackManager;
    [SerializeField]
    [Tooltip("I set this to SerializeField just to make life easier!")]
    private static Branch CurrentBranch;



    public static void SetCurrentCheckPoint(CheckPoint CP)
    {
        if (LastCheckPoint && LastCheckPoint.GetInstanceID() != CurrentCheckPoint.GetInstanceID())
        {
            LastCheckPoint = CurrentCheckPoint;
        }
        CurrentCheckPoint = CP;
    }


    public static void ReturnToLastCheckPoint()
    {
        CurrentCheckPoint = LastCheckPoint;
        FlashBackManager.Teleport(Player, LastCheckPoint.transform);
    }


    public static CheckPoint GetCurrentCheckPoint()
    {
        return CurrentCheckPoint;
    }
    

    // Somehow I feel keeping track of current branch can be useful
    public static void SetCurrentOngoingBranch(Branch currentBranch)
    {
        CurrentBranch = currentBranch;
    }
}