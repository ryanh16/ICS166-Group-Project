using UnityEngine;

public class EndingManager : MonoBehaviour
{
    [SerializeField]
    private GameObject BackGround;
    [SerializeField]
    private Dialogue Dia;
    [SerializeField]
    private GameObject Player;
    [SerializeField]
    private Transform SpawnPoint;

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space) || Input.GetMouseButtonUp(0))
        {
            DialogueManager.OnContinueButtonClick();
        }
    }

    public void Ending()
    {
        Player.GetComponent<Hertzole.GoldPlayer.GoldPlayerController>().enabled = false;
        FlashbackUIManager FM = GameObject.Find("FlashbackUIManager").GetComponent<FlashbackUIManager>();
        FM.SubscribeToTeleportEnds(OnTeleportEnds);
        FM.Teleport(Player, SpawnPoint);
    }

    public void OnDialogueEnds()
    {
        DialogueManager.UnsubscribeFromDialogueEnds(OnDialogueEnds);
        BackGround.SetActive(false);
        Player.GetComponent<Hertzole.GoldPlayer.GoldPlayerController>().enabled = true;
        this.gameObject.SetActive(false);
    }

    public void OnTeleportEnds()
    {
        FlashbackUIManager FM = GameObject.Find("FlashbackUIManager").GetComponent<FlashbackUIManager>();
        FM.DesubscribeFromTeleportEnds(OnTeleportEnds);
        Player.transform.rotation = SpawnPoint.rotation;
        BackGround.SetActive(true);


        DialogueManager.SetDialogues(Dia);
        DialogueManager.StartDialogue();
        DialogueManager.SubscribeToDialogueEnds(OnDialogueEnds);
    }
}
