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
    //public GameObject testRotater;
    //public GameObject gunObj;
    public GameObject gunRotater;
    [HideInInspector]
    public Weapon gun;

    // Stores when the player can next fire their weapon, prevents a million bullets firing at once
    float nextFire;

    void Start()
    {
        gun = new Weapon((int)Weapon.WeaponName.minigun, gunRotater);
    }

    // Update is called once per frame
    void Update () 
    {
        float angle = getWeaponAngle();
        gunRotater.transform.rotation = Quaternion.Euler(0, 0, angle);

        if (checkWeaponFiringStatus())
        {
            fireWeapon(angle);
        }
	}

    /// <summary>
    /// Checks if the player's weapon should fire or not based on the weapon's fire rate and whether it's single fire or not.
    /// </summary>
    /// <returns><c>true</c>, if the weapon should fire a bullet, <c>false</c> otherwise.</returns>
    bool checkWeaponFiringStatus()
    {
        if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire && !gun.SingleFire)
            return true;
        else if (Input.GetKeyDown(KeyCode.Mouse0) && Time.time > nextFire && gun.SingleFire)
            return true;
        else
            return false;
    }

    void fireWeapon(float angle)
    {
        StartCoroutine(Effects.MuzzleFlash(gun.bulletSpawnPoint));

        int bulletOffset = (int)Random.Range(-gun.AngleOffset, gun.AngleOffset + 1);

        // Shoot a bullet at the proper angle
        Instantiate(bullet, gun.bulletSpawnPoint.transform.position, Quaternion.Euler(0, 0, angle + bulletOffset));

        // Shake that screen, boye
        ScreenShaker.ShakeCamera(2f);

        // Set a delay between the next shot
        nextFire = Time.time + gun.FireRate;
    }

    float getWeaponAngle()
    {
        // Take the distance between the player and the crosshair
        Vector3 difference = cursor.transform.position - gameObject.transform.position;
        // Normalize it, making the the x and y values a value between 0 and 1
        difference.Normalize();

        // Convert the difference to an angle
        return Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
    }

}
