using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
		
	}
	
	// Update is called once per frame
	void Update () 
    {
		getMovementInput();
	}

    void getMovementInput()
    {
        if (Input.GetKey(KeyCode.W))
            transform.position += new Vector3(0, .1f);
        if (Input.GetKey(KeyCode.A))
            transform.position -= new Vector3(.1f, 0);
        if (Input.GetKey(KeyCode.S))
            transform.position -= new Vector3(0, .1f);
        if (Input.GetKey(KeyCode.D))
            transform.position += new Vector3(.1f, 0);
    }

}
