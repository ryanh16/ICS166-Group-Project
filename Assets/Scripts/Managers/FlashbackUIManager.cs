using System.Collections;
using UnityEngine;

public class FlashbackUIManager : MonoBehaviour
{
    [SerializeField] private GameObject flashbackUI;
    private Animator flashbackUIPanel;
    private AnimationClip[] clips;

    public static FlashbackUIManager Instance;



    private void Awake()
    {
        Instance = this;

        flashbackUIPanel = flashbackUI.GetComponentInChildren<Animator>();
        clips = flashbackUIPanel.runtimeAnimatorController.animationClips;
    }


    private void OnDestroy()
    {
        Instance = null;
    }


    public void Teleport(GameObject playerObject, Transform targetLocation)
    {
        StartCoroutine(StartTeleport(playerObject, targetLocation));
    }


    private IEnumerator StartTeleport(GameObject playerObject, Transform targetLocation)
    {
        CharacterController playerController = playerObject.GetComponent<CharacterController>();

        flashbackUI.SetActive(true);

        flashbackUIPanel.Play("Fade_Out");
        yield return new WaitForSeconds(clips[0].length);

        playerController.enabled = false;
        playerObject.transform.position = new Vector3(targetLocation.position.x, targetLocation.position.y + 1, targetLocation.position.z);
        playerController.enabled = true;

        flashbackUIPanel.Play("Fade_In");
        yield return new WaitForSeconds(clips[0].length);

        flashbackUI.SetActive(false);
    }
}