using UnityEngine;

[CreateAssetMenu(menuName = "Notifications/Notification")]
public class Notification : ScriptableObject
{
    [TextArea(5, 100)]
    public string _text = "Insert text here";
    public float _delayInSeconds = 0f;
    public Vector2 _normalizedPosition = Vector2.one * 0.5f;
    public NotificationStyle _style = null;
}