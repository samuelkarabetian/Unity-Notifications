using UnityEngine;

namespace Notifications
{
    [CreateAssetMenu(menuName = "Notifications/Notification Displayer Settings")]
    public class NotificationDisplayerSettings : ScriptableObject
    {
        [Header("Canvas Settings")]
        public Vector2 _referenceResolution = new Vector2(1920f, 1080f);
    }
}