using UnityEngine;
using UnityEngine.InputSystem;

public class Scene2PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D body;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        if (body == null)
        {
            Debug.LogError("Scene 2 player is missing Rigidbody2D.", this);
            enabled = false;
        }
    }

    private void Update()
    {
        if (Time.timeScale == 0f)
        {
            body.linearVelocity = Vector2.zero;
            return;
        }

        if (Keyboard.current == null)
        {
            body.linearVelocity = INPUTMANAGER.Movement * moveSpeed;
            return;
        }

        Vector2 movement = Vector2.zero;
        if (Keyboard.current.aKey.isPressed || Keyboard.current.leftArrowKey.isPressed)
        {
            movement.x -= 1f;
        }
        if (Keyboard.current.dKey.isPressed || Keyboard.current.rightArrowKey.isPressed)
        {
            movement.x += 1f;
        }
        if (Keyboard.current.sKey.isPressed || Keyboard.current.downArrowKey.isPressed)
        {
            movement.y -= 1f;
        }
        if (Keyboard.current.wKey.isPressed || Keyboard.current.upArrowKey.isPressed)
        {
            movement.y += 1f;
        }

        body.linearVelocity = movement.normalized * moveSpeed;
    }
}
