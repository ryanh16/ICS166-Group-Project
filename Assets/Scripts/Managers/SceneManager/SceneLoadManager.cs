using UnityEngine.SceneManagement;

public sealed class SceneLoadManager
{
    public static void LoadScene(SceneTypes.Scenes scene)
    {
        SceneManager.LoadScene(SceneTypes.GetSceneName(scene));
    }
}