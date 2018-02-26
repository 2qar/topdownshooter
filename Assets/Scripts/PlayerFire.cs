using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour 
{
    // The player's crosshair
    public GameObject cursor;

    // Bullet to fire
    public GameObject bullet;

    public GameObject testRotater;

	// Update is called once per frame
	void Update () 
    {
        Vector2 playerPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 crosshairPosition = new Vector2(cursor.transform.position.x, cursor.transform.position.y);
        float angle = Vector2.SignedAngle(playerPosition.normalized, crosshairPosition.normalized);

        Debug.Log(angle);
        testRotater.transform.rotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetKeyDown(KeyCode.E))
        {
            Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, angle));
        }

        /*
        Debug.Log("Player Position: " + gameObject.transform.position);
        Debug.Log("Crosshair Position: " + cursor.transform.position);
        */
	}
}
