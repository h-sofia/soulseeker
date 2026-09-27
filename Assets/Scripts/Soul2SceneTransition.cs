using UnityEngine;
using UnityEngine.SceneManagement;

public class Soul2SceneTransition : MonoBehaviour
{
    [SerializeField] private string nextScene = "Scene3";

    private bool isTransitioning;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (isTransitioning || !other.transform.root.CompareTag("Player"))
        {
            return;
        }

        isTransitioning = true;
        SceneManager.LoadScene(nextScene);
    }
}
