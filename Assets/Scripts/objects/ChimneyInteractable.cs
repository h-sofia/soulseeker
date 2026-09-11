using UnityEngine;

public class ChimneyInteractable : MonoBehaviour, IInteractable
{
    [TextArea(3, 8)]
    [SerializeField] private string dialogue =
        "The chimney is cold. No fire has burned here recently.";

    [SerializeField] private DialogueUI dialogueUI;

    public void Interact()
    {
        if (dialogueUI == null)
        {
            Debug.LogError(
                $"DialogueUI is not assigned on {name}.",
                this
            );

            return;
        }

        dialogueUI.ShowDialogue(dialogue);
    }

    public void ShowPrompt()
    {
        Debug.Log("Press E to inspect the chimney.");
    }

    public void HidePrompt()
    {
    }
}