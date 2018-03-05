using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates a weapon GameObject and a list of properties associated with the weapon.
/// </summary>
public class Weapon 
{
    [HideInInspector]
    public GameObject gun;
    [HideInInspector]
    public SpriteRenderer sr;
    [HideInInspector]
    public GameObject gunOutline;
    [HideInInspector]
    public GameObject bulletSpawnPoint;

    public enum WeaponName { pistol, minigun, shotgun };
    private string weaponName;

    private Sprite weaponSprite;
    private Sprite[] gunSprites = Resources.LoadAll<Sprite>("Sprites/guns");

    private float fireRate;
    public float FireRate { get { return fireRate; } }

    private float angleOffset;
    public float AngleOffset { get { return angleOffset; } }

    private int weaponDamage;
    public int WeaponDamage { get { return weaponDamage; } }

    private bool singleFire;
    public bool SingleFire { get { return singleFire; } }

    public enum AmmoType { bullets, shells, energy, explosive, voidAmmo };
    private int ammoType;
    public int Ammo { get { return ammoType; } }

    private float screenShakeAmount;
    public float ScreenShakeAmount { get { return screenShakeAmount; } }

    /// <summary>
    /// Creates a weapon object and creates a GameObject with the correct physical properties.
    /// </summary>
    /// <param name="weapon">
    /// Weapon number.
    /// </param>
    public Weapon(int weapon)
    {
        SetWeapon(weapon);

        InitializeWeapon();
    }

    /// <summary>
    /// Creates the correct weapon as a GameObject and parents it to a given GameObject.
    /// </summary>
    /// <param name="weapon">
    /// Weapon number.
    /// </param>
    /// <param name="parent">
    /// Parent object.
    /// </param>
    public Weapon(int weapon, GameObject parent)
    {
        SetWeapon(weapon);

        InitializeWeapon();

        SetParent(parent);
    }

    /// <summary>
    /// Set the weapon to the correct one based on the int given from the WeaponName enum.
    /// </summary>
    /// <param name="weapon">Weapon number.</param>
    private void SetWeapon(int weapon)
    {
        switch (weapon)
        {
            case (int)WeaponName.pistol:
                weaponName = "Pistol";
                weaponSprite = gunSprites[1];
                fireRate = .05f;
                angleOffset = 10;
                weaponDamage = 1;
                singleFire = true;
                ammoType = (int)AmmoType.bullets;
                screenShakeAmount = 2f;
                break;
            case (int)WeaponName.minigun:
                weaponName = "Minigun";
                weaponSprite = gunSprites[2];
                fireRate = .05f;
                angleOffset = 15;
                weaponDamage = 1;
                singleFire = false;
                ammoType = (int)AmmoType.bullets;
                screenShakeAmount = 5f;
                break;
            case (int)WeaponName.shotgun:
                weaponName = "Shotgun";
                weaponSprite = gunSprites[0];
                fireRate = .5f;
                angleOffset = 35;
                weaponDamage = 1;
                singleFire = true;
                ammoType = (int)AmmoType.shells;
                screenShakeAmount = 25f;
                break;
        }
    }

    /// <summary>
    /// Sets up the weapon's gameobject and outline.
    /// </summary>
    private void InitializeWeapon()
    {
        // create a gun gameobject with the right properties
        gun = new GameObject(weaponName);
        gun.transform.localScale = new Vector3(16f, 16f, 1f);
        sr = gun.AddComponent<SpriteRenderer>();
        sr.sprite = weaponSprite;
        gun.AddComponent<BoxCollider2D>().isTrigger = true;

        // set up the outline of the gun
        gunOutline = new GameObject();
        gunOutline.name = "Gun Outline";
        gunOutline.transform.parent = gun.transform;
        gunOutline.transform.position = new Vector3(0, 0, 1);
        gunOutline.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        SpriteRenderer outline = gunOutline.AddComponent<SpriteRenderer>();
        outline.sprite = sr.sprite;
        outline.color = Color.white;

        // hide the outline; this only needs to be showing when the gun is
        // on the ground and the player goes to pick it up
        gunOutline.SetActive(false);

        bulletSpawnPoint = new GameObject();
        bulletSpawnPoint.name = "Bullet Spawn Point";
        bulletSpawnPoint.transform.parent = gun.transform;
        bulletSpawnPoint.transform.position = new Vector3(0, .5f, 0);
    }

    /// <summary>
    /// Assigns the gun as a child of a given GameObject.
    /// </summary>
    /// <param name="parentObject">Object to act as the parent.</param>
    private void SetParent(GameObject parentObject)
    {
        gun.transform.parent = parentObject.transform;
        gun.transform.position = new Vector3(0, -0.5f, 0);
        gun.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    /// <summary>
    /// Move the gun to a random position in a given area.
    /// </summary>
    /// <param name="minPosition">Minimum position.</param>
    /// <param name="maxPosition">Max position.</param>
    public void RandomizePosition(Vector2 minPosition, Vector2 maxPosition)
    {
        gun.transform.position = new Vector2(Random.Range(minPosition.x, maxPosition.x + 1),
                                             Random.Range(minPosition.y, maxPosition.y + 1));
    }

    /// <summary>
    /// Sets the rotation of the weapon to a given angle.
    /// </summary>
    /// <param name="angle">Angle to rotate the weapon by.</param>
    public void SetRotation(float angle)
    {
        gun.transform.rotation = Quaternion.Euler(0, 0, angle);
    }

}