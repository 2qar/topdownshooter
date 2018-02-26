using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorManager : MonoBehaviour 
{
    public Camera mainCam;

	// Use this for initialization
	void Start () 
    {
        // Hide the mouse cursor
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        Vector3 mousePosition = Input.mousePosition;

        transform.position = mainCam.ScreenToWorldPoint(mousePosition);
        transform.position = new Vector3(transform.position.x, transform.position.y, -5);
	}
}
