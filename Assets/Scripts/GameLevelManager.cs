using System;
using System.Collections;
using System.Collections.Generic;
//using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;



public class GameLevelManager : MonoBehaviour
{
    /*[SerializeField] GameObject roadLeft;
    [SerializeField] GameObject roadRight;*/
    public Vector3 position;
    [SerializeField] List<GameObject> roads = new List<GameObject>();
    Vector3 worldPosition;
    Random random = new Random();
    
    void Start()
    {
        int count = roads.Count;
        int index = random.Next(count);
        GameObject pickedRoad = roads[index];
        position = transform.Find("FinishPos").position;
        //worldPosition = transform.TransformPoint(position);
        Instantiate(pickedRoad, position, Quaternion.identity); 
    }

    
    void Update()
    {
        
    }
}
