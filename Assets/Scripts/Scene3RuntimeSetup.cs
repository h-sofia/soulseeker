using UnityEngine;
using UnityEngine.InputSystem;

public class Scene3RuntimeSetup : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Sprite playerSprite;
    [SerializeField] private RuntimeAnimatorController playerAnimator;
    [SerializeField] private InputActionAsset inputActions;

    private void Awake()
    {
        GameplayMainMenuButton.CreateForGameplayScene();
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

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (player == null)
        {
            Debug.LogError("Scene 3 needs a Player object to use as its spawn point.", this);
            return;
        }

        player.layer = 8;
        player.tag = "Player";

        Rigidbody2D body = player.GetComponent<Rigidbody2D>();
        if (body == null)
        {
            body = player.AddComponent<Rigidbody2D>();
        }
        body.gravityScale = 0f;
        body.constraints = RigidbodyConstraints2D.FreezeRotation;

        SpriteRenderer renderer = player.GetComponent<SpriteRenderer>();
        if (renderer == null)
        {
            renderer = player.AddComponent<SpriteRenderer>();
        }
        renderer.sprite = playerSprite;
        renderer.enabled = true;
        renderer.color = Color.white;
        renderer.sortingLayerName = "bg";
        renderer.sortingOrder = 100;

        Animator animator = player.GetComponent<Animator>();
        if (animator == null)
        {
            animator = player.AddComponent<Animator>();
        }
        animator.runtimeAnimatorController = playerAnimator;
        animator.enabled = true;

        CapsuleCollider2D collider = player.GetComponent<CapsuleCollider2D>();
        if (collider == null)
        {
            collider = player.AddComponent<CapsuleCollider2D>();
        }
        collider.offset = new Vector2(0.01f, -0.81f);
        collider.size = new Vector2(0.87f, 3.3333333f);

        if (player.GetComponent<PlayerMovementS>() == null)
        {
            player.AddComponent<PlayerMovementS>();
        }

        PlayerInteractor interactor = player.GetComponent<PlayerInteractor>();
        if (interactor == null)
        {
            interactor = player.AddComponent<PlayerInteractor>();
        }
        Transform source = player.transform.Find("interactSource");
        if (source == null)
        {
            GameObject sourceObject = new GameObject("interactSource");
            source = sourceObject.transform;
            source.SetParent(player.transform);
            source.localPosition = Vector3.zero;
        }
        interactor.ConfigureInteraction(source, 3f, 1 << 8);

        Camera mainCamera = Camera.main;
        if (mainCamera == null)
        {
            Debug.LogError("Scene 3 requires a camera tagged MainCamera.", this);
            return;
        }

        mainCamera.transform.position = new Vector3(
            player.transform.position.x,
            player.transform.position.y,
            mainCamera.transform.position.z
        );
        Scene3CameraFollow follow =
            mainCamera.gameObject.AddComponent<Scene3CameraFollow>();
        follow.Target = player.transform;
    }
}
