using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

// This is an one-time manager that allows player to press
// Q/E key to rotate the object to examine the object
// Successful condition set in the Update() method is to 
// rotate the obejct 90 degrees now.
public class ExamineManager : MonoBehaviour
{
    private Action OnExaminEnds;

    [SerializeField]
    private GameObject ObjectToExamine;

    [SerializeField]
    private float RotationSpeed = 100f;

    [SerializeField]
    private GameObject Player;

    public void StartExamining(GameObject go)
    {
        ObjectToExamine = go;
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.Q))
        {
            ObjectToExamine.transform.Rotate(new Vector3(0, 0, 1) * RotationSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.E))
        {
            ObjectToExamine.transform.Rotate(new Vector3(0, 0, -1) * RotationSpeed * Time.deltaTime);
        }

        // Successfully examine condition
        float angleBetween = Vector3.SignedAngle(ObjectToExamine.transform.right.normalized, Player.transform.forward.normalized, Vector3.up);

        if (angleBetween < 90f && angleBetween > 80f)
        {
            OnExaminEnds?.Invoke();
            this.enabled = false;
        }
    }

    public void SubscribeToOnExamineEnds(Action action)
    {
        OnExaminEnds += action;
    }

    public void UnsubscribeFromOnExamineEnds(Action action)
    {
        OnExaminEnds -= action;
    }
}
