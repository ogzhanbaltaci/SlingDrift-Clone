using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftAndRightRoad : MonoBehaviour
{
    public Vector3 position;


    public GameManager gameManager;
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();     
        position = transform.Find(GameConstants.FinishPos).position;   
    }


    
    public void BuildRoad(GameObject road) 
    {
        
        GameObject pickedRoad = road;
        
        Instantiate(pickedRoad, position, Quaternion.identity);
        gameManager.builtRoads.Add(pickedRoad);
        
        
            
    }
}
