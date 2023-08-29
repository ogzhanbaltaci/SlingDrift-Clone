using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class StraightRoad : MonoBehaviour
{
    public Vector3 position;
    GameManagerExperimental gameManagerExperimental;
    Random random = new Random();
    void Start()
    {
        gameManagerExperimental = FindObjectOfType<GameManagerExperimental>();
    }

    public GameObject BuildRoad(List<GameObject> roads)
    {
        int count = roads.Count;
        int index = random.Next(count);
        GameObject pickedRoad = roads[index];
        position = transform.Find(GameConstants.FinishPos).position;
        
        
        Instantiate(pickedRoad, position, Quaternion.identity);
        //gameManagerExperimental.builtRoads.Add(pickedRoad);
        return pickedRoad;
            
        
            
    }
}
