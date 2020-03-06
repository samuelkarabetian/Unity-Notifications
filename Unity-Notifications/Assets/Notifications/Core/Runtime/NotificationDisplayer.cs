using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Notifications
{
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

        public void DisplayNotification(Notification notification)
        {
            StartCoroutine(DisplayNotificationCoroutine(notification));
        }

        private IEnumerator DisplayNotificationCoroutine(Notification notification)
        {
            yield return new WaitForSeconds(notification._delayInSeconds);

            var notificationGO = new GameObject(notification.name);
            notificationGO.transform.SetParent(_canvas.transform);
            notificationGO.layer = LayerMask.NameToLayer("UI");
            notificationGO.transform.localPosition = Vector3.zero;
            var notificationRectTransform = notificationGO.AddComponent<RectTransform>();
            notificationRectTransform.anchorMin = Vector2.zero;
            notificationRectTransform.anchorMax = Vector2.zero;
            notificationRectTransform.anchoredPosition = notification._normalizedPosition * _screenSize;

            var notificationText = notificationGO.AddComponent<Text>();
            notificationText.text = notification._text;
            notificationText.font = notification._style._font;
            notificationText.fontSize = notification._style._fontSize;
            notificationText.color = notification._style._textColor;
            notificationText.horizontalOverflow = HorizontalWrapMode.Overflow;
            notificationText.alignment = notification._style._textAnchor;

            if (notification._style._fadeInDuration > 0f || notification._style._fadeOutDuration > 0f)
            {
                var notificationCanvasGroup = notificationGO.AddComponent<CanvasGroup>();
                notificationCanvasGroup.alpha = 0f;
                yield return StartCoroutine(FadeNotificationCoroutine(notificationCanvasGroup, notification._style._fadeInDuration, 1f));

                yield return new WaitForSeconds(notification._style._duration);

                yield return StartCoroutine(FadeNotificationCoroutine(notificationCanvasGroup, notification._style._fadeOutDuration, 0f));
            }
            else
            {
                yield return new WaitForSeconds(notification._style._duration);
            }

            Destroy(notificationGO);
        }

        private IEnumerator FadeNotificationCoroutine(CanvasGroup notificationCanvasGroup, float fadeDuration, float targetAlpha)
        {
            var startTime = Time.time;
            var elapsedTime = 0f;
            var startAlpha = notificationCanvasGroup.alpha;

            while (elapsedTime < fadeDuration)
            {
                elapsedTime = Time.time - startTime;
                var percent = elapsedTime / fadeDuration;
                notificationCanvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, percent);
                yield return new WaitForEndOfFrame();
            }

            notificationCanvasGroup.alpha = targetAlpha;
        }
    }
}