using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerExperimental : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] Canvas retryCanvas;
    [SerializeField] Canvas startCanvas;
    [SerializeField] Canvas inGameCanvas;
    [SerializeField] TextMeshProUGUI scoreText;
    public List<GameObject> builtRoads = new List<GameObject>();
    public List<GameObject> afterStraightLeftRoads = new List<GameObject>();
    public List<GameObject> afterStraightRightRoads = new List<GameObject>();
    public List<GameObject> afterStraightUpRoads = new List<GameObject>();
    public  GameObject straightHorizantalRight;
    public GameObject straightHorizantalLeft;
    public GameObject straightVerticalUp;

    
    public int counter = 0;
    public int builtRoadCounter = 0;
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
        if(builtRoads.Count > 0)
            lastRoad = builtRoads[builtRoadCounter];

        levelText.text = carMovementController.roadSeen.ToString();
        if(carMovementController.roadSeen * 2 + 1 >= builtRoadCounter - 8)
        {
            if(lastRoad.tag == GameConstants.LeftTurn)
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter-1].GetComponent<LeftAndRightRoad>().BuildRoad(straightHorizantalLeft);
                
            }
            else if(lastRoad.tag == GameConstants.RightTurn)
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter-1].GetComponent<LeftAndRightRoad>().BuildRoad(straightHorizantalRight);
                
            }
            else if(lastRoad.tag == GameConstants.RightTurn90 || lastRoad.tag == GameConstants.LeftTurn90)
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter-1].GetComponent<LeftAndRightRoad>().BuildRoad(straightVerticalUp);
                
            }
            else if(lastRoad.tag == GameConstants.StraightLeft)
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter-1].GetComponent<StraightRoad>().BuildRoad(afterStraightLeftRoads);
                
            }
            else if(lastRoad.tag == GameConstants.StraightRight)
            {
                builtRoadCounter++;
                builtRoads[builtRoadCounter-1].GetComponent<StraightRoad>().BuildRoad(afterStraightRightRoads);
                
            }
            else
            {
                builtRoadCounter++;
                Debug.Log("Girdi" + builtRoadCounter);
                builtRoads[builtRoadCounter-1].GetComponent<StraightRoad>().BuildRoad(afterStraightUpRoads);
                
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
