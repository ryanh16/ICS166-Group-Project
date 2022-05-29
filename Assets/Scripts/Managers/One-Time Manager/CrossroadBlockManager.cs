using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrossroadBlockManager : MonoBehaviour
{
    [SerializeField]
    private bool ActivateOn735;

    [SerializeField]
    private bool ActivateOn755;

    // Start is called before the first frame update
    private void Start()
    {
        if (ActivateOn735)
        {
            TimerManager.Instance.SubscribeTo735(On735);
            return;
        }

        if (ActivateOn755)
        {
            TimerManager.Instance.SubscribeTo755(On755);
            return;
        }
    }

    private void OnDestroy()
    {
        TimerManager.Instance.UnsubscribeFrom735(On735);
        TimerManager.Instance.UnsubscribeFrom755(On755);
    }

    private void On735()
    {
        this.gameObject.SetActive(false);
    }

    private void On755()
    {
        this.gameObject.SetActive(false);
    }
}
