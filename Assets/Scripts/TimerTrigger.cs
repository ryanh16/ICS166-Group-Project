using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject door;
    [SerializeField]
    private GameObject CrossWalk1;
    [SerializeField]
    private GameObject CrossWalk2;
    [SerializeField]
    private GameObject Bus;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            TimerManager.Instance.ShowTimer(true);
            TimerManager.Instance.ActivateTimer(true);

            door.SetActive(true);
            Bus.SetActive(false);
            CrossWalk1.SetActive(true);
            CrossWalk2.SetActive(true);
            door.GetComponent<Outline>().SetColorToInvisible();

            door.GetComponent<DoorManager>().SubscribeTo745Time();
        }
    }
}
