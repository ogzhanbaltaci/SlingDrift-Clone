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
    public List<GameObject> builtRoads = new List<GameObject>();
    public int counter = 0;
    public int builtRoadCounter = 10;
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
        if(builtRoads.Count > 9)
            lastRoad = builtRoads[builtRoadCounter-1];

        levelText.text = carMovementController.roadSeen.ToString();
        if(carMovementController.roadSeen * 2 >= builtRoadCounter - 8)
        {
            if(lastRoad.tag == GameConstants.LeftTurn)
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter-2].GetComponent<LeftTurn>().BuildRoad();
                
            }
            else if(lastRoad.tag == GameConstants.RightTurn)
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter-2].GetComponent<RightTurn>().BuildRoad();
                
            }
            else if(lastRoad.tag == GameConstants.StraightLeft)
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter-2].GetComponent<StraightLeft>().BuildRoad();
                
            }
            else if(lastRoad.tag == GameConstants.StraightRight)
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter-2].GetComponent<StraightRight>().BuildRoad();
                
            }
            else
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter-2].GetComponent<StraightUp>().BuildRoad();
                
            }

        }        
    }
    public void RetryLevel()
    {
        SceneManager.LoadScene(0);
    }

    public void EnableRetryCanvas()
    {
        retryCanvas.gameObject.SetActive(true);
        inGameCanvas.gameObject.SetActive(false);
        scoreText.text = "Your Score " + carMovementController.roadSeen.ToString();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        startCanvas.gameObject.SetActive(false);
        inGameCanvas.gameObject.SetActive(true);
    }


}
