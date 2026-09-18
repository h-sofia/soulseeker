using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenuController : MonoBehaviour
{
    private UIDocument document;
    private Button startButton;
    private Button level1Button;
    private Button level2Button;

    private VisualElement mainMenuPanel;
    private VisualElement levelSelectPanel;

    private void Start()
    {
        document = GetComponent<UIDocument>();

        if (document == null)
        {
            Debug.LogError("UIDocument is missing from this GameObject.");
            return;
        }

        VisualElement root = document.rootVisualElement;

        mainMenuPanel = root.Q<VisualElement>("main-menu-panel");
        levelSelectPanel = root.Q<VisualElement>("level-select-panel");

        if (mainMenuPanel == null)
        {
            Debug.LogError("Could not find main-menu-panel.");
            return;
        }

        if (levelSelectPanel == null)
        {
            Debug.LogError("Could not find level-select-panel.");
            return;
        }

        levelSelectPanel.style.display = DisplayStyle.None;
        mainMenuPanel.style.display = DisplayStyle.Flex;

        Button levelSelectButton = root.Q<Button>("level-select-button");

        if (levelSelectButton != null)
        {
            levelSelectButton.clicked += OpenLevelSelect;
        }
        else
        {
            Debug.LogError("Could not find level-select-button.");
        }

        Button backButton = root.Q<Button>("level-select-back-button");

        if (backButton != null)
        {
            backButton.clicked += CloseLevelSelect;
        }
        else
        {
            Debug.LogWarning("Could not find level-select-back-button.");
        }

        startButton = root.Q<Button>("start-button");

        if (startButton != null)
        {
            startButton.clicked += StartGame;
            Debug.Log("Start Game button connected.");
        }
        else
        {
            Debug.LogError("Could not find start-button.");
        }

        level1Button = root.Q<Button>("level-1-button");

        if (level1Button != null)
        {
            level1Button.clicked += LoadLevel1;
        }
        else
        {
            Debug.LogError("Could not find level-1-button.");
        }

        level2Button = root.Q<Button>("level-2-button");

        if (level2Button != null)
        {
            level2Button.clicked += LoadLevel2;
        }
        else
        {
            Debug.LogError("Could not find level-2-button.");
        }
    }

    private void OnDestroy()
    {
        if (startButton != null)
        {
            startButton.clicked -= StartGame;
        }

        if (level1Button != null)
        {
            level1Button.clicked -= LoadLevel1;
        }

        if (level2Button != null)
        {
            level2Button.clicked -= LoadLevel2;
        }
    }

    private void StartGame()
    {
        LoadScene("Scene1");
    }

    private void LoadLevel1()
    {
        LoadScene("Scene1");
    }

    private void LoadLevel2()
    {
        LoadScene("Scene2");
    }

    private void LoadScene(string sceneName)
    {
        if (!Application.CanStreamedLevelBeLoaded(sceneName))
        {
            Debug.LogError(
                sceneName + " is not in Build Settings or Build Profiles."
            );
            return;
        }

        SceneManager.LoadScene(sceneName);
    }

    private void OpenLevelSelect()
    {
        Debug.Log("LEVEL SELECT CLICKED");

        mainMenuPanel.style.display = DisplayStyle.None;
        levelSelectPanel.style.display = DisplayStyle.Flex;

        Button backButton =
            levelSelectPanel.Q<Button>("level-select-back-button");

        if (backButton != null)
        {
            backButton.Focus();
        }
    }

    private void CloseLevelSelect()
    {
        Debug.Log("BACK CLICKED");

        levelSelectPanel.style.display = DisplayStyle.None;
        mainMenuPanel.style.display = DisplayStyle.Flex;

        Button levelButton =
            mainMenuPanel.Q<Button>("level-select-button");

        if (levelButton != null)
        {
            levelButton.Focus();
        }
    }
}