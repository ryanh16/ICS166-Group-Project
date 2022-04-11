using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    [SerializeField]
    private Text nameText;
    [SerializeField]
    private Text dialogueText;

    public Animator animator;

    private string whoIsSpeaking = "me";
    private Queue<string> dialogues = new Queue<string>();

    public void setDialogues(string[] sentences, string name = "me")
    {
        dialogues.Clear();
        whoIsSpeaking = name;
        foreach (string sentence in sentences)
        {
            dialogues.Enqueue(sentence);
        }
    }

    public void startDialogue()
    {
        animator.SetBool("IsOnScreen", true);

        if (dialogues.Count == 0)
        {
            endDialogue();
            return;
        }

        nameText.text = whoIsSpeaking;
        StartCoroutine(typeOutDialogue(dialogues.Dequeue()));
    }

    public void nextDialogue()
    {
        if (dialogues.Count == 0)
        {
            endDialogue();
            return;
        }

        StopAllCoroutines();
        StartCoroutine(typeOutDialogue(dialogues.Dequeue()));
    }

    public void endDialogue()
    {
        animator.SetBool("IsOnScreen", false) ;
        Debug.Log("The conversation is over");
    }

    IEnumerator typeOutDialogue(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }
}
