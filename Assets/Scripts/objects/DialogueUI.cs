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
            dialogueText == null ||
            closeButton == null)
        {
            Debug.LogError("DialogueUI has missing Inspector references.", this);
            enabled = false;
            return;
        }

        closeButton.onClick.AddListener(CloseDialogue);
        dialoguePanel.SetActive(false);
        Time.timeScale = 1f;
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
        if (closeButton != null)
        {
            closeButton.onClick.RemoveListener(CloseDialogue);
        }

        Time.timeScale = 1f;
    }
}