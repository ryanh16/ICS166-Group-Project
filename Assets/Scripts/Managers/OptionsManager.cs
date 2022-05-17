using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OptionsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject ButtonPrefabInGameObject;
    private static Button ButtonPrefab;

    [SerializeField]
    private GameObject Player;
    private static Hertzole.GoldPlayer.GoldPlayerController PlayerController;

    [SerializeField]
    private GameObject OptionsHolder;
    private static Transform OptionsParent;

    [SerializeField]
    [Tooltip("I set this to SerializeField just to make life eaiser!")]
    private static List<Button> ButtonList = new List<Button>();

    [SerializeField]
    private static float MarginBetweenButtons = 60;

    private static bool CurrentlyInBranch = false;

    private static OptionsManager Instance;

    private void Start()
    {
        ButtonPrefab = ButtonPrefabInGameObject.GetComponent<Button>();
        PlayerController = Player.GetComponent<Hertzole.GoldPlayer.GoldPlayerController>();
        OptionsParent = OptionsHolder.GetComponent<Transform>();
        Instance = this;
    }


    public static void CreateButton(Option option)
    {
        Button oneButton = (Button)Instantiate<Button>(ButtonPrefab, OptionsParent);
        oneButton.GetComponentInChildren<TextMeshProUGUI>().text = option.GetName();
        oneButton.onClick.AddListener(() => { option.OnClickOnThisOption(); });
        ButtonList.Add(oneButton);
    }


    // Call this *once* before creating any new buttons or when finished on this branch
    // This method will delete every button on the screen
    public static void ClearAllCurrentButtons()
    {
        foreach (Transform button in OptionsParent)
        {
            Destroy(button.gameObject);
        }
        ButtonList.Clear();
    }


    // This method should be called after setting everything up. Once this method 
    // called, player will not be able to move until EndOnThisBranch() is called
    public static void StartOnThisBranch()
    {
        // Should add code to arrange buttons properly
        // probably add it here

        // ==========START OF MATH==========
        int NumberofButtons = ButtonList.Count;
        
        if (NumberofButtons % 2 == 0)
        {
            // Even situation
            int HalfOfNumber = NumberofButtons / 2;
            for (int i = 0; i < NumberofButtons; i++)
            {
                if (i + 1 <= HalfOfNumber)
                {
                    Vector3 NewPosition = new Vector3(0, (HalfOfNumber - i) * MarginBetweenButtons, 0);
                    ButtonList[i].transform.localPosition = NewPosition;
                }

                else
                {
                    Vector3 NewPosition = new Vector3(0, -(i + 1 - HalfOfNumber) * MarginBetweenButtons, 0);
                    ButtonList[i].transform.localPosition = NewPosition;
                }
            }
        }

        else
        {
            // Odd situation
            int HalfOfNumber = NumberofButtons / 2; // grounding in math?
            float HalfOfNumberFloat =  NumberofButtons / (float) 2.0;
            for (int i = 0; i < NumberofButtons; i++)
            {
                if (i == HalfOfNumber)
                {
                    ButtonList[i].transform.localPosition = Vector3.zero;
                }

                else if (i < HalfOfNumber)
                {
                    Vector3 NewPosition = new Vector3(0, (HalfOfNumberFloat - (float) i) * MarginBetweenButtons, 0);
                    ButtonList[i].transform.localPosition = NewPosition;
                }

                else
                {
                    Vector3 NewPosition = new Vector3(0, -(i + 1 - HalfOfNumber) * MarginBetweenButtons, 0);
                    ButtonList[i].transform.localPosition = NewPosition;
                }
            }
        }

        // ==========END OF MATH==========

        CurrentlyInBranch = true;
        PlayerController.enabled = false;
    }


    // This method should be called after the branch is over. Once this method 
    // called, player will be able to start moving again
    public static void EndOnThisBranch()
    {
        Instance.StartCoroutine(Instance.SetToFalseAfterHalfSecond());
        PlayerController.enabled = true;
    }

    // This returns if player is still interacting with any checkpoint
    public static bool IsCurrentlyInBranch()
    {
        return CurrentlyInBranch;
    }

    // This returns if there are any buttons right now on the screen
    public static bool HasButtonsOnScreen()
    {
        return ButtonList.Count != 0;
    }

    // Set the CurrentlyInBranch to false after 0.5 seconds
    // this is prevent some edge-case when player clicks on Leave
    // option and the CheckPoint at the same time, the option will 
    // disappear real quick and re-appear on the screen real quick.
    IEnumerator SetToFalseAfterHalfSecond()
    {
        yield return new WaitForSeconds(0.5f);
        CurrentlyInBranch = false;
    }
}