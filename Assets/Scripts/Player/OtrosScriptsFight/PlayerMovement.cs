using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerMovementwa : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 5f;

    public Transform firePoint;

    float horizontalMove = 0f;

    private Animator animator;
    private InputAction moveAction;

    void Awake()
    {
        animator = GetComponent<Animator>();

        PlayerInput playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("MoveFight", true);
    }

    void Update()
    {
        float moveInput = moveAction.ReadValue<Vector2>().x;
        horizontalMove = moveInput * runSpeed;

        if (moveInput < 0f)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("FacingRight", false);

            firePoint.localPosition = new Vector3(-2.26f, 1.4f, 0f);
            firePoint.localRotation = Quaternion.Euler(0f, 0f, 180f);
        }
        else if (moveInput > 0f)
        {
            animator.SetBool("IsWalking", true);
            animator.SetBool("FacingRight", true);

            firePoint.localPosition = new Vector3(2.26f, 1.4f, 0f);
            firePoint.localRotation = Quaternion.Euler(0f, 0f, 0f);
        }
        else
        {
            animator.SetBool("IsWalking", false);
        }
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }

   
}