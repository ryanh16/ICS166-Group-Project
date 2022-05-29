using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BusPlaceLockedManagerr : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        TimerManager.Instance.SubscribeTo745(DeactivateOn745);
    }

    private void DeactivateOn745()
    {
        TimerManager.Instance.UnsubscribeFrom745(DeactivateOn745);
        this.gameObject.SetActive(false);
    }
}