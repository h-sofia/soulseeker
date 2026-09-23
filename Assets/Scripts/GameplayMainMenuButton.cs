using UnityEngine;
using UnityEngine.SceneManagement;

public class GameplayMainMenuButton : MonoBehaviour
{
    private static readonly string[] GameplayScenes =
    {
        "Scene1",
        "scene2",
        "Scene3"
    };

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    public static void CreateForGameplayScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        if (!IsGameplayScene(scene.name))
        {
            return;
        }

        if (FindFirstObjectByType<GameplayMainMenuButton>() != null)
        {
            return;
        }

        GameObject buttonObject = new GameObject("GameplayMainMenuButton");
        buttonObject.AddComponent<GameplayMainMenuButton>();
    }

    private static bool IsGameplayScene(string sceneName)
    {
        foreach (string gameplayScene in GameplayScenes)
        {
            if (sceneName == gameplayScene)
            {
                return true;
            }
        }

        return false;
    }

    private void OnGUI()
    {
        const float width = 220f;
        const float height = 56f;
        Rect buttonRect = new Rect(24f, 24f, width, height);

        GUIStyle buttonStyle = new GUIStyle(GUI.skin.button)
        {
            fontSize = 24,
            alignment = TextAnchor.MiddleCenter
        };

        if (GUI.Button(buttonRect, "Main Menu", buttonStyle))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}
