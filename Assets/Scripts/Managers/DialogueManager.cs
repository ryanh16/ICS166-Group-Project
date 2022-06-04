using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

// This class is a simple Singleton class that handles the dialogue box that will 
// appear most of the time. Right now the animation for the dialogue box to appear 
// and disappear is to flash into and out of the screen. If you want to the change 
// the animation of it, remember to check and change the "animator" related statements.

public class DialogueManager : MonoBehaviour
{
    // Variables storing references to dialogue box objects
    [SerializeField]
    private GameObject dialogueBox;
    private static Text nameText;
    private static Text dialogueText;
    private static Animator animator;

    // Variables related to dialogue text management
    private static string currentSentence = "";
    private static Queue<string> dialogues = new Queue<string>();
    private static bool isInDia = false;
    private Dialogue curDialogue;

    // Variables related to Player
    [SerializeField]
    private GameObject playerObject;
    [SerializeField]
    private AudioSource playerDialogueAudio;
    private float originalDialogueAudioPitch;
    [SerializeField, Range(0.2f, 1)]
    private float dialoguePitchRange = 0.2f;
    [SerializeField, Range(0.02f, 0.1f)]
    private float dialogueLetterDelay = 0.02f;
    private static Hertzole.GoldPlayer.GoldPlayerController goldController;

    private static DialogueManager Instance;

    private static Action OnDialogueEndsAction;



    private void Start()
    {
        Instance = this; // this instance variable is used to call coroutine related methods
        animator = dialogueBox.GetComponent<Animator>();
        nameText = dialogueBox.transform.GetChild(0).GetComponent<Text>();
        dialogueText = dialogueBox.transform.GetChild(1).GetComponent<Text>();
        goldController = playerObject.GetComponent<Hertzole.GoldPlayer.GoldPlayerController>();
        originalDialogueAudioPitch = playerDialogueAudio.pitch;
    }


    private void OnDestroy()
    {
        Instance = null;
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
        Instance.StopAllCoroutines();
        if (dialogueText.text == ParseSentence(currentSentence).Value)
        {
            NextDialogue();
        }

        else
        {
            dialogueText.text = ParseSentence(currentSentence).Value;
        }
    }


    // This method allows to set the dialogues, every element in the "sentences"
    // parameter will occupy a dialogue box, the parameter "name" is just the 
    // name of the thing/person that is speaking, and it has a default value of "me"
    public static void SetDialogues(Dialogue incomingDialogue)
    {
        dialogues = new Queue<string>(incomingDialogue.GetDialogue());
        Instance.curDialogue = incomingDialogue;
    }


    // When starting the dialogue, the dialogue box will first flash in the screen
    // (if the dialogue box is already in the screen, it will just stay), and then
    // display the name of speaker and the first sentence in the dialogue box.
    public static void StartDialogue()
    {
        // a fail-safe
        if (dialogues.Count == 0)
        {
            EndDialogue();
            return;
        }

        goldController.enabled = false;
        isInDia = true;
        animator.SetBool("IsOnScreen", true);

        currentSentence = dialogues.Dequeue();
        Instance.StartCoroutine(TypeOutDialogue(currentSentence));
    }


    // This method will advance the dialogue to the next sentence 
    // and update the currentSentence variable 
    public static void NextDialogue()
    {
        if (dialogues.Count == 0)
        {
            EndDialogue();
            return;
        }

        isInDia = true;

        Instance.StopAllCoroutines();
        currentSentence = dialogues.Dequeue();
        Instance.StartCoroutine(TypeOutDialogue(currentSentence));
    }


    // When the dialogue is finished, the dialogue box will flash out of the screen
    public static void EndDialogue()
    {
        isInDia = false;
        goldController.enabled = true;
        animator.SetBool("IsOnScreen", false);
        Instance.StopCoroutine(TypeOutDialogue(currentSentence));

        OnDialogueEndsAction?.Invoke();
    }


    // This method will type out the dialogue text one letter by one letter
    // at a rate of 1 letter per frame.
    public static IEnumerator TypeOutDialogue(string sentence)
    {
        KeyValuePair<string, string> splitSentence = ParseSentence(sentence);
        nameText.text = splitSentence.Key;
        
        dialogueText.text = "";

        foreach (char letter in splitSentence.Value.ToCharArray())
        {
            dialogueText.text += letter;

            Instance.playerDialogueAudio.pitch = UnityEngine.Random.Range(Instance.originalDialogueAudioPitch - Instance.dialoguePitchRange, Instance.originalDialogueAudioPitch + Instance.dialoguePitchRange);
            Instance.playerDialogueAudio.Play();

            yield return new WaitForSeconds(Instance.dialogueLetterDelay);
        }
    }


    public static bool IsInDialogue()
    {
        return isInDia;
    }


    // Splits a sentence into its speaker and content
    private static KeyValuePair<string, string> ParseSentence(string curSentence)
    {
        // Parse current sentence to separate speaker index from content
        string[] splitSentence = curSentence.Split('\\');

        if (string.IsNullOrEmpty(splitSentence[0]))
        {
            Debug.LogError($"No speaker assigned for message: {splitSentence[1]}");
        }

        return new KeyValuePair<string, string>(Instance.curDialogue.GetSpeaker(int.Parse(splitSentence[0])), splitSentence[1]);
    }


    public static void SubscribeToDialogueEnds(Action action)
    {
        OnDialogueEndsAction += action;
    }


    public static void UnsubscribeFromDialogueEnds(Action action)
    {
        OnDialogueEndsAction -= action;
    }
}