using System.Collections;
using UnityEngine;
using System;

public class FlashbackUIManager : MonoBehaviour
{
    [SerializeField] private GameObject flashbackUI;
    private Animator flashbackUIPanel;
    private AnimationClip[] clips;

    public static FlashbackUIManager Instance;

    private Action OnTeleportEndsAction;

    private bool isDuringTeleport = false;

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

    public bool IsDuringTeleport()
    {
        return isDuringTeleport;
    }

    public void Teleport(GameObject playerObject, Transform targetLocation)
    {
        isDuringTeleport = true;
        StartCoroutine(StartTeleport(playerObject, targetLocation));
    }


    private IEnumerator StartTeleport(GameObject playerObject, Transform targetLocation)
    {
        CharacterController playerController = playerObject.GetComponent<CharacterController>();
        Hertzole.GoldPlayer.GoldPlayerController goldPlayerController = playerObject.GetComponent<Hertzole.GoldPlayer.GoldPlayerController>();

        flashbackUI.SetActive(true);
        goldPlayerController.enabled = false;

        flashbackUIPanel.Play("Fade_Out");
        yield return new WaitForSeconds(clips[0].length);

        playerController.enabled = false;
        playerObject.transform.position = new Vector3(targetLocation.position.x, targetLocation.position.y + 1, targetLocation.position.z);
        playerObject.transform.rotation = targetLocation.rotation;
        playerController.enabled = true;
        goldPlayerController.enabled = true;

        flashbackUIPanel.Play("Fade_In");
        yield return new WaitForSeconds(clips[0].length);

        flashbackUI.SetActive(false);
        OnTeleportEndsAction?.Invoke();
        isDuringTeleport = false;
    }

    public void SubscribeToTeleportEnds(Action action)
    {
        OnTeleportEndsAction += action;
    }

    public void DesubscribeFromTeleportEnds(Action action)
    {
        OnTeleportEndsAction -= action;
    }

}