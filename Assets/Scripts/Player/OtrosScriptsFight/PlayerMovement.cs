using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementwa : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 40f;
    float horizontalMove = 0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed)
                horizontalMove = -runSpeed;
            else if (Keyboard.current.dKey.isPressed)
                horizontalMove = runSpeed;
        }
    }
    void FixedUpdate() {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }
}
