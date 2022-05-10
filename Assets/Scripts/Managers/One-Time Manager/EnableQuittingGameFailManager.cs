using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableQuittingGameFailManager : MonoBehaviour
{
    [SerializeField]
    private QuittingGameFailManager QM;

    private void OnTriggerEnter(Collider other)
    {
        QM.enabled = true;
    }
}
