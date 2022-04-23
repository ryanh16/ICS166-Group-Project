using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// This class is a simple Singleton class that handles the dialogue box that will 
// appear most of the time. Right now the animation for the dialogue box to appear 
// and disappear is to flash into and out of the screen. If you want to the change 
// the animation of it, remember to check and change the "animator" related statements.

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private GameObject dialogueBox;

    private static Text nameText;
    private static Text dialogueText;
    private static Animator animator;

    private static string whoIsSpeaking = "me";
    private static string currentSentence = "";
    private static Queue<string> dialogues = new Queue<string>();

    private static DialogueManager instance;

    private static bool isInDia = false;

    [SerializeField]
    private GameObject controll;
    private static Hertzole.GoldPlayer.GoldPlayerController goldController;


    private void Start()
    {
        instance = this; // this instance variable is used to call coroutine related methods
        animator = dialogueBox.GetComponent<Animator>();
        nameText = dialogueBox.transform.GetChild(1).GetComponent<Text>();
        dialogueText = dialogueBox.transform.GetChild(2).GetComponent<Text>();
        goldController = controll.GetComponent<Hertzole.GoldPlayer.GoldPlayerController>();
    }

    // This method is called when user click on the "Continue" button in the dialogue box
    // 1. if the currentSentence has been typed out completely, this method will 
    //    advance the dialogue to the next sentence
    // 2. if the currentSentence has not been typed out completely, this method will
    //    immediately "type" out the entire sentence (this is achieved by checking 
    //    whether the text in the dialogue box is the same as the currentSentence
    //    that is supposed to be typed out, currentSentence variable is updated
    //    everytime when the dialogue is advanced)
    public static void OnContinueButtonClick()
    {
        // stop any ongoing typing out coroutines first 
        instance.StopAllCoroutines();
        if (dialogueText.text == currentSentence)
        {
            nextDialogue();
        }
        else
        {
            dialogueText.text = currentSentence;
        }
    }

    // This method allows to set the dialogues, every element in the "sentences"
    // parameter will occupy a dialogue box, the parameter "name" is just the 
    // name of the thing/person that is speaking, and it has a default value of "me"
    public static void setDialogues(string[] sentences, string name = "me")
    {
        dialogues.Clear();
        whoIsSpeaking = name;
        foreach (string sentence in sentences)
        {
            dialogues.Enqueue(sentence);
        }
    }

    // When starting the dialogue, the dialogue box will first flash in the screen
    // (if the dialogue box is already in the screen, it will just stay), and then
    // display the name of speaker and the first sentence in the dialogue box.
    public static void startDialogue()
    {
        // a fail-safe
        if (dialogues.Count == 0)
        {
            endDialogue();
            return;
        }

        goldController.enabled = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        isInDia = true;

        animator.SetBool("IsOnScreen", true);

        nameText.text = whoIsSpeaking;
        currentSentence = dialogues.Dequeue();
        instance.StartCoroutine(typeOutDialogue(currentSentence));
    }

    // This method will advance the dialogue to the next sentence 
    // and update the currentSentence variable 
    public static void nextDialogue()
    {
        if (dialogues.Count == 0)
        {
            endDialogue();
            return;
        }

        isInDia = true;

        instance.StopAllCoroutines();
        currentSentence = dialogues.Dequeue();
        instance.StartCoroutine(typeOutDialogue(currentSentence));
    }

    // When the dialogue is finished, the dialogue box will flash out of the screen
    public static void endDialogue()
    {
        isInDia = false;
        goldController.enabled = true;
        animator.SetBool("IsOnScreen", false) ;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // This method will type out the dialogue text one letter by one letter
    // at a rate of 1 letter per frame.
    public static IEnumerator typeOutDialogue(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public static bool isInDialogue()
    {
        return isInDia;
    }
}
