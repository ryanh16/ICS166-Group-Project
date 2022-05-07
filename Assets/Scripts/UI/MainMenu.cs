using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void onStartButtonClick()
    {
        SceneLoadManager.LoadScene(SceneTypes.Scenes.GameplayArea);
    }


    public void onQuitButtonClick()
    {
        Application.Quit();
    }
}