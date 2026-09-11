using UnityEngine;

public class BookshelfInteractable : MonoBehaviour, IInteractable
{
    [Header("Dialogue")]
    [TextArea(3, 8)]
    [SerializeField] private string dialogue =
        "The bookshelf is filled with old, dusty books.";

    [SerializeField] private DialogueUI dialogueUI;

    public void Interact()
    {
        if (dialogueUI == null)
        {
            Debug.LogError(
                $"DialogueUI is not assigned on {gameObject.name}.",
                this
            );

            return;
        }

        dialogueUI.ShowDialogue(dialogue);
    }

    public void ShowPrompt()
    {
        Debug.Log("Press E to read the bookshelf.");
        // Enable your interaction prompt UI here.
    }

    public void HidePrompt()
    {
        // Disable your interaction prompt UI here.
    }
}