using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This manager is a kind of one time manager, it checks if player collides
// with this certain collider; if yes, it will enable the QuittinggameFailManager
// which simply just allows player to advance the dialogue will be brought
// up after player lands.
public class EnableQuittingGameFailManager : MonoBehaviour
{
    [SerializeField]
    private QuittingGameFailManager QM;

    private void OnTriggerEnter(Collider other)
    {
        QM.enabled = true;
    }
}
