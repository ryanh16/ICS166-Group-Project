using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This is just a simple Singleton class that stores the object that the player
// is looking at (i.e. aiming at with the aiming point in the center of the screen)

public class ObjectLookingAt : MonoBehaviour
{
    private static GameObject currentObject;

    public static void setCurrentObject(GameObject go)
    {
        currentObject = go;
    }

    public static void removeCurrentObject()
    {
        currentObject = null;
    }

    public static GameObject getCurrentObject()
    {
        return currentObject;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            // probably another place to start the dialogue
            if (currentObject)
            {
                string[] d = { "this is just a black cube", "I put it here for testing", "but this is a way to substitue fungus" };
                DialogueManager.setDialogues(d);
                DialogueManager.startDialogue();
            }
            else
            {
                DialogueManager.endDialogue();
            }
        }
    }
}
