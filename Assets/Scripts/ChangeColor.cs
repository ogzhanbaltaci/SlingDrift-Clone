using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    public Color[] colors = new Color[] { Color.cyan, Color.blue, Color.green, Color.red, Color.yellow };
    public float duration = 2.0f;            
    private int currentColorIndex = 0;
    private float elapsedTime = 0.0f;
    GameManager gameManager;
    CarMovementController carMovementController;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        carMovementController = FindObjectOfType<CarMovementController>();
        Camera.main.backgroundColor = colors[currentColorIndex];
    }

    private void Update()
    {
        if(carMovementController.isLevelUp)
        {
            //Changes the color of the background with a smooth transition between colors
            float t = elapsedTime += Time.deltaTime;
            Color currentColor = Color.Lerp(colors[currentColorIndex], colors[(currentColorIndex + 1) % colors.Length], t);
            Camera.main.backgroundColor = currentColor;

            if (t >= 1.0f)
            {
                elapsedTime = 0.0f;
                currentColorIndex = (currentColorIndex + 1) % colors.Length;
            }
        }
    }
}

