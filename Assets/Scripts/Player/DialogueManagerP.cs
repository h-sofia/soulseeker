using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DialogueManagerP : MonoBehaviour
{
    [System.Serializable]
    public class DialogueLine
    {
        public string speaker;

        [TextArea(2, 5)]
        public string text;

        public Sprite portrait;
    }

    public DialogueLine[] dialogue;

    public GameObject dialogueUI;
    public GameObject player;

    public GameObject npcToFade;

    public int fadeAfterLine = 11;

    public float fadeTime = 1f;

    public string nextScene;

    private UIDocument uiDocument;

    private Label speakerName;
    private Label dialogueText;
    private VisualElement portrait;

    private int currentLine = 0;

    private PlayerMovementS playerMovement;
    private VisualElement dialogueRoot;

    private bool fadingNPC = false;
    private bool dialogueActive = false;

    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }

        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovementS>();
            if (playerMovement == null)
            {
                playerMovement = player.GetComponentInChildren<PlayerMovementS>();
            }
        }

        if (dialogueUI == null)
        {
            UIDocument[] documents = FindObjectsByType<UIDocument>(
                FindObjectsInactive.Include,
                FindObjectsSortMode.None);

            foreach (UIDocument document in documents)
            {
                if (document.visualTreeAsset != null &&
                    document.visualTreeAsset.name == "DialogueUI")
                {
                    dialogueUI = document.gameObject;
                    break;
                }
            }
        }

        if (dialogueUI != null)
        {
            dialogueUI.SetActive(false);
        }
    }

    public void StartDialogue()
    {
        StartDialogue(null);
    }

    public void StartDialogue(GameObject triggeringPlayer)
    {
        if (dialogueActive)
        {
            return;
        }

        if (playerMovement == null && triggeringPlayer != null)
        {
            playerMovement = triggeringPlayer.GetComponent<PlayerMovementS>();
            if (playerMovement == null)
            {
                playerMovement = triggeringPlayer.GetComponentInChildren<PlayerMovementS>();
            }
        }

        if (dialogueUI == null || playerMovement == null || dialogue == null || dialogue.Length == 0)
        {
            Debug.LogError(
                "DialogueManagerP needs a DialogueUI UIDocument, a Player with PlayerMovementS, and at least one dialogue line.",
                this);
            return;
        }

        playerMovement.canMove = false;

        dialogueUI.SetActive(true);

        uiDocument = dialogueUI.GetComponent<UIDocument>();
        if (uiDocument == null)
        {
            playerMovement.canMove = true;
            dialogueUI.SetActive(false);
            Debug.LogError("DialogueManagerP's dialogue UI needs a UIDocument component.", dialogueUI);
            return;
        }

        dialogueRoot = uiDocument.rootVisualElement;

        speakerName = dialogueRoot.Q<Label>("SpeakerName");
        dialogueText = dialogueRoot.Q<Label>("DialogueText");
        portrait = dialogueRoot.Q<VisualElement>("Portrait");

        if (speakerName == null || dialogueText == null || portrait == null)
        {
            playerMovement.canMove = true;
            dialogueUI.SetActive(false);
            Debug.LogError("DialogueUI is missing SpeakerName, DialogueText, or Portrait.", dialogueUI);
            return;
        }

        currentLine = 0;
        fadingNPC = false;
        dialogueActive = true;
        dialogueRoot.RegisterCallback<ClickEvent>(OnDialogueClicked);
        ShowLine();
    }

    private void OnDialogueClicked(ClickEvent evt)
    {
        if (dialogueActive && !fadingNPC)
        {
            NextLine();
        }
    }

    void ShowLine()
    {
        speakerName.text = dialogue[currentLine].speaker;
        dialogueText.text = dialogue[currentLine].text;

        if (dialogue[currentLine].portrait != null)
        {
            portrait.style.backgroundImage =
                new StyleBackground(dialogue[currentLine].portrait);
        }
    }

    void NextLine()
    {
        currentLine++;

        if (currentLine >= dialogue.Length)
        {
            EndDialogue();
            return;
        }

        if (currentLine == fadeAfterLine - 1)
        {
            StartCoroutine(FadeNPC());
        }

        ShowLine();
    }

    IEnumerator FadeNPC()
    {
        fadingNPC = true;

        SpriteRenderer sprite = npcToFade != null
            ? npcToFade.GetComponent<SpriteRenderer>()
            : null;

        if (sprite != null)
        {
            Color color = sprite.color;
            float startAlpha = color.a;

            float timer = 0f;

            while (timer < fadeTime)
            {
                timer += Time.deltaTime;

                float alpha = Mathf.Lerp(
                    startAlpha,
                    0f,
                    timer / fadeTime
                );

                sprite.color = new Color(
                    color.r,
                    color.g,
                    color.b,
                    alpha
                );

                yield return null;
            }

            sprite.color = new Color(
                color.r,
                color.g,
                color.b,
                0f
            );
        }

        if (npcToFade != null)
        {
            npcToFade.SetActive(false);
        }

        fadingNPC = false;
    }

    void EndDialogue()
    {
        dialogueActive = false;
        dialogueRoot.UnregisterCallback<ClickEvent>(OnDialogueClicked);
        playerMovement.canMove = true;

        dialogueUI.SetActive(false);

        if (!string.IsNullOrEmpty(nextScene))
        {
            SceneManager.LoadScene(nextScene);
        }
    }


}