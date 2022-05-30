using UnityEngine;

public class EndingManager : MonoBehaviour
{
    [SerializeField]
    private Transform TeleportDestination;

    private void OnTriggerEnter(Collider other)
    {
        FlashbackUIManager.Instance.Teleport(other.gameObject, TeleportDestination);
        TimerManager.Instance.ShowTimer(false);
    }
}
