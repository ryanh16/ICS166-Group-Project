using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestBecalled : MonoBehaviour
{
    public void TestBeCalled()
    {
        Debug.Log(" this is called");
        gameObject.SetActive(true);
    }
}
