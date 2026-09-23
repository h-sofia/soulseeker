using UnityEngine;

public class StarInteractable : MonoBehaviour, IInteractable
{
    [TextArea(3, 8)]
    [SerializeField] private string dialogue =
        "The star shines brightly, but something about it feels familiar.";

    private Scene2DialogueUI dialogueUI;

    public void SetDialogueUI(Scene2DialogueUI ui)
    {
        dialogueUI = ui;
    }

    public void Interact()
    {
        if (dialogueUI == null)
        {
            Debug.LogError(
                $"Scene 3 dialogue UI is not assigned on {gameObject.name}.",
                this
            );
            return;
        }

        dialogueUI.ShowDialogue(dialogue);
    }

    public void ShowPrompt()
    {
        Debug.Log("Press E to examine the star.");
    }

    public void HidePrompt()
    {
    }
}
