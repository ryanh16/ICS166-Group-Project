using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public void onStartButtonClick()
    {
        SceneLoadManager.LoadScene(SceneTypes.Scenes.Sounds);
    }


    public void onQuitButtonClick()
    {
        Application.Quit();
    }
}