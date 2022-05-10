using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
