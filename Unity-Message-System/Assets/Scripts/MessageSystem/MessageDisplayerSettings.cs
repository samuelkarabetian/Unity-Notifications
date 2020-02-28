using UnityEngine;

[CreateAssetMenu(menuName = "Message Displayer/Message Displayer Settings")]
public class MessageDisplayerSettings : ScriptableObject
{
    [Header("Canvas Settings")]
    public Vector2 _referenceResolution = new Vector2(1920f, 1080f);
}