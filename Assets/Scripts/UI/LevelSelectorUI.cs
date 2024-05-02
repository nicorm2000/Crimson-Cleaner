using UnityEngine;
using UnityEngine.UI;

public class LevelSelectorUI : MonoBehaviour
{
    public RectTransform bounds;
    public Image cursorImage;

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 realCursorPosition = Input.mousePosition;

        Vector3 clampedCursorPosition = ClampToBoundaries(realCursorPosition);

        cursorImage.rectTransform.position = clampedCursorPosition;
    }

    private Vector3 ClampToBoundaries(Vector3 position)
    {
        Vector2 localCursor;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(bounds, position, null, out localCursor);

        Vector2 clampedLocalCursor = new Vector2(
            Mathf.Clamp(localCursor.x, bounds.rect.xMin, bounds.rect.xMax),
            Mathf.Clamp(localCursor.y, bounds.rect.yMin, bounds.rect.yMax)
        );

        return bounds.TransformPoint(clampedLocalCursor);
    }
}