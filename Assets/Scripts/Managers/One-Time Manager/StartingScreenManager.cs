using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// I made this as a substitution because of the camera depth issue happening.
// But this should be trash now.
public class StartingScreenManager : MonoBehaviour
{
    [SerializeField]
    private FlashbackUIManager FM;

    [SerializeField]
    private Transform SpawnPoint;

    [SerializeField]
    private GameObject Player;

    [SerializeField]
    private GameObject StartingScreen;

    [SerializeField]
    private CursorManager CM;

    private void Awake()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Player.GetComponent<Hertzole.GoldPlayer.GoldPlayerController>().enabled = false;
    }

    public void OnStartButtonClick()
    {
        FM.Teleport(Player, SpawnPoint);
        Destroy(this);
        CM.enabled = true;
        StartingScreen.SetActive(false);
    }

    public void OnQuitButtonClick()
    {
        Application.Quit();
    }
}
