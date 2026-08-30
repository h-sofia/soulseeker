using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f; // Turn off gravity
    }

    void FixedUpdate()
    {
        // 1. Check which keys are pressed
        bool pressingLeft = Keyboard.current != null && Keyboard.current.aKey.isPressed;
        bool pressingRight = Keyboard.current != null && Keyboard.current.dKey.isPressed;
        bool pressingUp = Keyboard.current != null && Keyboard.current.wKey.isPressed;
        bool pressingDown = Keyboard.current != null && Keyboard.current.sKey.isPressed;

        // 2. Calculate movement direction
        float moveX = 0f;
        float moveY = 0f;

        if (pressingLeft) moveX = -1f;
        if (pressingRight) moveX = 1f;
        if (pressingUp) moveY = 1f;
        if (pressingDown) moveY = -1f;

        // 3. Apply velocity (or force it to zero if no keys are pressed)
        if (moveX != 0f || moveY != 0f)
        {
            // Moving: apply speed
            Vector2 moveInput = new Vector2(moveX, moveY).normalized;
            rb.linearVelocity = moveInput * moveSpeed;
        }
        else
        {
            // NOT moving: FORCE velocity to zero
            rb.linearVelocity = Vector2.zero;
        }
    }
}