using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CarMovementController : MonoBehaviour
{
    [SerializeField] float carSpeed = 10f;
    [SerializeField] ParticleSystem crashEffect;
    SpriteRenderer carSprite;
    public DriftPoleController driftPoleController;
    public GameManager gameManager;
    public Rigidbody2D rb;
    public int roadSeen = 0;
    public bool isTriggered;
    public bool isDrifting;
    public bool isCrashed;
    int levelUpPoints = 5;
    
    
    [Header("Car settings")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 30;

    //Local variables
    float accelerationInput = 1;
    float steeringInput = 0;

    float rotationAngle = 0;

    float velocityVsUp = 0;
    float crashForce = 100f;



    void Awake() 
    {
        //driftPoleController =  FindObjectOfType<DriftPoleController>();   
        carSprite = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {

    }

    void Update() 
    {
        //Move();
        //CheckInput();
        if(roadSeen >= levelUpPoints)
        {
            maxSpeed += 1;
            levelUpPoints += 5; 
        }
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
                isDrifting = true;
                driftPoleController.lr.positionCount = 2;
                driftPoleController.isCall(transform);
                ApplySteering();
                
                //Vector3 driftForceVector = transform.forward * 1 * driftForce;

                //rb.AddForce(driftForceVector, ForceMode2D.Force);
            }
        }
        else if(isTriggered == true)
        {
            driftPoleController.lr.positionCount = 0; 
            isDrifting = false;
            //Debug.Log(gameManager.builtRoads[roadSeen].transform.position);
            //transform.LookAt(gameManager.builtRoads[roadSeen].transform.position * Time.deltaTime);
            //Quaternion targetRotation = Quaternion.LookRotation(gameManager.builtRoads[roadSeen].transform.position - transform.position);

            // Yavaşça dönüş yap
            //transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 1f * Time.deltaTime);
            
  
        }
        else
        {
            isDrifting = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other) 
    { 
        isTriggered = true;
        driftPoleController = other.gameObject.GetComponent<DriftPoleController>();
        if(other.gameObject.tag == ("roadLeft"))
        {
            driftPoleController.turningAngle = -1;
        }
        else if(other.gameObject.tag == ("roadRight"))
        {
            driftPoleController.turningAngle = 1;
        }     
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        roadSeen++;
        isTriggered = false;  
        driftPoleController.lr.positionCount = 0; 
        /*Debug.Log(gameManager.builtRoads[roadSeen * 2].transform.position);
          Vector3 direction = gameManager.builtRoads[roadSeen * 2].transform.position;
          Debug.Log(transform.position);
          Debug.Log(direction);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Debug.Log(angle);
        // Z rotasyonunu hesaplayarak nesneyi döndür
        transform.rotation = Quaternion.Euler(0, 0, angle);*/
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        gameManager.EnableRetryCanvas();
        isCrashed = true;
        //rb.AddForce(Vector2.zero, ForceMode2D.Impulse);
        //rb.AddForce(-other.contacts[0].normal * crashForce, ForceMode2D.Impulse);
        carSprite.color = Color.black;
        Vector3 stoppingDirection = -other.contacts[0].normal;

           
        rb.AddForceAtPosition(stoppingDirection * crashForce, other.contacts[0].point, ForceMode2D.Impulse);
        crashEffect.Play();
        
    }




    void FixedUpdate()
    {
        if(!isCrashed)
        {
            ApplyEngineForce();

            KillOrthogonalVelocity();

            CheckInput();
        }
    }

    void ApplyEngineForce()
    {
        velocityVsUp = Vector2.Dot(transform.up, rb.velocity);
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;

        //Create a force for the engine
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        //Apply force and pushes the car forward
        rb.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {

        //Update the rotation angle based on input
        rotationAngle -= driftPoleController.turningAngle * turnFactor ;

        //Apply steering by rotating the car object
        rb.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        //Get forward and right velocity of the car
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);

        //Kill the orthogonal velocity (side velocity) based on how much the car should drift. 
        rb.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    float GetLateralVelocity()
    {
        //Returns how how fast the car is moving sideways. 
        return Vector2.Dot(transform.right, rb.velocity);
    }


    public float GetVelocityMagnitude()
    {
        return rb.velocity.magnitude;
    }

}
