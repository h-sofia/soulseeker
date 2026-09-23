using UnityEngine;
using UnityEngine.InputSystem;

public class Scene3RuntimeSetup : MonoBehaviour
{
    [SerializeField] private Vector3 playerSpawn = new Vector3(0f, 0f, -0.5f);
    [SerializeField] private Sprite playerSprite;
    [SerializeField] private RuntimeAnimatorController playerAnimator;
    [SerializeField] private InputActionAsset inputActions;

    private void Awake()
    {
        Scene2DialogueUI dialogueUI = gameObject.AddComponent<Scene2DialogueUI>();
        StarInteractable[] stars = FindObjectsByType<StarInteractable>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );
        foreach (StarInteractable star in stars)
        {
            star.SetDialogueUI(dialogueUI);
        }

        GameObject inputManager = new GameObject("INPUTMANAGER");
        PlayerInput playerInput = inputManager.AddComponent<PlayerInput>();
        playerInput.actions = inputActions;
        playerInput.defaultActionMap = "Player";
        inputManager.AddComponent<INPUTMANAGER>();
        playerInput.ActivateInput();

        GameObject player = new GameObject("Player");
        player.layer = 8;
        player.tag = "Player";
        player.transform.position = playerSpawn;

        Rigidbody2D body = player.AddComponent<Rigidbody2D>();
        body.gravityScale = 0f;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;

        SpriteRenderer renderer = player.AddComponent<SpriteRenderer>();
        renderer.sprite = playerSprite;
        renderer.sortingLayerName = "bg";
        renderer.sortingOrder = 100;

        Animator animator = player.AddComponent<Animator>();
        animator.runtimeAnimatorController = playerAnimator;

        CapsuleCollider2D collider = player.AddComponent<CapsuleCollider2D>();
        collider.offset = new Vector2(0.01f, -0.81f);
        collider.size = new Vector2(0.87f, 3.3333333f);

        player.AddComponent<PlayerMovementS>();
        PlayerInteractor interactor = player.AddComponent<PlayerInteractor>();
        GameObject sourceObject = new GameObject("interactSource");
        sourceObject.transform.SetParent(player.transform);
        sourceObject.transform.localPosition = Vector3.zero;
        interactor.ConfigureInteraction(sourceObject.transform, 3f, 1 << 8);

        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Scene 3 requires a camera tagged MainCamera.", this);
            return;
        }

        mainCamera.transform.position = new Vector3(
            playerSpawn.x,
            playerSpawn.y,
            mainCamera.transform.position.z
        );
        Scene3CameraFollow follow =
            mainCamera.gameObject.AddComponent<Scene3CameraFollow>();
        follow.Target = player.transform;
    }
}
