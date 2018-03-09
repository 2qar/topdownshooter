using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFire : MonoBehaviour 
{
    [HideInInspector]
    public static PlayerFire instance;

    // The player's crosshair
    public GameObject cursor;

    // Bullet to fire
    public GameObject bullet;
    // Where to spawn the bullet
    public GameObject bulletCreationPoint;

    public GameObject gunRotater;
    [HideInInspector]
    public Weapon gun;

    // Stores when the player can next fire their weapon, prevents a million bullets firing at once
    float nextFire;

    void Start()
    {
        // the player's weapon
        gun = new Weapon("Pistol", gunRotater);
        instance = this;

        // spawn a gun somewhere in the room or smth
        Weapon randomGun = new Weapon("Shotgun");
        randomGun.RandomizePosition(new Vector2(50, 50), new Vector2(50, 50));
    }

    // Update is called once per frame
    void Update () 
    {
        float angle = gun.GetWeaponAngle(cursor, gameObject);
        gunRotater.transform.rotation = Quaternion.Euler(0, 0, angle);

        if (checkWeaponFiringStatus())
            fireWeapon(angle);

        gun.UpdateGunSpritePosition(angle, gun.sr);
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

        if(gun.gun.name == "Shotgun")
            // make a shotgun blast
            for(int bullets = 0; bullets <= 5; bullets++)
            {
                bulletOffset = (int)Random.Range(-gun.AngleOffset, gun.AngleOffset + 1);
                Instantiate(bullet, gun.bulletSpawnPoint.transform.position, Quaternion.Euler(0, 0, angle + bulletOffset));
            }
        else
            // Shoot a bullet at the proper angle
            Instantiate(bullet, gun.bulletSpawnPoint.transform.position, Quaternion.Euler(0, 0, angle + bulletOffset));

        // Shake that screen, boye
        ScreenShaker.ShakeCamera(gun.ScreenShakeAmount);

        // Set a delay between the next shot
        nextFire = Time.time + gun.FireRate;
    }

}