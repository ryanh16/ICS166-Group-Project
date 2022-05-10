using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject doors;

    public void StartCounting()
    {
        StartCoroutine(WaitFor20Min());
    }

    IEnumerator WaitFor20Min()
    {
        yield return new WaitForSeconds(1200);
        Destroy(doors);
        Destroy(this);
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.H))
        {
            StopAllCoroutines();
            Destroy(doors);
            Destroy(this);
        }
    }
}
