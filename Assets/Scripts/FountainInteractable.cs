using UnityEngine;

public class FountainInteractable : MonoBehaviour, IInteractable
{
    private const string Dialogue =
        "There are a bunch of coins in the bottom. Maybe I should make a dive for it. Y'know, for the bus fare.";

    private DialogueUI dialogueUI;

    public void SetDialogueUI(DialogueUI ui)
    {
        dialogueUI = ui;
    }

    public void Interact()
    {
        if (dialogueUI == null)
        {
            Debug.LogError("The fountain is missing its DialogueUI.", this);
            return;
        }

        dialogueUI.ShowDialogue(Dialogue);
    }

    public void ShowPrompt()
    {
    }

    public void HidePrompt()
    {
    }
}
