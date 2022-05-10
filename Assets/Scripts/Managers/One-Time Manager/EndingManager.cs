using UnityEngine;

// This is an one time manager that deals with the ending.
// When the player reaches the ending, this Manager will be 
// enabled, and thus allow player to advance the dialogue.
// The ending includes two parts:
// 1. Teleport the player to the room area, this is done in Ending()
//    or OnDialogueEnds() if we supply any dialogues;
// 2. Show up a thanks for playing dialogue and enable players to move.
//    This is done in OnTeleportEnds().
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
