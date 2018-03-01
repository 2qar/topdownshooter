using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour 
{
    // The player's crosshair
    public GameObject cursor;

    // Bullet to fire
    public GameObject bullet;
    // Where to spawn the bullet
    public GameObject bulletCreationPoint;

    // temporary gun object that rotates w/ mouse 
    public GameObject testRotater;

    // Stores when the player can next fire their weapon, prevents a million bullets firing at once
    float nextFire;

    // Update is called once per frame
    void Update () 
    {
        // Take the distance between the player and the crosshair
        Vector3 difference = cursor.transform.position - gameObject.transform.position;
        // Normalize it, making the the x and y values a value between 0 and 1
        difference.Normalize();

        // Convert the difference to an angle
        float angle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        testRotater.transform.rotation = Quaternion.Euler(0, 0, angle);

        // If the player presses the E key,
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
        {
            fireWeapon(angle);
        }
	}

    void fireWeapon(float angle)
    {
        StartCoroutine(Effects.MuzzleFlash(bulletCreationPoint));

        // Shoot a bullet at the proper angle
        Instantiate(bullet, bulletCreationPoint.transform.position, Quaternion.Euler(0, 0, angle + Random.Range(-15, 16)));

        // Shake that screen, boye
        ScreenShaker.ShakeCamera(2f);

        // Set a delay between the next shot
        nextFire = Time.time + .05f;
    }

}
