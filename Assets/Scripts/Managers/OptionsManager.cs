using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private static float MarginBetweenButtons = 10;

    private static bool CurrentlyInBranch = false;


    private void Start()
    {
        ButtonPrefab = ButtonPrefabInGameObject.GetComponent<Button>();
        PlayerController = Player.GetComponent<Hertzole.GoldPlayer.GoldPlayerController>();
        OptionsParent = OptionsHolder.GetComponent<Transform>();
    }

    public static void CreateButton(Option option)
    {
        Button oneButton = (Button)Instantiate<Button>(ButtonPrefab);
        oneButton.GetComponentInChildren<Text>().text = option.GetName();
        // oneButton.transform.position = OptionsParent.transform.position;
        oneButton.transform.SetParent(OptionsParent);
        oneButton.onClick.AddListener(() => { option.OnClickOnThisOption(); });
        ButtonList.Add(oneButton);
    }

    // Call this *once* before creating any new buttons or when finished on this branch
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
        int NumberofButtons = ButtonList.Count;
        
        if (NumberofButtons%2 == 0)
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
            int HalfOfNumber = NumberofButtons / 2; // grounding?
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

        CurrentlyInBranch = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        PlayerController.enabled = false;
    }

    // This method should be called after the branch is over. Once this method 
    // called, player will be able to start moving again
    public static void EndOnThisBranch()
    {
        CurrentlyInBranch = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        PlayerController.enabled = true;
    }

    private void LateUpdate()
    {
        if (CurrentlyInBranch)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            PlayerController.enabled = false;
        }
    }
}