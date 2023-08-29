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
        Debug.Log(builtRoadCounter);
        if(builtRoads.Count > 0 && builtRoadCounter < builtRoads.Count)
            lastRoad = builtRoads[builtRoadCounter];

        levelText.text = carMovementController.roadSeen.ToString();
        if(carMovementController.roadSeen * 2 + 1 >= builtRoadCounter - 8 && builtRoadCounter < builtRoads.Count)
        {
            if(lastRoad.tag == GameConstants.LeftTurn)
            {
                
                builtRoads[builtRoadCounter].GetComponent<LeftAndRightRoad>().BuildRoad(straightHorizantalLeft);
                builtRoads.Add(straightHorizantalLeft);
                builtRoadCounter++;
            }
            else if(lastRoad.tag == GameConstants.RightTurn)
            {
                
                builtRoads[builtRoadCounter].GetComponent<LeftAndRightRoad>().BuildRoad(straightHorizantalRight);
                builtRoads.Add(straightHorizantalRight);
                builtRoadCounter++;
                
            }
            else if(lastRoad.tag == GameConstants.RightTurn90 || lastRoad.tag == GameConstants.LeftTurn90)
            {
                
                builtRoads[builtRoadCounter].GetComponent<LeftAndRightRoad>().BuildRoad(straightVerticalUp);
                builtRoadCounter++;
                
            }
            else if(lastRoad.tag == GameConstants.StraightLeft)
            {
                
                
                builtRoads.Add(builtRoads[builtRoadCounter].GetComponent<StraightRoad>().BuildRoad(afterStraightLeftRoads));
                builtRoadCounter++;
                
            }
            else if(lastRoad.tag == GameConstants.StraightRight)
            {
                
                //builtRoads[builtRoadCounter-1].GetComponent<StraightRoad>().BuildRoad(afterStraightRightRoads);
                builtRoads.Add(builtRoads[builtRoadCounter].GetComponent<StraightRoad>().BuildRoad(afterStraightRightRoads));
                builtRoadCounter++;
                
            }
            else
            {
                
                Debug.Log("Girdi" + builtRoadCounter);
                //builtRoads[builtRoadCounter-1].GetComponent<StraightRoad>().BuildRoad(afterStraightUpRoads);
                builtRoads.Add(builtRoads[builtRoadCounter].GetComponent<StraightRoad>().BuildRoad(afterStraightUpRoads));
                builtRoadCounter++;
                
            }
            if(carMovementController.isLevelUp == true)
            {
                
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
