using System.Collections;
using UnityEngine;

public class ScreenShake : MonoBehaviour
{
    /// <summary>
    /// Performs a shake effect over a specified duration.
    /// </summary>
    /// <returns>An enumerator for the shake effect.</returns>
    public IEnumerator Shake(float duration, AnimationCurve animationCurve)
    {
        Vector3 startPosition = GetStartPosition();
        float elapsedTime = 0.0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            float strength = animationCurve.Evaluate(elapsedTime / duration);
            ApplyOffset(startPosition, strength);
            yield return null;
        }

        ResetPosition(startPosition);
    }

    /// <summary>
    /// Applies the offset to the transform based on the starting position and strength.
    /// </summary>
    /// <param name="startPosition">The starting position of the transform.</param>
    /// <param name="strength">The strength of the shake.</param>
    private void ApplyOffset(Vector3 startPosition, float strength)
    {
        transform.localPosition = startPosition + Random.insideUnitSphere * strength;
    }

    /// <summary>
    /// Retrieves the starting position of the transform.
    /// </summary>
    /// <returns>The starting position of the transform.</returns>
    private Vector3 GetStartPosition()
    {
        return transform.localPosition;
    }

    /// <summary>
    /// Resets the position of the transform to the starting position.
    /// </summary>
    /// <param name="startPosition">The starting position of the transform.</param>
    private void ResetPosition(Vector3 startPosition)
    {
        transform.localPosition = startPosition;
    }
}