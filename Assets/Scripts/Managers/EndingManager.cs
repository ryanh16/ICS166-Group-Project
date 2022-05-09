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
        if (Input.GetKeyUp(KeyCode.Space))
        {
            DialogueManager.OnContinueButtonClick();
        }
    }

    public void Ending()
    {
        Player.transform.position = SpawnPoint.position;
        Player.transform.rotation = SpawnPoint.rotation;
        BackGround.SetActive(true);
        Player.GetComponent<Hertzole.GoldPlayer.GoldPlayerController>().enabled = false;

        DialogueManager.SetDialogues(Dia);
        DialogueManager.StartDialogue();
        DialogueManager.SubscribeToDialogueEnds(OnDialogueEnds);

    }

    public void OnDialogueEnds()
    {
        DialogueManager.DesubscribeFromDialogueEnds(OnDialogueEnds);
        BackGround.SetActive(false);
        Player.GetComponent<Hertzole.GoldPlayer.GoldPlayerController>().enabled = true;
        this.gameObject.SetActive(false);
    }
}
