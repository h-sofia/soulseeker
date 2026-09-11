using TMPro;
using UnityEngine;
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
            Debug.LogError(
                $"Missing DialogueUI reference on {name}. " +
                $"Panel: {dialoguePanel != null}, " +
                $"Text: {dialogueText != null}, " +
                $"Button: {closeButton != null}",
                this
            );

            enabled = false;
            return;
        }

        dialoguePanel.SetActive(false);
        closeButton.onClick.AddListener(CloseDialogue);
    }

    public void ShowDialogue(string message)
    {
        if (dialoguePanel == null ||
            dialogueText == null)
        {
            Debug.LogError("Panel or text is missing from DialogueUI.", this);
            return;
        }

        dialogueText.text = message;
        dialoguePanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseDialogue()
    {
        if (dialoguePanel == null)
        {
            return;
        }

        dialoguePanel.SetActive(false);
        Time.timeScale = 1f;
    }
}