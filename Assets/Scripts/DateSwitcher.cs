using UnityEngine;

public class DateSwitcher : MonoBehaviour
{
    private void Start()
    {
        EventManager.Instance.Subscribe(EventTypes.Events.BusPassInteractedWith, FlashbackDate);
        EventManager.Instance.Subscribe(EventTypes.Events.Ending, PresentDate);
    }


    private void OnDestroy()
    {
        EventManager.Instance.Unsubscribe(EventTypes.Events.BusPassInteractedWith, FlashbackDate);
        EventManager.Instance.Unsubscribe(EventTypes.Events.Ending, PresentDate);
    }


    private void FlashbackDate()
    {
        DateUI.Instance.UpdateDate("MAY 2022");
    }


    private void PresentDate()
    {
        DateUI.Instance.UpdateDate("JUNE 2025");
    }
}