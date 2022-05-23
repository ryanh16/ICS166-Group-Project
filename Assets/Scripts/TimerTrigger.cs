using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TimerManager.Instance.ShowTimer(true);
            TimerManager.Instance.ActivateTimer(true);
        }
    }
}
