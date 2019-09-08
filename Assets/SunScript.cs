using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunScript : MonoBehaviour
{
    public float spinSpeed;
    public float moveSpeed;
	private int dir = 1;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localRotation *= Quaternion.AngleAxis(Time.deltaTime * spinSpeed, new Vector3(0.0f, 0.0f, -1.0f));
		//this.transform.localPosition += new Vector3(0.0f, 0.0f, Time.deltaTime * moveSpeed * dir);
		
		// if (this.transform.localPosition.z < 0.0f) {
		// 	dir = 1;
		// }
		// else if (this.transform.localPosition.z > 10.0f) {
		// 	dir = -1;
		// }
    }
}
