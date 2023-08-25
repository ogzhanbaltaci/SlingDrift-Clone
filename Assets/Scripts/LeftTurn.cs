using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class LeftTurn : MonoBehaviour
{
    [SerializeField] GameObject straightHorizantalLeft;
    public Vector3 position;
    Vector3 worldPosition;
    GameObject finishPos;
    void Start()
    {
        position = transform.Find("FinishPos").position;
        Instantiate(straightHorizantalLeft, position, Quaternion.identity); 
    }


    
    void Update()
    {
        
    }
}
