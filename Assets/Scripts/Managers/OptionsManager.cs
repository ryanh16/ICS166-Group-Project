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
        branch.SetOptionsManager(this);
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
        foreach (Transform button in this.transform)
        {
            Destroy(button.gameObject);
        }
        ButtonList.Clear();
        Cursor.visible = false;
        Player.enabled = true;
    }

    private void LateUpdate()
    {
        if (ButtonList.Count != 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            Player.enabled = false;
        }
    }
}