using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class NotificationDisplayer : MonoBehaviour
{
    [SerializeField] private NotificationDisplayerSettings _settings = null;
    private Canvas _canvas = null;
    private CanvasScaler _canvasScaler = null;
    private Vector2 _screenSize = Vector2.zero;

    private void Awake()
    {
        var canvasGO = new GameObject("Message Displayer Canvas");
        canvasGO.layer = LayerMask.NameToLayer("UI");

        _canvas = canvasGO.AddComponent<Canvas>();
        _canvas.renderMode = RenderMode.ScreenSpaceOverlay;

        _canvasScaler = canvasGO.AddComponent<CanvasScaler>();
        _canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        _canvasScaler.referenceResolution = _settings._referenceResolution;

        canvasGO.AddComponent<CanvasRenderer>();

        _screenSize = new Vector2(Screen.width, Screen.height);
    }

    public void DisplayMessage(Notification message)
    {
        StartCoroutine(DisplayMessageCoroutine(message));
    }

    private IEnumerator DisplayMessageCoroutine(Notification message) 
    {
        yield return new WaitForSeconds(message._delayInSeconds);

        var messageGO = new GameObject(message.name);
        messageGO.transform.SetParent(_canvas.transform);
        messageGO.layer = LayerMask.NameToLayer("UI");
        messageGO.transform.localPosition = Vector3.zero;
        var messageRectTransform = messageGO.AddComponent<RectTransform>();
        messageRectTransform.anchorMin = Vector2.zero;
        messageRectTransform.anchorMax = Vector2.zero;
        messageRectTransform.anchoredPosition = message._normalizedPosition * _screenSize;

        var messageText = messageGO.AddComponent<Text>();
        messageText.text = message._text;
        messageText.font = message._style._font;
        messageText.fontSize = message._style._fontSize;
        messageText.color = message._style._textColor;
        messageText.horizontalOverflow = HorizontalWrapMode.Overflow;

        if (message._style._fadeInDuration > 0f || message._style._fadeOutDuration > 0f)
        {
            var messageCanvasGroup = messageGO.AddComponent<CanvasGroup>();
            messageCanvasGroup.alpha = 0f;
            StartCoroutine(FadeMessageCoroutine(messageCanvasGroup, message._style._fadeInDuration, 1f));

            yield return new WaitForSeconds(message._style._duration);

            StartCoroutine(FadeMessageCoroutine(messageCanvasGroup, message._style._fadeOutDuration, 0f));
        }
    }

    private IEnumerator FadeMessageCoroutine(CanvasGroup messageCanvasGroup, float fadeDuration, float targetAlpha) 
    {
        var startTime = Time.time;
        var elapsedTime = 0f;
        var startAlpha = messageCanvasGroup.alpha;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime = Time.time - startTime;
            var percent = elapsedTime / fadeDuration;
            messageCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, percent);
            yield return new WaitForEndOfFrame();
        }

        messageCanvasGroup.alpha = targetAlpha;
    }
}