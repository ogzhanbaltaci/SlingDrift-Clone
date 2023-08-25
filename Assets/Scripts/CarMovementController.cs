using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarMovementController : MonoBehaviour
{
    [SerializeField] float carSpeed = 10f;
    public DriftPoleController driftPoleController;
    
    public bool isTriggered;
    void Awake() 
    {
        //driftPoleController =  FindObjectOfType<DriftPoleController>();    
    }
    
    void Start()
    {
        
    }

    
    void Update() 
    {
        Move();
        CheckInput();
    }

    void Move()
    {
        transform.Translate(transform.up * (Time.deltaTime * carSpeed), Space.World);
    }

    public void CheckInput()
    {
        if(Input.GetMouseButton(0))
        {
            if(isTriggered)
            {
                driftPoleController.lr.positionCount = 2;
                driftPoleController.isCall(transform);
            }
        }
        else if(isTriggered == true)
        {
            driftPoleController.lr.positionCount = 0; 
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        isTriggered = true;
        driftPoleController = other.gameObject.GetComponent<DriftPoleController>();
        if(other.gameObject.tag == ("roadLeft"))
        {
            driftPoleController.turningAngle = 1;
            Debug.Log(driftPoleController.turningAngle);
        }
        else if(other.gameObject.tag == ("roadRight"))
        {
            driftPoleController.turningAngle = -1;
            Debug.Log(driftPoleController.turningAngle);
        }
        Debug.Log("girdi");      
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        isTriggered = false;  
        driftPoleController.lr.positionCount = 0; 
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        SceneManager.LoadScene(0);
    }

}
