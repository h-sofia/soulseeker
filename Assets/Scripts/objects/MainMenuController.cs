using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    private UIDocument document;
    private Button startButton;

    private void Start()
    {
        document = GetComponent<UIDocument>();

        if (document == null)
        {
            Debug.LogError("UIDocument is missing from this GameObject.");
            return;
        }

        VisualElement root = document.rootVisualElement;
        startButton = root.Q<Button>("start-button");

        if (startButton == null)
        {
            Debug.LogError("Could not find a button named start-button.");
            return;
        }

        startButton.RegisterCallback<ClickEvent>(OnStartButtonClicked);

        Debug.Log("Start Game button connected.");
    }

    private void OnDestroy()
    {
        if (startButton != null)
        {
            startButton.UnregisterCallback<ClickEvent>(OnStartButtonClicked);
        }
    }

    private void OnStartButtonClicked(ClickEvent evt)
    {
        Debug.Log("START GAME CLICKED");
        StartGame();
    }

    private void StartGame()
    {
        if (!Application.CanStreamedLevelBeLoaded("Scene1"))
        {
            Debug.LogError(
                "Scene1 is not in the Build Settings/Build Profiles scene list."
            );
            return;
        }

        Debug.Log("Loading Scene1...");
        SceneManager.LoadScene("Scene1");
    }
}