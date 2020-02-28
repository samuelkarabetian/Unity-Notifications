using UnityEngine;

[CreateAssetMenu(menuName = "Notifications/Notification Style Settings")]
public class NotificationStyle : ScriptableObject
{
    public Font _font = null;
    public int _fontSize = 16;
    public Color _textColor = Color.black;
    public bool _hasDuration = true;
    public TextAnchor _textAnchor = TextAnchor.MiddleCenter;
    [Tooltip("Determines how long this notification will be displayed.")]
    [Range(0f, 15f)] public float _duration = 5f;
    [Tooltip("Determines how long it will take for the notification to fade in.")]
    [Range(0f, 3f)] public float _fadeInDuration = 0f;
    [Tooltip("Determines how long it will take for the notification to fade out.")]
    [Range(0f, 3f)] public float _fadeOutDuration = 0f;
}