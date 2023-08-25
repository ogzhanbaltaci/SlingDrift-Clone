using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;

public class DriftPoleController : MonoBehaviour
{
    [SerializeField] 

    public bool isTriggered;
    public bool ropeAttached;
    
    public int turningAngle;
    
    [SerializeField] Transform rope;
    public LineRenderer lr;

    void Start()
    {
        lr = GetComponentInChildren<LineRenderer>();
        lr.positionCount = 2;
    }

    
    void Update()
    {
        
    }

    void isDrifting()
    {
        while(isTriggered && Input.GetMouseButton(0))
        {
            //other.transform.RotateAround(transform.position, transform.up, Time.deltaTime * 50f);
        }
    }
    
    public void isCall(Transform playerCar)
    {
        
        playerCar.transform.RotateAround(transform.position, transform.forward * turningAngle, Time.deltaTime * 90f);
        playerCar.Rotate(0,0,Time.deltaTime * 50 * turningAngle);
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, playerCar.transform.position);

    }    
    
    
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        isTriggered = true;
        Debug.Log(other.transform);
        
        
        //other.transform.RotateAround(transform.position, transform.up, Time.deltaTime * 50f);
    }
    
    private void OnTriggerExit2D(Collider2D other) 
    {
        isTriggered = false;     
        lr.positionCount = 0;

    }
}
