using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CarMovementController : MonoBehaviour
{
    [SerializeField] ParticleSystem crashEffect;

    SpriteRenderer carSprite;
    public DriftPoleController driftPoleController;
    public GameManager gameManager;
    public Rigidbody2D rb;
    public Transform targetLevelUp;
    public Transform target;
    
    public int roadSeen = 0;
    public bool isTriggered;
    public bool isDrifting;
    public bool isCrashed;
    public bool isLevelUp = false;

    [Header("Car settings")]
    public float driftFactor = 0.95f;
    public float accelerationFactor = 30.0f;
    public float turnFactor = 3.5f;
    public float maxSpeed = 30;

    float accelerationInput = 1;
    float rotationAngle = 0;
    float rotationSpeed = 10f; 
    float velocityVsUp = 0;
    float crashForce = 100f;

    void Awake() 
    {  
        carSprite = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManager>();
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Update() 
    {
        if(!isCrashed)
            CheckInput(); 
    }

    void FixedUpdate()
    {
        if(!isCrashed)
        {
            ApplyEngineForce();
            KillOrthogonalVelocity();
            //CheckInput(); //for pc build 
        }
        if(isLevelUp)
        {
            LevelUpRedirection();
        }
    }

    private void LevelUpRedirection() //Rotates the car so it can go straight on the levelUpRoads
    {

        Vector3 direction = targetLevelUp.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle - 90f);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    public void CheckInput()
    {
        if(Input.GetMouseButton(0))
        {
            if(isTriggered && isLevelUp == false)
            {
                isDrifting = true;
                driftPoleController.lr.positionCount = 2;
                driftPoleController.isDrifting(transform);
                ApplySteering();
            }
        }
        else if(isTriggered == true && isLevelUp == false)
        {
            driftPoleController.lr.positionCount = 0; 
            isDrifting = false;

            /*Vector3 direction = target.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angle - 90f);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);*/
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

        //Finds the turning angle so it can rotate the right way
        if(other.gameObject.tag == ("roadLeft"))
        {
            driftPoleController.turningAngle = -1;
        }
        else if(other.gameObject.tag == ("roadRight"))
        {
            driftPoleController.turningAngle = 1;
        }     
        else if(other.name == (GameConstants.LevelUpLeft) || //Finds the levelUpRoads position
                other.name == (GameConstants.LevelUpRight) ||
                other.name == (GameConstants.LevelUpUp))
        { 
            targetLevelUp = other.transform.Find(GameConstants.FinishPos);
            isLevelUp = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        roadSeen++;
        isTriggered = false;  
        isLevelUp = false;
    }
    private void OnCollisionEnter2D(Collision2D other) 
    {
        OnCrash();
        Vector3 stoppingDirection = -other.contacts[0].normal;
        rb.AddForceAtPosition(stoppingDirection * crashForce, other.contacts[0].point, ForceMode2D.Impulse);
    }

    void OnCrash() //When the car hits the collider this method gets called
    {
        gameManager.EnableRetryCanvas();
        isCrashed = true;
        isDrifting = false;
        carSprite.color = Color.black;
        driftPoleController.lr.positionCount = 0;
        crashEffect.Play();
    }

    void ApplyEngineForce()
    {
        //Prevents the car from going faster than maxSpeed
        velocityVsUp = Vector2.Dot(transform.up, rb.velocity);
        if (velocityVsUp > maxSpeed && accelerationInput > 0)
            return;

        //Create a force for the engine
        Vector2 engineForceVector = transform.up * accelerationInput * accelerationFactor;

        //Applies force that will move the car forward
        rb.AddForce(engineForceVector, ForceMode2D.Force);
    }

    void ApplySteering()
    {
        //Sets the drift direction according to the next incoming driftPole
        rotationAngle -= driftPoleController.turningAngle * turnFactor ;

        //Applies the angle so the car will drift
        rb.MoveRotation(rotationAngle);
    }

    void KillOrthogonalVelocity()
    {
        //Get forward and right velocity of the car
        Vector2 forwardVelocity = transform.up * Vector2.Dot(rb.velocity, transform.up);
        Vector2 rightVelocity = transform.right * Vector2.Dot(rb.velocity, transform.right);

        //The value is slightly reduced with the driftFactor to prevent the car from drifting too much.
        rb.velocity = forwardVelocity + rightVelocity * driftFactor;
    }

    public float GetVelocityMagnitude()
    {
        return rb.velocity.magnitude;
    }

}
