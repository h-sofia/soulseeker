using UnityEngine;

public class DoorTeleportInteractable : MonoBehaviour, IInteractable
{
    private Vector3 destination;

    public void SetDestination(Vector3 position)
    {
        destination = position;
    }

    public void Interact()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            Debug.LogError("Cannot use the door because no Player was found.", this);
            return;
        }

        player.transform.position = destination;
        Rigidbody2D body = player.GetComponent<Rigidbody2D>();
        if (body != null)
        {
            body.linearVelocity = Vector2.zero;
        }
    }

    public void ShowPrompt()
    {
        Debug.Log("Press E to use the door.");
    }

    public void HidePrompt()
    {
    }
}
