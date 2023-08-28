using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System.Diagnostics.Tracing;

public class LeftTurn : MonoBehaviour
{
    [SerializeField] GameObject straightRoad;
    [SerializeField] GameObject straightRoadFinish;
    public Vector3 position;


    GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        BuildRoad();
        
    }
    public void BuildLevelUpRoad()
    {
        position = transform.Find(GameConstants.FinishPos).position;
        if(gameManager.counter < gameManager.builtRoadCounter)
        {
            GameObject road = Instantiate(straightRoadFinish, position, Quaternion.identity);
            gameManager.builtRoads.Add(road);
            gameManager.counter++;
            gameManager.levelCounter++;
        }
    }

    
    public void BuildRoad()
    {
        position = transform.Find(GameConstants.FinishPos).position;
        if(gameManager.counter < gameManager.builtRoadCounter)
        {
            GameObject road = Instantiate(straightRoad, position, Quaternion.identity);
            gameManager.builtRoads.Add(road);
            gameManager.counter++;
            gameManager.levelCounter++;
        }
            
    }
}
