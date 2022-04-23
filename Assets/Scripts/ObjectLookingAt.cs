using UnityEngine;

// This is just a simple Singleton class that stores the object that the player
// is looking at (i.e. aiming at with the aiming point in the center of the screen)

public class ObjectLookingAt : MonoBehaviour
{
    private static GameObject currentObject;



    public static void SetCurrentObject(GameObject go)
    {
        currentObject = go;
    }


    public static void RemoveCurrentObject()
    {
        currentObject = null;
    }


    public static GameObject GetCurrentObject()
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
                currentObject.GetComponent<Interactable>().StartDialogue();
            }

            else
            {
                if (!DialogueManager.isInDialogue())
                    DialogueManager.endDialogue();
            }
        }
    }
}