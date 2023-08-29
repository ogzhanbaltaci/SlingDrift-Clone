using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftAndRightRoad : MonoBehaviour
{
    public Vector3 position;


    public GameManagerExperimental gameManagerExperimental;
    void Start()
    {
        gameManagerExperimental = FindObjectOfType<GameManagerExperimental>();     
        position = transform.Find(GameConstants.FinishPos).position;   
    }


    
    public void BuildRoad(GameObject road) 
    {
        
        GameObject pickedRoad = road;
        //gameManagerExperimental.builtRoads.Add(pickedRoad);
        Instantiate(pickedRoad, position, Quaternion.identity);
        
    
            
    }
}
