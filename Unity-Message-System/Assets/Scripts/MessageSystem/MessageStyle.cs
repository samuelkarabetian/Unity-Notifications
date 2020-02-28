using UnityEngine;

[CreateAssetMenu(menuName = "Message Displayer/Message Style Settings")]
public class MessageStyle : ScriptableObject
{
    public Font _font = null;
    public int _fontSize = 16;
    public Color _textColor = Color.black;
    public bool _hasDuration = true;
    [Range(0f, 10f)] public float _duration = 0f;
    [Range(0f, 3f)] public float _fadeInDuration = 0f;
    [Range(0f, 3f)] public float _fadeOutDuration = 0f;
}
