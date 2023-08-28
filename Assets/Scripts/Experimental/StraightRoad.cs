using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class StraightRoad : MonoBehaviour
{
    public Vector3 position;
    GameManager gameManager;
    Random random = new Random();
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void BuildRoad(List<GameObject> roads)
    {
        int count = roads.Count;
        int index = random.Next(count);
        GameObject pickedRoad = roads[index];
        position = transform.Find(GameConstants.FinishPos).position;
        
        
        Instantiate(pickedRoad, position, Quaternion.identity);
        gameManager.builtRoads.Add(pickedRoad);
            
        
            
    }
}
