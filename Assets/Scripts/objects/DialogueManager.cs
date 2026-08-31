using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("UI Elements")]
    [SerializeField] private GameObject dialogueCanvas;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;

    private string[] _lines;
    private int _currentIndex;
    private bool _isActive;

    private void Awake()
    {
        // Singleton pattern to easily call this script from anywhere
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Update()
    {
        if (!_isActive) return;

        // Advance dialogue when pressing Space or E
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.E))
        {
            DisplayNextLine();
        }
    }

    public void StartDialogue(string speakerName, string[] textLines)
    {
        _lines = textLines;
        _currentIndex = 0;
        _isActive = true;

        nameText.text = speakerName;
        dialogueCanvas.SetActive(true);
        
        // Optional: Freeze player movement here (e.g., Time.timeScale = 0 or disable player script)

        DisplayNextLine();
    }

    private void DisplayNextLine()
    {
        if (_currentIndex < _lines.Length)
        {
            dialogueText.text = _lines[_currentIndex];
            _currentIndex++;
        }
        else
        {
            EndDialogue();
        }
    }

    private void EndDialogue()
    {
        _isActive = false;
        dialogueCanvas.SetActive(false);
        
        // Optional: Unfreeze player movement here
    }
}

