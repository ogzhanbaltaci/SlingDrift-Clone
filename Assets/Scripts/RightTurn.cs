using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightTurn : MonoBehaviour
{
    [SerializeField] GameObject straightHorizantalRight;
    public Vector3 position;
    void Start()
    {
        position = transform.Find("FinishPos").position;
        Instantiate(straightHorizantalRight, position, Quaternion.identity); 
    }

    void Update()
    {
        
    }
}
