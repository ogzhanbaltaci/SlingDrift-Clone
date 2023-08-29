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
    

    public List<GameObject> builtRoads = new List<GameObject>();
    public int counter = 0;
    public int builtRoadCounter = 10;
    public int levelCounter = 0;
    public int levelUpCap = 12;

    CarMovementController carMovementController;
    GameObject lastRoad;

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
        UpdateBuiltRoadCounter();

        levelText.text = carMovementController.roadSeen.ToString();
        ContinueBuilding();

        if (carMovementController.isLevelUp)
        {
            StartCoroutine(WaitForTextDisappear());
        }
    }

    private void UpdateBuiltRoadCounter()
    {
        if (builtRoads.Count > 10)
            lastRoad = builtRoads[builtRoadCounter - 1]; //Finds the last roadPart on the list

        if (levelCounter > levelUpCap)
            levelCounter = 0;
    }

    private void ContinueBuilding() //Generates a new roadPart based on the shape of the last roadPart in the list
    {
        if (ShouldContinueBuilding())
        {
            if (lastRoad.tag == GameConstants.LeftTurn)
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter - 2].GetComponent<LeftTurn>().BuildRoad();
            }
            else if (lastRoad.tag == GameConstants.RightTurn)
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter - 2].GetComponent<RightTurn>().BuildRoad();
            }
            else if (lastRoad.tag == GameConstants.StraightLeft)
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter - 2].GetComponent<StraightLeft>().BuildRoad();
            }
            else if (lastRoad.tag == GameConstants.StraightRight)
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter - 2].GetComponent<StraightRight>().BuildRoad();
            }
            else
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter - 2].GetComponent<StraightUp>().BuildRoad();
            }
        }
        else if (levelCounter == levelUpCap)
        {
            carMovementController.maxSpeed += 1;
            if (lastRoad.tag == GameConstants.LeftTurn)
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter - 2].GetComponent<LeftTurn>().BuildLevelUpRoad();
            }
            else if (lastRoad.tag == GameConstants.RightTurn)
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter - 2].GetComponent<RightTurn>().BuildLevelUpRoad();
            }
        }
    }
    private bool ShouldContinueBuilding()
    {
        return carMovementController.roadSeen * 2 - 1 >= builtRoadCounter - 8 && levelCounter < levelUpCap;
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
        scoreText.text = "Your Score " + carMovementController.roadSeen.ToString();
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