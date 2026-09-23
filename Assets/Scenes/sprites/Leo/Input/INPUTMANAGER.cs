using UnityEngine;
using UnityEngine.InputSystem;

public class INPUTMANAGER : MonoBehaviour
{
    public static Vector2 Movement;

    private PlayerInput _playerInput;
    private InputAction _moveAction;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        InitializeMoveAction();
    }

    private void Update()
    {
        if (_moveAction == null)
        {
            InitializeMoveAction();
        }

        if (_moveAction == null)
        {
            Movement = Vector2.zero;
            return;
        }

        Movement = _moveAction.ReadValue<Vector2>();
    }

    private void InitializeMoveAction()
    {
        if (_playerInput != null &&
            _playerInput.actions != null)
        {
            _playerInput.ActivateInput();
            _moveAction = _playerInput.actions["Move"];
        }
    }
}
