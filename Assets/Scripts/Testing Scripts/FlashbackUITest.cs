using System.Collections;
using UnityEngine;

public class FlashbackUITest : MonoBehaviour
{
    [SerializeField] private GameObject flashbackUI;
    private Animator flashbackUIPanel;
    private AnimationClip[] clips;
    private bool waiting = false;



    private void Awake()
    {
        flashbackUIPanel = flashbackUI.GetComponentInChildren<Animator>();
        clips = flashbackUIPanel.runtimeAnimatorController.animationClips;
    }


    private void Update()
    {
        if (!waiting)
        {
            StartCoroutine(LoopAnimation());
        }
    }


    // Plays animation, waits for animation to conclude before starting animation again
    private IEnumerator LoopAnimation()
    {
        Debug.Log("Animation Start");
        waiting = true;
        flashbackUI.SetActive(true);

        flashbackUIPanel.Play("FlashbackUIController");
        yield return new WaitForSeconds(clips[0].length);

        flashbackUI.SetActive(false);
        waiting = false;
        Debug.Log("Animation End");
    }
}