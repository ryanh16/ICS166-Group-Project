using UnityEngine;


// This class deals with the users' input
// Right now there are two ways for users to advance the dialogue:
// 1. press the space bar key; 2. click on the object again

public class InputManager : MonoBehaviour
{
    [SerializeField] private FlashbackUIManager FM;
    private GameObject currentObject;

    public static InputManager Instance;



    private void Awake()
    {
        Instance = this;
    }


    private void OnDestroy()
    {
        Instance = null;
    }


    // Update is called once per frame
    private void Update()
    {
        currentObject = ObjectLookingAt.GetCurrentObject();

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (DialogueManager.IsInDialogue())
            {
                DialogueManager.OnContinueButtonClick();
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (DialogueManager.IsInDialogue())
            {
                DialogueManager.OnContinueButtonClick();
            }

            else if (currentObject && !FM.IsDuringTeleport() && !OptionsManager.HasButtonsOnScreen())
            {
                Interactable curInteractable = currentObject.GetComponent<Interactable>();
                CheckPoint curCheckPoint = currentObject.GetComponent<CheckPoint>();

                if (curCheckPoint)
                {
                    if (!DialogueManager.IsInDialogue() && !OptionsManager.IsCurrentlyInBranch() && curCheckPoint.CanManuallyActivate())
                    {
                        curCheckPoint.ManuallyActivateCheckPoint();
                        return;
                    }
                }

                if (curInteractable)
                {
                    if (!DialogueManager.IsInDialogue() && !OptionsManager.IsCurrentlyInBranch())
                    {
                        curInteractable.OnInteractingWith();
                        return;
                    }
                }
            }
        }

        // Return/Enter key exits dialogue early
        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (DialogueManager.IsInDialogue())
            {
                DialogueManager.EndDialogue();
            }
            OptionsManager.ClearAllCurrentButtons();
            OptionsManager.EndOnThisBranch();
        }
    }
}