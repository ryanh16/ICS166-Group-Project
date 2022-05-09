using System.Collections.Generic;

public sealed class SceneTypes
{
    public enum Scenes
    {
        NONE,
        GameplayArea,
        Sounds,
    }

    private static readonly Dictionary<Scenes, string> sceneNames = new Dictionary<Scenes, string>()
    {
        { Scenes.NONE, null },
        { Scenes.GameplayArea, "GameplayArea" },
        { Scenes.Sounds, "Sounds" },
    };



    public static string GetSceneName(Scenes scene)
    {
        return sceneNames[scene];
    }
}