using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    public float speed = 1f;
    public float rotationSensitivity = 3f;
    private float RotationSmoothFactor = 0.15f;    
    private float xChange;
    private float yChange;
    private Vector3 startingDirection = new Vector3(-2.5f,5.0f,-2.5f);
    private Vector3 startingRotation = new Vector3(60.0f, 45.0f, 0.0f);
    private Vector3 targetRotation;
    private Vector3 currentVelocity;
    private Vector3 currentRotation;
    private Vector3 forwardVelocity;
    private Vector3 sideVelocity;
    private Vector3 pos;
    private Rigidbody rb;
    private float forwardDirection;
    private float sideDirection;
    private float terrainSize = 5f;

    // Start is called before the first frame update
    void Start()
    {
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;

        // Get rigidbody and set starting position
        rb = GetComponent<Rigidbody>();
        rb.position = startingDirection;
        targetRotation = startingRotation;
    }

    
    // Update is called once per frame
    void Update(){
        getInput();
        processInput();
        move();          
        }

    void getInput(){
        // Getting Keyboard Inputs
        forwardDirection = Input.GetAxis("Vertical");
        sideDirection = Input.GetAxis("Horizontal");

        // Get Mouse Input
        xChange = Input.GetAxis("Mouse Y");
        yChange = Input.GetAxis("Mouse X");
        }

    void processInput(){
        // Calculate the directional velocity with speed and delta time respective to its direction
        forwardVelocity = transform.forward * forwardDirection * speed * Time.deltaTime;
        sideVelocity = transform.right * sideDirection * speed * Time.deltaTime;
        currentVelocity = forwardVelocity + sideVelocity;

        // Rotate based on sensitivity that is set
        targetRotation = new Vector3(
            targetRotation.x + xChange * rotationSensitivity,
            targetRotation.y + yChange * rotationSensitivity
            );  
        
        // Add in smooth factor to rotation
        currentRotation = Vector3.Lerp(currentRotation, 
            targetRotation, RotationSmoothFactor
            );
        }
    void move(){
        // Move the player
        rb.velocity = Vector3.zero;
        rb.position = rb.position + currentVelocity;
        
        // // Makes sure player doesn't go out of bound
        pos = rb.position;
        pos.x = Mathf.Clamp(pos.x, -terrainSize, terrainSize); 
        pos.z = Mathf.Clamp(pos.z, -terrainSize, terrainSize);   
        pos.y = Mathf.Clamp(pos.y, 0, Mathf.Infinity);
        rb.position = pos;

        // Rotates the player
        transform.rotation = Quaternion.Euler(currentRotation);
        }
}