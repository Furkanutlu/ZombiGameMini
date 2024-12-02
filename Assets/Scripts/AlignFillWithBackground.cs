using UnityEngine;

public class AlignFillWithBackground : MonoBehaviour
{
    public RectTransform fillRect; // Fill objesi
    public RectTransform fillAreaRect; // Fill Area objesi
    public RectTransform backgroundRect; // Background objesi

    void Start()
    {
        AlignFill();
    }

    void AlignFill()
    {
        if (fillRect == null || backgroundRect == null || fillAreaRect == null)
        {
            Debug.LogError("Fill, Fill Area veya Background referanslarý eksik!");
            return;
        }

        // Fill Area'yý Background ile hizala
        fillAreaRect.anchorMin = new Vector2(0, 0);
        fillAreaRect.anchorMax = new Vector2(1, 1);
        fillAreaRect.offsetMin = Vector2.zero;
        fillAreaRect.offsetMax = Vector2.zero;

        // Fill'i Fill Area'ya tam oturt
        fillRect.anchorMin = new Vector2(0, 0);
        fillRect.anchorMax = new Vector2(1, 1);
        fillRect.offsetMin = Vector2.zero;
        fillRect.offsetMax = Vector2.zero;

        Debug.Log("Fill ve Fill Area baþarýyla Background ile hizalandý!");
    }
}
