using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugManager : MonoBehaviour
{
    [SerializeField]
    private GameObject Block735;

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))
        {
            TimerManager.Instance.SetTimer(7, 45);
            Block735.SetActive(false);
        }
    }
}
