using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button closeButton;

    private void Awake()
    {
        if (dialoguePanel == null ||
            dialogueText == null)
        {
            Debug.LogError("DialogueUI has missing Inspector references.", this);
            enabled = false;
            return;
        }

        ConfigureTextLayout();
        if (closeButton != null)
        {
            closeButton.gameObject.SetActive(false);
        }

        HideCloseLabel();
        HideSpeakerName();
        CreateExitPrompt();
        dialoguePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void HideCloseLabel()
    {
        TMP_Text[] textElements =
            dialoguePanel.GetComponentsInChildren<TMP_Text>(true);

        foreach (TMP_Text textElement in textElements)
        {
            if (string.Equals(
                    textElement.text.Trim(),
                    "close",
                    System.StringComparison.OrdinalIgnoreCase
                ))
            {
                textElement.gameObject.SetActive(false);
            }
        }
    }

    private void HideSpeakerName()
    {
        TMP_Text[] textElements =
            dialoguePanel.GetComponentsInChildren<TMP_Text>(true);

        foreach (TMP_Text textElement in textElements)
        {
            if (textElement.gameObject.name == "name" ||
                textElement.text == "Leo")
            {
                textElement.gameObject.SetActive(false);
            }
        }
    }

    private void CreateExitPrompt()
    {
        const string promptName = "DialogueExitPrompt";
        Transform existingPrompt = dialoguePanel.transform.Find(promptName);
        if (existingPrompt != null)
        {
            return;
        }

        GameObject promptObject = new GameObject(
            promptName,
            typeof(RectTransform),
            typeof(CanvasRenderer),
            typeof(TextMeshProUGUI)
        );
        RectTransform promptRect = promptObject.GetComponent<RectTransform>();
        promptRect.SetParent(dialoguePanel.transform, false);
        promptRect.anchorMin = new Vector2(0f, 0f);
        promptRect.anchorMax = new Vector2(1f, 0f);
        promptRect.pivot = new Vector2(0.5f, 0f);
        promptRect.offsetMin = new Vector2(32f, 12f);
        promptRect.offsetMax = new Vector2(-32f, 42f);

        TextMeshProUGUI promptText =
            promptObject.GetComponent<TextMeshProUGUI>();
        promptText.text = "Press Esc to exit dialogue";
        promptText.font = dialogueText.font;
        promptText.fontSize = 20f;
        promptText.color = dialogueText.color;
        promptText.alignment = TextAlignmentOptions.Center;
        promptText.enableAutoSizing = true;
        promptText.fontSizeMin = 16f;
        promptText.fontSizeMax = 20f;
        promptText.textWrappingMode = TextWrappingModes.NoWrap;
        promptText.raycastTarget = false;
    }

    private void ConfigureTextLayout()
    {
        RectTransform textRect = dialogueText.rectTransform;
        textRect.SetParent(dialoguePanel.transform, false);
        textRect.localScale = Vector3.one;
        textRect.anchorMin = Vector2.zero;
        textRect.anchorMax = Vector2.one;
        textRect.anchoredPosition = Vector2.zero;
        textRect.offsetMin = new Vector2(48f, 38f);
        textRect.offsetMax = new Vector2(-48f, -38f);

        dialogueText.alignment = TextAlignmentOptions.TopLeft;
        dialogueText.enableAutoSizing = true;
        dialogueText.fontSizeMin = 22f;
        dialogueText.fontSizeMax = 32f;
        dialogueText.fontSize = 30f;
        dialogueText.margin = new Vector4(12f, 8f, 12f, 8f);
        dialogueText.raycastTarget = false;
    }

    private void Update()
    {
        if (dialoguePanel.activeSelf &&
            Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseDialogue();
        }
    }

    public void ShowDialogue(string message)
    {
        dialogueText.text = message;
        dialoguePanel.SetActive(true);
        Time.timeScale = 0f;

        Debug.Log(
            $"Dialogue opened. Panel active: {dialoguePanel.activeSelf}"
        );
    }

    public void CloseDialogue()
    {
        dialoguePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
    public bool IsOpen
{
    get
    {
        return dialoguePanel != null &&
               dialoguePanel.activeSelf;
    }
}
}