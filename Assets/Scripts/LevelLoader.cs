using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Tilemaps;

public class LevelLoader : MonoBehaviour
{
    public GameObject gameOverUI;
    public Animator transition;
    public float transitionTime = 1f;

    private void Awake()
    {
        GameplayMainMenuButton.CreateForGameplayScene();

        if (SceneManager.GetActiveScene().name == "Scene1")
        {
            AddTilemapCollision("miniwall");
            AddTilemapCollision("LVL1");
            AddBlockingCollider("shower_0");
            AddBlockingCollider("WC_0");
            AddBlockingCollider("sink_0");
            ConfigureWallTeleport(
                "wallCollider (1)",
                new Vector3(17.21f, -43.9f, 0f)
            );
            ConfigureWallTeleport(
                "wallCollider (2)",
                new Vector3(64.08f, -69.8f, 0f)
            );
            ConfigureFountainInteraction();
        }
    }

    private void ConfigureWallTeleport(string objectName, Vector3 destination)
    {
        GameObject wall = GameObject.Find(objectName);
        if (wall == null)
        {
            Debug.LogError($"Scene 1 is missing {objectName}.", this);
            return;
        }

        BoxCollider2D collider = wall.GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            Debug.LogError($"{objectName} is missing its BoxCollider2D.", wall);
            return;
        }

        wall.layer = 8;

        DoorTeleportInteractable interactable =
            wall.GetComponent<DoorTeleportInteractable>();
        if (interactable == null)
        {
            interactable = wall.AddComponent<DoorTeleportInteractable>();
        }

        interactable.SetDestination(destination);
    }

    private void AddBlockingCollider(string objectName)
    {
        SpriteRenderer[] renderers = FindObjectsByType<SpriteRenderer>(
            FindObjectsInactive.Include,
            FindObjectsSortMode.None
        );

        foreach (SpriteRenderer renderer in renderers)
        {
            if (renderer.gameObject.name != objectName)
            {
                continue;
            }

            BoxCollider2D collider = renderer.GetComponent<BoxCollider2D>();
            if (collider == null)
            {
                collider = renderer.gameObject.AddComponent<BoxCollider2D>();
            }

            collider.isTrigger = false;
            if (renderer.sprite != null)
            {
                collider.size = renderer.sprite.bounds.size;
                collider.offset = renderer.sprite.bounds.center;
            }

            return;
        }

        Debug.LogError($"Scene 1 is missing {objectName}.", this);
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

        Debug.LogError($"Scene 1 is missing the {tilemapName} Tilemap.", this);
    }

    private void ConfigureFountainInteraction()
    {
        GameObject fountain = GameObject.Find("fountain_0");
        if (fountain == null)
        {
            Debug.LogError("Scene 1 is missing fountain_0.", this);
            return;
        }

        fountain.layer = 8;

        BoxCollider2D collider = fountain.GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            collider = fountain.AddComponent<BoxCollider2D>();
        }

        collider.isTrigger = true;
        SpriteRenderer renderer = fountain.GetComponent<SpriteRenderer>();
        if (renderer != null && renderer.sprite != null)
        {
            collider.size = renderer.sprite.bounds.size;
        }

        FountainInteractable interactable =
            fountain.GetComponent<FountainInteractable>();
        if (interactable == null)
        {
            interactable = fountain.AddComponent<FountainInteractable>();
        }

        interactable.SetDialogueUI(FindFirstObjectByType<DialogueUI>());
    }

    void Update()
    {
        if (Keyboard.current != null && Keyboard.current.enterKey.wasPressedThisFrame)
        {
            LoadNextLevel();
        }
    }

    public void LoadNextLevel()
    {
        StartCoroutine(LoadScene("Fight"));
    }

    IEnumerator LoadScene(string sceneName)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(sceneName);
    }

    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }
}
