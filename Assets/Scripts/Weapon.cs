using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// FIXME: Fix gun outline rotation: maybe get rid of gun outlines cus they look ugly
// TODO: Make guns appear in front of the player when aiming below and behind the player when aiming above

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

    //public enum WeaponName { pistol, minigun, shotgun };
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
    /// Weapon name.
    /// </param>
    public Weapon(string weapon)
    {
        SetWeapon(weapon);

        InitializeWeapon();
    }

    /// <summary>
    /// Creates the correct weapon as a GameObject and parents it to a given GameObject.
    /// </summary>
    /// <param name="weapon">
    /// Weapon name.
    /// </param>
    /// <param name="parent">
    /// Parent object.
    /// </param>
    public Weapon(string weapon, GameObject parent)
    {
        SetWeapon(weapon);

        InitializeWeapon();

        SetParent(parent);
    }

    public Weapon(string weapon, Transform parent)
    {
        SetWeapon(weapon);

        InitializeWeapon();

        SetParent(parent);
    }

    /// <summary>
    /// Set the weapon to the correct one based on the int given from the WeaponName enum.
    /// </summary>
    /// <param name="weapon">Weapon number.</param>
    private void SetWeapon(string weapon)
    {
        switch (weapon)
        {
            case "Pistol":
                weaponName = weapon;
                weaponSprite = gunSprites[1];
                fireRate = .05f;
                angleOffset = 10;
                weaponDamage = 1;
                singleFire = true;
                ammoType = (int)AmmoType.bullets;
                screenShakeAmount = 2f;
                break;
            case "Minigun":
                weaponName = weapon;
                weaponSprite = gunSprites[2];
                fireRate = .05f;
                angleOffset = 15;
                weaponDamage = 1;
                singleFire = false;
                ammoType = (int)AmmoType.bullets;
                screenShakeAmount = 5f;
                break;
            case "Shotgun":
                weaponName = weapon;
                weaponSprite = gunSprites[0];
                fireRate = .5f;
                angleOffset = 35;
                weaponDamage = 1;
                singleFire = true;
                ammoType = (int)AmmoType.shells;
                screenShakeAmount = 25f;
                break;
            default:
                Debug.Log("weapon name not recognized: " + weapon);
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
        BoxCollider2D collider = gun.AddComponent<BoxCollider2D>();
        collider.isTrigger = true;
        gun.transform.tag = "PlayerWeapon";
        GunObjectTriggers gunTriggerHandlingScript = gun.AddComponent<GunObjectTriggers>();

        // set up the outline of the gun
        gunOutline = new GameObject();
        gunOutline.name = "Gun Outline";
        gunOutline.transform.parent = gun.transform;
        gunOutline.transform.position = new Vector3(0, 0, 1);
        gunOutline.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        SpriteRenderer outline = gunOutline.AddComponent<SpriteRenderer>();
        outline.sprite = sr.sprite;
        outline.color = Color.white;
        // hide the gun outline; only enable it when the player is touching the 
        outline.enabled = false;
        // give triggerscript the gun's outline so it can be enabled and disabled when the player collides w/ the gun
        gunTriggerHandlingScript.outline = outline;

        bulletSpawnPoint = new GameObject();
        bulletSpawnPoint.name = "Bullet Spawn Point";
        bulletSpawnPoint.transform.parent = gun.transform;
        bulletSpawnPoint.transform.localPosition = new Vector3(.5f, 0, 0);
    }

    /// <summary>
    /// Assigns the gun as a child of a given GameObject.
    /// </summary>
    /// <param name="parentObject">Object to act as the parent.</param>
    private void SetParent(GameObject parentObject)
    {
        gun.transform.parent = parentObject.transform;
        // set to X: 1.5f Y: 0 Z: 0
        gun.transform.localPosition = new Vector3(.8f, 0, 0);
        gun.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    /// <summary>
    /// Assigns the gun as a child of a given Transform.
    /// </summary>
    /// <param name="parentTransform">Object to act as the parent.</param>
    private void SetParent(Transform parentTransform)
    {
        gun.transform.parent = parentTransform;
        // set to X: 1.5f Y: 0 Z: 0
        gun.transform.localPosition = new Vector3(.8f, 0, 0);
        gun.transform.localRotation = Quaternion.Euler(0, 0, 0);
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


    /// <summary>
    /// Gets the angle between the two GameObjects.
    /// </summary>
    /// <returns>The weapon angle.</returns>
    /// <param name="objectToLookAt">
    /// The object to get the angle from.
    /// </param>
    /// <param name="obj">
    /// Origin object.
    /// </param>
    public float GetWeaponAngle(GameObject objectToLookAt, GameObject obj)
    {
        // Take the distance between the player and the crosshair
        Vector3 difference = objectToLookAt.transform.position - obj.transform.position;
        // Normalize it, making the the x and y values a value between 0 and 1
        difference.Normalize();

        // Convert the difference to an angle
        return Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
    }

    /// <summary>
    /// Updates the rotation of a weapon sprite based on whether the player or enemy is aiming to the left or right.
    /// </summary>
    /// <param name="angle">
    /// Current aiming angle.
    /// </param>
    /// <param name="sr">
    /// SpriteRenderer to flip.
    /// </param>
    public void UpdateGunSpritePosition(float angle, SpriteRenderer sr)
    {
        if (angle > 90 || angle < -90)
            sr.flipY = true;
        else
            sr.flipY = false;
    }

}