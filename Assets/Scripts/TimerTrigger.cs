using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TimerManager.Instance.ShowTimer(true);
            TimerManager.Instance.ActivateTimer(true);

            door.SetActive(true);
            door.GetComponent<Outline>().SetColorToInvisible();

            door.GetComponent<DoorManager>().SubscribeTo745Time();
        }
    }
}
