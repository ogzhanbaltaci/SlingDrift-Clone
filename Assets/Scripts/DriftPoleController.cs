using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using UnityEngine;

public class DriftPoleController : MonoBehaviour
{
    [SerializeField] Transform rope;
    
    public bool isTriggered;
    public bool ropeAttached;
    public int turningAngle;

    public LineRenderer lr;
    CarMovementController carMovementController;
    

    void Start()
    {
        carMovementController = FindObjectOfType<CarMovementController>();
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
        //playerCar.LookAt(transform.position.z);
       // playerCar.transform.RotateAround(transform.position, transform.forward * turningAngle, Time.deltaTime * 90f);
        carMovementController.isDrifting = true;
        //playerCar.transform.RotateAround(transform.position, transform.forward, Time.deltaTime * turningAngle * 50);
        //playerCar.Rotate(0,0,Time.deltaTime * 140 * turningAngle);
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, playerCar.transform.position);

    }    
    
    
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        isTriggered = true;
        
        //other.transform.RotateAround(transform.position, transform.up, Time.deltaTime * 50f);
    }
    
    private void OnTriggerExit2D(Collider2D other) 
    {
        isTriggered = false;     
        lr.positionCount = 0;

    }
}
