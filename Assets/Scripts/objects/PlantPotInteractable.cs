using UnityEngine;

public class PlantPotInteractable : MonoBehaviour, IInteractable
{
    [TextArea(3, 8)]
    [SerializeField] private string dialogue =
        "The plant looks healthy. Someone must be taking care of it.";

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
        Debug.Log("Press E to inspect the plant.");
    }

    public void HidePrompt()
    {
    }
}