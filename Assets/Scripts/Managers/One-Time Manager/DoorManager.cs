using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public void On745Time()
    {
        TimerManager.Instance.UnsubscribeFrom745(On745Time);
        Destroy(gameObject);
    }

    public void SubscribeTo745Time()
    {
        TimerManager.Instance.SubscribeTo745(On745Time);
    }
}
