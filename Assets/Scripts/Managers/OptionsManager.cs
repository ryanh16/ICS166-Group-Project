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

    public void CreateButton(Branch branch)
    {
        ClearAllButtons();
        Button oneButton = (Button)Instantiate<Button>(ButtonPrefab);
        oneButton.GetComponentInChildren<Text>().text = branch.GetName();
        oneButton.onClick.AddListener(() => { branch.OnClickOnThisBranch(); });
        ButtonList.Add(oneButton);
        oneButton.transform.parent = this.gameObject.transform;
        Debug.Log($"added new item to buttonlist, now it has {ButtonList.Count} items");
    }

    public void FinishSettingUpButtons()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Player.enabled = false;
    }

    public void ClearAllButtons()
    {
        /*Debug.Log($"clearing button list, before it has {ButtonList.Count} items");
        foreach (Button b in ButtonList)
        {
            if (b == null)
            {
                Debug.Log("b is null");
            }
            ButtonList.Remove(b);
            Destroy(b);
        }*/
        Debug.Log("clearing all buttons");
        Cursor.visible = false;
        Player.enabled = true;
    }
}