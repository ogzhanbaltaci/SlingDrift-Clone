using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;



public class StraightUp : MonoBehaviour
{
    [SerializeField] List<GameObject> roads = new List<GameObject>();
    public Vector3 position;
    
    GameManager gameManager;
    Random random = new Random();

    
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        BuildRoad();
    }

    
    public void BuildRoad()
    {
        int count = roads.Count;
        int index = random.Next(count);
        GameObject pickedRoad = roads[index];
        position = transform.Find(GameConstants.FinishPos).position;
        if(gameManager.counter < gameManager.builtRoadCounter)
        {
            GameObject road = Instantiate(pickedRoad, position, Quaternion.identity);
            gameManager.builtRoads.Add(road);
            gameManager.counter++;
            gameManager.levelCounter++;
        }
            
    }
}
