using System.Collections;
using System.Collections.Generic;
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
    
    public void isDrifting(Transform playerCar)
    {
        carMovementController.isDrifting = true;
        
        //Sets line between the car and the pole
        lr.SetPosition(0, transform.position);
        lr.SetPosition(1, playerCar.transform.position);
    }    

    private void OnTriggerEnter2D(Collider2D other) 
    {
        isTriggered = true;
    }
    
    private void OnTriggerExit2D(Collider2D other) 
    {
        isTriggered = false;     
        lr.positionCount = 0;
    }
}
