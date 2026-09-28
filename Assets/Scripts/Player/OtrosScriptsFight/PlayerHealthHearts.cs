using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(PlayerHealth))]
public class PlayerHealthHearts : MonoBehaviour
{
    [SerializeField] private Sprite heartSprite;
    [SerializeField] private int heartCount = 5;
    [SerializeField] private float heartSize = 56f;
    [SerializeField] private float spacing = 8f;
    [SerializeField] private Vector2 screenPadding = new Vector2(32f, 32f);

    private PlayerHealth playerHealth;
    private Image[] heartImages;
    private GameObject heartsCanvas;

    private void Awake()
    {
        playerHealth = GetComponent<PlayerHealth>();

        if (heartSprite == null)
        {
            Debug.LogError("PlayerHealthHearts requires a heart sprite.", this);
            enabled = false;
            return;
        }

        if (heartCount <= 0 || heartSize <= 0f)
        {
            Debug.LogError("PlayerHealthHearts requires a positive heart count and size.", this);
            enabled = false;
            return;
        }

        CreateHeartsCanvas();
    }

    private void Update()
    {
        int maxHealth = playerHealth.maxHealth;
        if (maxHealth <= 0)
        {
            Debug.LogError("PlayerHealth.maxHealth must be greater than zero to show health hearts.", this);
            enabled = false;
            return;
        }

        float healthRatio = Mathf.Clamp01((float)playerHealth.health / maxHealth);
        int visibleHeartCount = Mathf.CeilToInt(healthRatio * heartCount);

        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = i < visibleHeartCount;
        }
    }

    private void OnDestroy()
    {
        if (heartsCanvas != null)
        {
            Destroy(heartsCanvas);
        }
    }

    private void CreateHeartsCanvas()
    {
        heartsCanvas = new GameObject("PlayerHealthHeartsCanvas", typeof(RectTransform), typeof(Canvas), typeof(CanvasScaler));

        Canvas canvas = heartsCanvas.GetComponent<Canvas>();
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.sortingOrder = 20;

        CanvasScaler scaler = heartsCanvas.GetComponent<CanvasScaler>();
        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920f, 1080f);
        scaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        scaler.matchWidthOrHeight = 0.5f;

        GameObject row = new GameObject("Hearts", typeof(RectTransform));
        RectTransform rowTransform = row.GetComponent<RectTransform>();
        rowTransform.SetParent(heartsCanvas.transform, false);
        rowTransform.anchorMin = Vector2.one;
        rowTransform.anchorMax = Vector2.one;
        rowTransform.pivot = Vector2.one;
        rowTransform.anchoredPosition = new Vector2(-screenPadding.x, -screenPadding.y);
        rowTransform.sizeDelta = new Vector2(
            heartCount * heartSize + (heartCount - 1) * spacing,
            heartSize
        );

        heartImages = new Image[heartCount];
        for (int i = 0; i < heartCount; i++)
        {
            GameObject heart = new GameObject("Heart " + (i + 1), typeof(RectTransform), typeof(CanvasRenderer), typeof(Image));
            RectTransform heartTransform = heart.GetComponent<RectTransform>();
            heartTransform.SetParent(rowTransform, false);
            heartTransform.anchorMin = new Vector2(0f, 0.5f);
            heartTransform.anchorMax = new Vector2(0f, 0.5f);
            heartTransform.pivot = new Vector2(0f, 0.5f);
            heartTransform.anchoredPosition = new Vector2(i * (heartSize + spacing), 0f);
            heartTransform.sizeDelta = new Vector2(heartSize, heartSize);

            Image image = heart.GetComponent<Image>();
            image.sprite = heartSprite;
            image.preserveAspect = true;
            image.raycastTarget = false;
            heartImages[i] = image;
        }
    }
}
