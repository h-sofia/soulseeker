using UnityEngine;

public class DialogueTriggerP : MonoBehaviour
{
    public DialogueManagerP dialogueManager;

    private bool triggered = false;

    private void Awake()
    {
        if (dialogueManager == null)
        {
            dialogueManager = FindFirstObjectByType<DialogueManagerP>();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered)
        {
            return;
        }

        if (other.transform.root.CompareTag("Player"))
        {
            if (dialogueManager == null)
            {
                Debug.LogError("DialogueTriggerP could not find a DialogueManagerP in the scene.", this);
                return;
            }

            triggered = true;
            dialogueManager.StartDialogue(other.transform.root.gameObject);
        }
    }
}