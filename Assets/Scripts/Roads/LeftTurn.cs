using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using System.Diagnostics.Tracing;

public class LeftTurn : MonoBehaviour
{
    [SerializeField] GameObject straightHorizantalLeft;
    public Vector3 position;


    GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        BuildRoad();
        
    }
    

    
    public void BuildRoad()
    {
        position = transform.Find(GameConstants.FinishPos).position;
        if(gameManager.counter < gameManager.builtRoadCounter)
        {
            GameObject road = Instantiate(straightHorizantalLeft, position, Quaternion.identity);
            gameManager.builtRoads.Add(road);
            gameManager.counter++;
        }
            
    }
}
