using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private Canvas gameOverCanvas;
    [SerializeField] private string mainMenuScene = "Scene1";

    private void Awake()
    {
        if (gameOverCanvas == null)
        {
            gameOverCanvas = GetComponent<Canvas>();
        }

        if (gameOverCanvas == null)
        {
            Debug.LogError("GameOverScreen requires a Canvas component.", this);
        }
        else
        {
            gameOverCanvas.enabled = false;
        }
    }

    public void ShowAfter(float delay)
    {
        StartCoroutine(ShowAfterDelay(delay));
    }

    public void Show()
    {
        if (gameOverCanvas == null)
        {
            return;
        }

        gameOverCanvas.enabled = true;
        Time.timeScale = 0f;
    }

    public void RestartFight()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenuScene);
    }

    private IEnumerator ShowAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Show();
    }
}
