using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void onStartButtonClick()
    {
        SceneLoadManager.LoadScene(SceneTypes.Scenes.Bedroom);
    }


    public void onQuitButtonClick()
    {
        Application.Quit();
    }
}
