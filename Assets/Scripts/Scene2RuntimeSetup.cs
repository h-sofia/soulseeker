using UnityEngine;
using UnityEngine.InputSystem;

public class Scene2RuntimeSetup : MonoBehaviour
{
    [SerializeField] private Vector3 playerSpawn = new Vector3(10f, 40f, -1f);
    [SerializeField] private Sprite playerSprite;
    [SerializeField] private RuntimeAnimatorController playerAnimator;
    [SerializeField] private InputActionAsset inputActions;

    private void Awake()
    {
        Debug.Log("Scene2RuntimeSetup started.");
        GameplayMainMenuButton.CreateForGameplayScene();
        GameObject inputManager = new GameObject("INPUTMANAGER");
        PlayerInput playerInput = inputManager.AddComponent<PlayerInput>();
        playerInput.actions = inputActions;
        playerInput.defaultActionMap = "Player";
        inputManager.AddComponent<INPUTMANAGER>();
        playerInput.ActivateInput();

        Scene2DialogueUI dialogueUI = gameObject.AddComponent<Scene2DialogueUI>();
        TornPageInteractable[] pages =
            FindObjectsByType<TornPageInteractable>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None
            );
        foreach (TornPageInteractable page in pages)
        {
            page.SetDialogueUI(dialogueUI);
        }

        GameObject player = new GameObject("Player");
        player.layer = 8;
        player.tag = "Player";
        player.transform.position = new Vector3(
            playerSpawn.x,
            playerSpawn.y,
            -0.5f
        );

        player.transform.localScale = Vector3.one;

        Rigidbody2D body = player.AddComponent<Rigidbody2D>();
        body.gravityScale = 0f;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;

        SpriteRenderer renderer = player.AddComponent<SpriteRenderer>();
        if (playerSprite == null)
        {
            Debug.LogError(
                "Scene 2 player sprite is not assigned on Scene2RuntimeSetup.",
                this
            );
        }
        renderer.sprite = playerSprite;
        renderer.enabled = true;
        renderer.color = Color.white;
        renderer.sortingLayerName = "bg";
        renderer.sortingOrder = 100;
        Debug.Log(
            $"Scene 2 player created at {player.transform.position} with sprite {renderer.sprite?.name}."
        );

        Animator animator = player.AddComponent<Animator>();
        animator.runtimeAnimatorController = playerAnimator;
        animator.enabled = true;

        CapsuleCollider2D collider = player.AddComponent<CapsuleCollider2D>();
        collider.offset = new Vector2(0.01f, -0.81f);
        collider.size = new Vector2(0.87f, 3.3333333f);

        player.AddComponent<PlayerMovementS>();
        PlayerInteractor interactor = player.AddComponent<PlayerInteractor>();
        GameObject sourceObject = new GameObject("interactSource");
        sourceObject.transform.SetParent(player.transform);
        sourceObject.transform.localPosition = Vector3.zero;
        interactor.ConfigureInteraction(
            sourceObject.transform,
            3f,
            1 << 8
        );

        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Scene 2 requires a camera tagged MainCamera.");
            return;
        }

        mainCamera.transform.position = new Vector3(
            playerSpawn.x,
            playerSpawn.y,
            mainCamera.transform.position.z
        );
        Scene2CameraFollow follow =
            mainCamera.gameObject.AddComponent<Scene2CameraFollow>();
        follow.Target = player.transform;
        Debug.Log("Scene 2 camera attached to player.");
    }
}
