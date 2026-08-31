using UnityEngine;

public class Bookshelf : MonoBehaviour, IInteractable
{
    [Header("Dialogue Content")]
    [SerializeField] private string objectName = "Bookshelf";
    [TextArea(3, 5)]
    [SerializeField] private string[] dialogueLines;

    [Header("UI Prompt")]
    [SerializeField] private GameObject promptPrefab;
    [SerializeField] private Vector3 promptOffset = new Vector3(0, 1.2f, 0);

    private GameObject _spawnedPrompt;

    private void Start()
    {
        if (promptPrefab != null)
        {
            _spawnedPrompt = Instantiate(promptPrefab, transform.position + promptOffset, Quaternion.identity, transform);
            _spawnedPrompt.SetActive(false);
        }
    }

    public void Interact()
    {
        HidePrompt(); 
        DialogueManager.Instance.StartDialogue(objectName, dialogueLines);
    }

    public void ShowPrompt()
    {
        if (_spawnedPrompt != null) _spawnedPrompt.SetActive(true);
    }

    public void HidePrompt()
    {
        if (_spawnedPrompt != null) _spawnedPrompt.SetActive(false);
    }
}
