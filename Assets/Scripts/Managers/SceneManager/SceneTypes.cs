using System.Collections.Generic;

public sealed class SceneTypes
{
    public enum Scenes
    {
        NONE,
        Bedroom,
        TEST_SCENE
    }

    private static readonly Dictionary<Scenes, string> sceneNames = new Dictionary<Scenes, string>()
    {
        { Scenes.NONE, null },
        { Scenes.Bedroom, "Bedroom" },
        { Scenes.TEST_SCENE, "TEST SCENE" }
    };



    public static string GetSceneName(Scenes scene)
    {
        return sceneNames[scene];
    }
}