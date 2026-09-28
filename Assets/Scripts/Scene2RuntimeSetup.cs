using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class Scene2RuntimeSetup : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Sprite playerSprite;
    [SerializeField] private RuntimeAnimatorController playerAnimator;
    [SerializeField] private InputActionAsset inputActions;

    private void Awake()
    {
        Debug.Log("Scene2RuntimeSetup started.");
        GameplayMainMenuButton.CreateForGameplayScene();
        AddTilemapCollision("wallTop");
        AddTilemapCollision("wall2ndfloor_0");
        ConfigureTeleportDoor(
            "doorGreen_0",
            new Vector3(8.9f, 12.4f, -1.3f)
        );
        ConfigureTeleportDoor(
            "greenDoorReturn",
            new Vector3(7.27f, -7.01f, -3f)
        );
        ConfigureTeleportDoor(
            "doorBlue_0",
            new Vector3(40.1f, 12.81f, -1.3f)
        );
        ConfigureTeleportDoor(
            "blueDoorReturn",
            new Vector3(40.1f, -6.92f, -1.3f)
        );
        ConfigureTeleportDoor(
            "doorRed_0",
            new Vector3(78.7f, 13.16f, -2.1f)
        );
        ConfigureTeleportDoor(
            "redDoorReturn",
            new Vector3(78.7f, -6.97f, -2.1f)
        );
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

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (player == null)
        {
            Debug.LogError("Scene 2 needs a Player object to use as its spawn point.", this);
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
            $"Scene 2 player configured at {player.transform.position} with sprite {renderer.sprite?.name}."
        );

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

        interactor.ConfigureInteraction(
            source,
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
            player.transform.position.x,
            player.transform.position.y,
            mainCamera.transform.position.z
        );
        Scene2CameraFollow follow =
            mainCamera.gameObject.AddComponent<Scene2CameraFollow>();
        follow.Target = player.transform;
        Debug.Log("Scene 2 camera attached to player.");
    }

    private void AddTilemapCollision(string tilemapName)
    {
        Tilemap[] tilemaps = FindObjectsByType<Tilemap>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        foreach (Tilemap tilemap in tilemaps)
        {
            if (tilemap.gameObject.name != tilemapName)
            {
                continue;
            }

            if (tilemap.GetComponent<TilemapCollider2D>() == null)
            {
                tilemap.gameObject.AddComponent<TilemapCollider2D>();
            }

            return;
        }

        Debug.LogError(
            $"Scene 2 is missing the {tilemapName} Tilemap.",
            this
        );
    }

    private void ConfigureTeleportDoor(string doorName, Vector3 destination)
    {
        GameObject door = GameObject.Find(doorName);
        if (door == null)
        {
            Debug.LogError($"Scene 2 is missing {doorName}.", this);
            return;
        }

        door.layer = 8;

        BoxCollider2D doorCollider = door.GetComponent<BoxCollider2D>();
        if (doorCollider == null)
        {
            doorCollider = door.AddComponent<BoxCollider2D>();
        }

        doorCollider.isTrigger = true;
        SpriteRenderer renderer = door.GetComponent<SpriteRenderer>();
        if (renderer != null && renderer.sprite != null)
        {
            doorCollider.size = renderer.sprite.bounds.size;
        }

        DoorTeleportInteractable interactable =
            door.GetComponent<DoorTeleportInteractable>();
        if (interactable == null)
        {
            interactable = door.AddComponent<DoorTeleportInteractable>();
        }

        interactable.SetDestination(destination);
    }
}
