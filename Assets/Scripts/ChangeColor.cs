using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Color startColor = Color.blue;   // Initial color
    public Color endColor = Color.red;       // Final color
    public float duration = 5.0f;            // Time it takes to transition between colors

    private float elapsedTime = 0.0f;

    private void Update()
    {
        // Update the elapsed time
        elapsedTime += Time.deltaTime;

        // Calculate the interpolation value based on elapsed time and duration
        float t = Mathf.Clamp01(elapsedTime / duration);

        // Interpolate between startColor and endColor
        Color currentColor = Color.Lerp(startColor, endColor, t);

        // Set the camera's background color
        Camera.main.backgroundColor = currentColor;

        // Reset elapsed time if the transition is complete
        if (t >= 1.0f)
        {
            elapsedTime = 0.0f;
            Color tempColor = startColor;
            startColor = endColor;
            endColor = tempColor;
        }
    }
}

