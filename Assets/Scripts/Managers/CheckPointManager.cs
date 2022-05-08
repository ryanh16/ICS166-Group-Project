using System.Collections;
using System.Collections.Generic;
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
}
