using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform interactSource;
    [SerializeField] private float interactRange = 3f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private DialogueUI dialogueUI;

    [Header("2D Direction")]
    [SerializeField] private Vector2 interactDirection = Vector2.up;

    private IInteractable currentInteractable;

    private void Update()
    {
        if (dialogueUI != null && dialogueUI.IsOpen)
        {
            return;
        }

        CheckForInteractable();

        if (currentInteractable != null &&
            Keyboard.current != null &&
            Keyboard.current.eKey.wasPressedThisFrame)
        {
            currentInteractable.Interact();
        }
    }

    private void CheckForInteractable()
    {
        if (interactSource == null)
        {
            Debug.LogError("Interact source is not assigned.", this);
            return;
        }

        RaycastHit2D hit = Physics2D.Raycast(
            interactSource.position,
            interactDirection.normalized,
            interactRange,
            interactableLayer
        );

        if (hit.collider != null)
        {
            IInteractable interactable =
                hit.collider.GetComponentInParent<IInteractable>();

            if (interactable != null)
            {
                if (interactable != currentInteractable)
                {
                    if (currentInteractable != null)
                    {
                        currentInteractable.HidePrompt();
                    }

                    currentInteractable = interactable;
                    currentInteractable.ShowPrompt();

                    Debug.Log(
                        $"Interactable found: {hit.collider.name}, " +
                        $"distance: {hit.distance}"
                    );
                }

                return;
            }
        }

        if (currentInteractable != null)
        {
            currentInteractable.HidePrompt();
            currentInteractable = null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (interactSource == null)
        {
            return;
        }

        Gizmos.color = Color.yellow;

        Gizmos.DrawRay(
            interactSource.position,
            interactDirection.normalized * interactRange
        );
    }
}