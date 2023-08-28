using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTurn : MonoBehaviour
{
    [SerializeField] GameObject straightHorizantalRight;
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
            GameObject road = Instantiate(straightHorizantalRight, position, Quaternion.identity);
            gameManager.builtRoads.Add(road);
            gameManager.counter++;
        }
            
    }
}
