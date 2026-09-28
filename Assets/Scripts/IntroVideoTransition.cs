using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

[RequireComponent(typeof(VideoPlayer))]
public class IntroVideoTransition : MonoBehaviour
{
    [SerializeField] private string nextScene = "Scene1";

    private VideoPlayer videoPlayer;
    private bool transitionStarted;

    private void Awake()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.isLooping = false;
        videoPlayer.loopPointReached += OnVideoFinished;
    }

    private void Start()
    {
        if (!videoPlayer.isPlaying)
        {
            videoPlayer.Play();
        }
    }

    private void OnDestroy()
    {
        if (videoPlayer != null)
        {
            videoPlayer.loopPointReached -= OnVideoFinished;
        }
    }

    private void OnVideoFinished(VideoPlayer source)
    {
        if (transitionStarted)
        {
            return;
        }

        transitionStarted = true;

        if (!Application.CanStreamedLevelBeLoaded(nextScene))
        {
            Debug.LogError("Intro video finished, but scene '" + nextScene + "' is not enabled in Build Settings.", this);
            return;
        }

        SceneManager.LoadScene(nextScene);
    }
}
