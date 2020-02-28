using UnityEngine;

[CreateAssetMenu(menuName = "Message Displayer/Message")]
public class Message : ScriptableObject
{
    [TextArea(5, 100)]
    public string _text = "Insert text here";
    public float _delayInSeconds = 0f;
    public Vector2 _normalizedPosition = Vector2.one * 0.5f;
    public MessageStyle _style = null;
}
