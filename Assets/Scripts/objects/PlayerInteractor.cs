using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform interactSource;
    [SerializeField] private float interactRange = 3f;
    [SerializeField] private float castRadius = 0.2f;
    [SerializeField] private LayerMask interactableLayer;
    [SerializeField] private DialogueUI dialogueUI;

    [Header("2D Direction")]
    [SerializeField] private Vector2 interactDirection = Vector2.up;

    private IInteractable currentInteractable;

    public void ConfigureInteraction(Transform source, float range, LayerMask layer)
    {
        interactSource = source;
        interactRange = range;
        interactableLayer = layer;
    }

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

    Collider2D[] colliders = Physics2D.OverlapCircleAll(
        interactSource.position,
        interactRange,
        interactableLayer
    );

    IInteractable closestInteractable = null;
    float closestDistance = float.MaxValue;

    foreach (Collider2D collider in colliders)
    {
        if (collider == null ||
            collider.transform.IsChildOf(transform))
        {
            continue;
        }

        IInteractable interactable =
            collider.GetComponentInParent<IInteractable>();

        if (interactable == null)
        {
            continue;
        }

        Vector2 closestPoint =
            collider.ClosestPoint(interactSource.position);

        float distance = Vector2.Distance(
            interactSource.position,
            closestPoint
        );

        if (distance < closestDistance)
        {
            closestInteractable = interactable;
            closestDistance = distance;
        }
    }

    if (closestInteractable != null)
    {
        if (closestInteractable != currentInteractable)
        {
            if (currentInteractable != null)
            {
                currentInteractable.HidePrompt();
            }

            currentInteractable = closestInteractable;
            currentInteractable.ShowPrompt();

            Debug.Log(
                $"Interactable found. Distance: {closestDistance}"
            );
        }

        return;
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

        Vector3 direction =
            (Vector3)interactDirection.normalized * interactRange;

        Gizmos.DrawRay(interactSource.position, direction);
        Gizmos.DrawWireSphere(
            interactSource.position + direction,
            castRadius
        );
    }
}