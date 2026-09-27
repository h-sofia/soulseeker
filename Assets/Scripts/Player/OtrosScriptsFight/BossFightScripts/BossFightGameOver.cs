using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BossFightGameOver : MonoBehaviour
{
    private const string RestartScene = "Scene3";
    private const string MainMenuScene = "SampleScene";

    private bool uiCreated;

    private void Awake()
    {
        CreateScreen();
        EnsureEventSystem();
    }

    private void OnEnable()
    {
        Time.timeScale = 0f;
    }

    public void Restart()
    {
        LoadScene(RestartScene);
    }

    public void MainMenu()
    {
        LoadScene(MainMenuScene);
    }

    public void Quit()
    {
        Time.timeScale = 1f;
        Application.Quit();
    }

    private void LoadScene(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }

    private void CreateScreen()
    {
        if (uiCreated)
            return;

        uiCreated = true;

        Canvas canvas = GetComponent<Canvas>();
        if (canvas == null)
        {
            Debug.LogError("BossFightGameOver requires a Canvas component.", this);
            return;
        }

        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 100;

        Font font = Resources.GetBuiltinResource<Font>("LegacyRuntime.ttf");
        if (font == null)
        {
            Debug.LogError("Could not load Unity's built-in UI font.", this);
            return;
        }

        Transform panel = CreatePanel(transform);
        CreateLabel(panel, "Game Over", 64, new Vector2(0f, 190f));
        CreateButton(panel, "Restart", new Vector2(0f, 45f), Restart);
        CreateButton(panel, "Main Menu", new Vector2(0f, -35f), MainMenu);
        CreateButton(panel, "Quit", new Vector2(0f, -115f), Quit);

        void CreateLabel(Transform parent, string text, int fontSize, Vector2 position)
        {
            GameObject labelObject = new GameObject("GameOverTitle", typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
            RectTransform rect = labelObject.GetComponent<RectTransform>();
            rect.SetParent(parent, false);
            rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(700f, 100f);
            rect.anchoredPosition = position;

            Text label = labelObject.GetComponent<Text>();
            label.text = text;
            label.font = font;
            label.fontSize = fontSize;
            label.fontStyle = FontStyle.Bold;
            label.alignment = TextAnchor.MiddleCenter;
            label.color = Color.white;
            label.raycastTarget = false;
        }

        void CreateButton(Transform parent, string text, Vector2 position, UnityEngine.Events.UnityAction onClick)
        {
            GameObject buttonObject = new GameObject(text + "Button", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image), typeof(Button));
            RectTransform rect = buttonObject.GetComponent<RectTransform>();
            rect.SetParent(parent, false);
            rect.anchorMin = rect.anchorMax = new Vector2(0.5f, 0.5f);
            rect.sizeDelta = new Vector2(320f, 64f);
            rect.anchoredPosition = position;

            Image image = buttonObject.GetComponent<Image>();
            image.color = new Color(0.35f, 0.04f, 0.06f, 1f);

            Button button = buttonObject.GetComponent<Button>();
            button.targetGraphic = image;
            button.onClick.AddListener(onClick);

            GameObject labelObject = new GameObject("Label", typeof(RectTransform), typeof(CanvasRenderer), typeof(Text));
            RectTransform labelRect = labelObject.GetComponent<RectTransform>();
            labelRect.SetParent(rect, false);
            labelRect.anchorMin = Vector2.zero;
            labelRect.anchorMax = Vector2.one;
            labelRect.offsetMin = Vector2.zero;
            labelRect.offsetMax = Vector2.zero;

            Text label = labelObject.GetComponent<Text>();
            label.text = text;
            label.font = font;
            label.fontSize = 28;
            label.alignment = TextAnchor.MiddleCenter;
            label.color = Color.white;
            label.raycastTarget = false;
        }

        Transform CreatePanel(Transform parent)
        {
            GameObject panelObject = new GameObject("GameOverPanel", typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            RectTransform rect = panelObject.GetComponent<RectTransform>();
            rect.SetParent(parent, false);
            rect.anchorMin = Vector2.zero;
            rect.anchorMax = Vector2.one;
            rect.offsetMin = Vector2.zero;
            rect.offsetMax = Vector2.zero;

            Image background = panelObject.GetComponent<Image>();
            background.color = new Color(0f, 0f, 0f, 0.88f);
            background.raycastTarget = true;
            return rect;
        }
    }

    private static void EnsureEventSystem()
    {
        EventSystem eventSystem = EventSystem.current;
        if (eventSystem == null)
        {
            GameObject eventSystemObject = new GameObject("EventSystem", typeof(EventSystem));
            eventSystem = eventSystemObject.GetComponent<EventSystem>();
        }

        InputSystemUIInputModule inputModule = eventSystem.GetComponent<InputSystemUIInputModule>();
        if (inputModule == null)
        {
            inputModule = eventSystem.gameObject.AddComponent<InputSystemUIInputModule>();
            inputModule.AssignDefaultActions();
        }
    }
}
