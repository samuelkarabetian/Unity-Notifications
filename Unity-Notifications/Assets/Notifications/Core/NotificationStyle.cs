using UnityEngine;

namespace Notifications
{
    [CreateAssetMenu(menuName = "Notifications/Notification Style Settings")]
    public class NotificationStyle : ScriptableObject
    {
        [Header("General Settings")]
        public Vector2 _size = new Vector2(200f, 80f);
        public Sprite _backgroundSprite = null;

        [Header("Text Settings")]
        public Font _font = null;
        public int _fontSize = 16;
        public Color _textColor = Color.black;
        public TextAnchor _textAnchor = TextAnchor.MiddleCenter;

        [Header("Display Settings")]
        [Tooltip("How many seconds it will take for the notification to fade in.")]
        [Range(0f, 5f)] public float _fadeInDuration = 0f;
        [Tooltip("How many seconds it will take for the notification to fade out.")]
        [Range(0f, 5f)] public float _fadeOutDuration = 0f;
    }
}