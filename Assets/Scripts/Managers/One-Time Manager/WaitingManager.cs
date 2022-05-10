using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is an one time manager that will be enabled after player chooses 
// to wait for 20 mins outside the transportation center.
// This manager also allows player to skip this 20 mins if they find
// the secret behind the transportation center.
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
