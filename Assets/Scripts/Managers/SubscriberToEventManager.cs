using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SubscriberToEventManager : MonoBehaviour
{
    [SerializeField]
    [Tooltip("This is the Event when you want to this game object to be active in the scene.")]
    private List<EventTypes.Events> ActiveInTheseEvents;

    [SerializeField]
    [Tooltip("This is the Event when you want to this game object to be inactive in the scene.")]
    private List<EventTypes.Events> InactiveInTheseEvents;

    [SerializeField]
    [Tooltip("This indicates if this GameObject can advance the event after player finishes interacting with it.")]
    private bool CanAdvanceEvent;

    private void Start()
    {
        foreach (EventTypes.Events e in ActiveInTheseEvents)
        {
            EventManager.Instance.Subscribe(e, SetSelfActive);
        }

        foreach (EventTypes.Events e in InactiveInTheseEvents)
        {
            EventManager.Instance.Subscribe(e, SetSelfInactive);
        }
    }

    private void SetSelfActive()
    {
        gameObject.SetActive(true);
    }

    private void SetSelfInactive()
    {
        gameObject.SetActive(false);
    }

    public bool CanAdvanceEventOrNot()
    {
        return CanAdvanceEvent;
    }

    private void OnDestroy()
    {
        foreach (EventTypes.Events e in ActiveInTheseEvents)
        {
            EventManager.Instance.Unsubscribe(e, SetSelfActive);
        }

        foreach (EventTypes.Events e in InactiveInTheseEvents)
        {
            EventManager.Instance.Unsubscribe(e, SetSelfInactive);
        }
    }
}
