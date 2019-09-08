using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightingScript : MonoBehaviour
{
    public float spinSpeed;
    public float moveSpeed;
    public float dayLength;
    public float rotationSpeed;
	private int dir = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.localRotation *= Quaternion.AngleAxis(Time.deltaTime * spinSpeed, new Vector3(0.0f, 0.0f, -1.0f));
        //rotationSpeed = Time.deltaTime / dayLength;
      
        //transform.Rotate(0, rotationSpeed ,0);
 
    }
}
