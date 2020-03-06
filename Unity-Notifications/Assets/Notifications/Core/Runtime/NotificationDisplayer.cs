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

            var notificationGameObject = new GameObject(notification.name);
            notificationGameObject.transform.SetParent(_canvas.transform);
            notificationGameObject.layer = LayerMask.NameToLayer("UI");
            notificationGameObject.transform.localPosition = Vector3.zero;
            var notificationRectTransform = notificationGameObject.AddComponent<RectTransform>();
            notificationRectTransform.anchorMin = Vector2.zero;
            notificationRectTransform.anchorMax = Vector2.zero;
            notificationRectTransform.anchoredPosition = notification._normalizedPosition * _screenSize;

            AddNotificationBackgroundImage(notification, notificationGameObject);

            AddNotificationText(notification, notificationGameObject);

            if (notification._style._fadeInDuration > 0f || notification._style._fadeOutDuration > 0f)
            {
                var notificationCanvasGroup = notificationGameObject.AddComponent<CanvasGroup>();
                notificationCanvasGroup.alpha = 0f;
                yield return StartCoroutine(FadeNotificationCoroutine(notificationCanvasGroup, notification._style._fadeInDuration, 1f));

                yield return new WaitForSeconds(notification._style._duration);

                yield return StartCoroutine(FadeNotificationCoroutine(notificationCanvasGroup, notification._style._fadeOutDuration, 0f));
            }
            else
            {
                yield return new WaitForSeconds(notification._style._duration);
            }

            Destroy(notificationGameObject);
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

        private void AddNotificationBackgroundImage(Notification notification, GameObject notificationGameObject)
        {
            if (notification._style._backgroundSprite == null) return;

            var backgroundGameObject = new GameObject("NotificationBackgroundImage");
            backgroundGameObject.transform.SetParent(notificationGameObject.transform, true);
            backgroundGameObject.transform.localPosition = Vector3.zero;
            backgroundGameObject.transform.localScale = Vector3.one;

            var rectTransform = backgroundGameObject.AddComponent<RectTransform>();
            rectTransform.sizeDelta = notification._style._size;

            var backgroundImage = backgroundGameObject.AddComponent<Image>();
            backgroundImage.sprite = notification._style._backgroundSprite;
        }

        private void AddNotificationText(Notification notification, GameObject notificationGameObject)
        {
            var textGameObject = new GameObject("NotificationText");
            textGameObject.transform.SetParent(notificationGameObject.transform, true);
            textGameObject.transform.localPosition = Vector3.zero;
            textGameObject.transform.localScale = Vector3.one;

            var rectTransform = textGameObject.AddComponent<RectTransform>();
            rectTransform.sizeDelta = notification._style._size;

            var notificationText = textGameObject.AddComponent<Text>();
            notificationText.text = notification._text;
            notificationText.font = notification._style._font;
            notificationText.fontSize = notification._style._fontSize;
            notificationText.color = notification._style._textColor;
            notificationText.horizontalOverflow = HorizontalWrapMode.Overflow;
            notificationText.alignment = notification._style._textAnchor;
        }
    }
}