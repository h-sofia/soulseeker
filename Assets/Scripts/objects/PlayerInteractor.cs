using UnityEngine;

public class PlayerInteractor : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private Transform interactSource; // Usually the Main Camera
    [SerializeField] private float interactRange = 3.0f;
    [SerializeField] private LayerMask interactableLayer; // Set this to an "Interactable" layer in Unity
    [SerializeField] private KeyCode interactKey = KeyCode.E;

    private IInteractable currentInteractable;

    void Update()
    {
        CheckForInteractable();

        // If we have a valid target and the player presses the key
        if (currentInteractable != null && Input.GetKeyDown(interactKey))
        {
            currentInteractable.Interact();
        }
    }

        private void CheckForInteractable()
    {
        // Shoot a ray straight forward from the interaction source
        Ray ray = new Ray(interactSource.position, interactSource.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, interactRange, interactableLayer))
        {
            // Check if the object we hit has an IInteractable script
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();

            if (interactable != null)
            {
                // We found a new or existing interactable target
                if (interactable != currentInteractable)
                {
                    // If we were looking at something else before, hide its prompt first
                    if (currentInteractable != null) currentInteractable.HidePrompt();

                    currentInteractable = interactable;
                    
                    // Call your interface's ShowPrompt method!
                    currentInteractable.ShowPrompt();
                }
                return; // Exit early since we found something valid
            }
        }

        // If the raycast misses or hits something non-interactable, clear the target
        if (currentInteractable != null)
        {
            // Call your interface's HidePrompt method!
            currentInteractable.HidePrompt();
            currentInteractable = null;
        }
    }


    // Optional: Draws a line in the editor scene view to help you debug range
    private void OnDrawGizmosSelected()
    {
        if (interactSource == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(interactSource.position, interactSource.forward * interactRange);
    }
}
