using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testcaller : MonoBehaviour
{
    public GameObject go; 

    // Start is called before the first frame update
    void Start()
    {
        go.GetComponent<TestBecalled>().TestBeCalled();
    }


}
