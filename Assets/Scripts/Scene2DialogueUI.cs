using UnityEngine;
using UnityEngine.InputSystem;

public class Scene2DialogueUI : MonoBehaviour
{
    private string message;
    private bool isOpen;

    public bool IsOpen => isOpen;

    public void ShowDialogue(string text)
    {
        message = text;
        isOpen = true;
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (isOpen && Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            CloseDialogue();
        }
    }

    private void OnGUI()
    {
        if (!isOpen)
        {
            return;
        }

        GUIStyle dialogueStyle = new GUIStyle(GUI.skin.label)
        {
            alignment = TextAnchor.UpperLeft,
            wordWrap = true,
            padding = new RectOffset(24, 24, 18, 18),
            fontSize = 18
        };

        Rect dialogueRect =
            new Rect(80f, Screen.height - 330f, Screen.width - 160f, 250f);

        GUI.Box(
            dialogueRect,
            GUIContent.none
        );
        GUI.Label(dialogueRect, message, dialogueStyle);
        GUI.Label(
            new Rect(Screen.width - 220f, Screen.height - 70f, 140f, 30f),
            "Press Escape to close"
        );
    }

    private void CloseDialogue()
    {
        isOpen = false;
        Time.timeScale = 1f;
    }

    private void OnDestroy()
    {
        Time.timeScale = 1f;
    }
}
