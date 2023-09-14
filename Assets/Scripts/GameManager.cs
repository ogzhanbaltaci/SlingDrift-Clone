using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Canvas retryCanvas;
    [SerializeField] Canvas startCanvas;
    [SerializeField] Canvas inGameCanvas;
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI levelUpText;

    CarMovementController carMovementController;

    void Awake() 
    {
        carMovementController = FindObjectOfType<CarMovementController>(); 
    }

    void Start()
    {
        Time.timeScale = 0; 
        inGameCanvas.gameObject.SetActive(false);
    }

    void Update()
    {
        levelText.text = carMovementController.roadSeen.ToString();

        if (carMovementController.isLevelUp)
        {
            carMovementController.IncreaseMaxSpeed(carMovementController.canSpeedUp);
            StartCoroutine(WaitForTextDisappear());
        }
    }

    private void EnableCanvas(Canvas canvasToShow, Canvas canvasToHide)
    {
        canvasToShow.gameObject.SetActive(true);
        canvasToHide.gameObject.SetActive(false);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void EnableRetryCanvas()
    {
        EnableCanvas(retryCanvas, inGameCanvas);
        string roadToString = carMovementController.roadSeen.ToString();
        scoreText.text = $"Your Score {roadToString}";
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        EnableCanvas(inGameCanvas, startCanvas);
    }
    
    IEnumerator WaitForTextDisappear()
    {
        levelUpText.text = "Level Up!";
        yield return new WaitForSeconds(2);
        levelUpText.text = "";
    }


}