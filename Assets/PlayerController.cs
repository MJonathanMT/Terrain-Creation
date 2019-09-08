using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float speed = 15f;

    public float MoveSmoothFactor = 0.75f;
    public float RotationSmoothFactor = 0.15f;


    public float rotationSensitivity = 3f;
    
    private float rotationX = 0.0f;
    private float rotationY = 0.0f;

    private Vector3 facingDirection;
    private Vector3 targetRotation;
    private Vector3 currentVelocity;
    private Vector3 currentRotation;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Screen.lockCursor = true;
    }

    
    // Update is called once per frame
    void Update()
        {
            // normalized just incase multiple buttons pressed
            facingDirection = new Vector3(
                Input.GetAxis("Horizontal"),
                0,
                Input.GetAxis("Vertical")
            ).normalized;
 
            float xChange = Input.GetAxis("Mouse Y");
            float yChange = Input.GetAxis("Mouse X");

            targetRotation = new Vector3(
                targetRotation.x + xChange * rotationSensitivity,
                targetRotation.y + yChange * rotationSensitivity
                );
            
            currentVelocity = Vector3.Lerp(currentVelocity, facingDirection * speed * Time.deltaTime, MoveSmoothFactor);
            transform.Translate (currentVelocity);

            currentRotation = Vector3.Lerp(currentRotation, targetRotation, RotationSmoothFactor);
            transform.rotation = Quaternion.Euler(currentRotation);
            //("rotationX" and "rotationY" are public variables)            
            rotationX += xChange * rotationSensitivity * Time.deltaTime;
            rotationY -= yChange * rotationSensitivity * Time.deltaTime;
               }
}