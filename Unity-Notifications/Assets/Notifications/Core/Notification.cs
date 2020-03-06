using UnityEngine;

namespace Notifications
{
    [CreateAssetMenu(menuName = "Notifications/Notification")]
    public class Notification : ScriptableObject
    {
        [TextArea(5, 100)]
        public string _text = "Insert text here";
        [Tooltip("How many seconds this notification will be delayed before being displayed.")]
        public float _delay = 0f;
        [Tooltip("How many seconds this notification will be displayed.")]
        [Range(0f, 120f)] public float _duration = 5f;
        public Vector2 _normalizedPosition = Vector2.one * 0.5f;
        public NotificationStyle _style = null;
    }
}