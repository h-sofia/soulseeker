using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementwa : MonoBehaviour
{
    public CharacterController2D controller;
    public float runSpeed = 5f;

    public Transform firePoint;

    float horizontalMove = 0f;

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalMove = 0f;

        if (Keyboard.current != null)
        {
            if (Keyboard.current.aKey.isPressed)
            {
                horizontalMove = -runSpeed;

                animator.SetBool("IsWalking", true);
                animator.SetBool("FacingRight", false);

                firePoint.localPosition = new Vector3(-2.26f, 1.4f, 0f);
                firePoint.localRotation = Quaternion.Euler(0f, 0f, 180f);
            }
            else if (Keyboard.current.dKey.isPressed)
            {
                horizontalMove = runSpeed;

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
    }

    void FixedUpdate()
    {
        controller.Move(horizontalMove * Time.fixedDeltaTime, false, false);
    }
}