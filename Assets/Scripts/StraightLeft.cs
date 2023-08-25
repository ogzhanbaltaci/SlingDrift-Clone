using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class StraightLeft : MonoBehaviour
{
    [SerializeField] List<GameObject> roads = new List<GameObject>();
    public Vector3 position;

    
    Random random = new Random();
    void Start()
    {
        int count = roads.Count;
        int index = random.Next(count);
        GameObject pickedRoad = roads[index];
        position = transform.Find("FinishPos").position;
        Instantiate(pickedRoad, position, Quaternion.identity); 
    }
}
