using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsManager : MonoBehaviour
{
    [SerializeField]
    private Button ButtonPrefab;

    [SerializeField]
    private List<Button> ButtonList = new List<Button>();

    [SerializeField]
    private Hertzole.GoldPlayer.GoldPlayerController Player;

    public void CreateButtons(Branch[] branches)
    {
        ClearAllButtons();
        for (int i = 0; i < branches.Length; i++)
        {
            Button oneButton = (Button)Instantiate<Button>(ButtonPrefab);
            oneButton.GetComponent<Text>().text = branches[i].GetName();
            oneButton.onClick.AddListener(branches[i].OnClickOnThisBranch);
            ButtonList.Add(oneButton);
            oneButton.transform.parent = this.gameObject.transform;
        }
        Cursor.visible = true;
        Player.enabled = false;
    }

    public void ClearAllButtons()
    {
        foreach (Button b in ButtonList)
        {
            ButtonList.Remove(b);
            Destroy(b);
        }
        Cursor.visible = false;
        Player.enabled = true;
    }
}
