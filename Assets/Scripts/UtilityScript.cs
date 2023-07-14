using UnityEngine;
using TMPro;

public static class UtilityScript
{
    public const int sortingOrderDefault = 5000;

    public static TextMeshPro CreateWorldText(string text, Transform parent = null, Vector3 localPosition = default(Vector3), int fontSize = 40, Color? color = null, TextAlignmentOptions textAlignment = TextAlignmentOptions.Center, int sortingOrder = sortingOrderDefault)
    {
        if (color == null)
            color = Color.white;

        GameObject gameObject = new GameObject("World_Text", typeof(TextMeshPro));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;

        // Set the rotation to face the camera's forward direction
        transform.LookAt(Camera.main.transform);
        transform.Rotate(0, 180, 0); // Adjust the rotation to face upward

        TextMeshPro textMeshPro = gameObject.GetComponent<TextMeshPro>();
        textMeshPro.alignment = textAlignment;
        textMeshPro.text = text;
        textMeshPro.fontSize = fontSize;
        textMeshPro.color = (Color)color;
        textMeshPro.sortingOrder = sortingOrder;

        return textMeshPro;
    }
}