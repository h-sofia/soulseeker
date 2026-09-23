using UnityEngine;

public class TornPageInteractable : MonoBehaviour, IInteractable
{
    [Header("Dialogue")]
    [TextArea(3, 8)]
    [SerializeField] private string dialogue = @"18 july

Mom keeps forcing me to go to church with her. I try to keep to myself yet I feel his eyes following me. I wish he’d leave me alone.

I’ll see Leo today. She keeps trying to convince me to hang out at this college party. Maybe I’ll go if only to stop thinking about everything. She doesn’t know anything, of course. No one does.";

    [SerializeField] private DialogueUI dialogueUI;
    private Scene2DialogueUI scene2DialogueUI;

    private void Awake()
    {
        if (dialogueUI == null)
        {
            dialogueUI = FindFirstObjectByType<DialogueUI>();
        }
    }

    public void SetDialogueUI(Scene2DialogueUI ui)
    {
        scene2DialogueUI = ui;
    }

    public void Interact()
    {
        if (scene2DialogueUI != null)
        {
            scene2DialogueUI.ShowDialogue(dialogue);
            return;
        }

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
        Debug.Log("Press E to read the torn page.");
    }

    public void HidePrompt()
    {
    }
}
