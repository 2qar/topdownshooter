using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generates a weapon GameObject and a list of properties associated with the weapon.
/// </summary>
public class Weapon 
{
    public Sprite testSprite;
    [HideInInspector]
    public GameObject gun;

    public enum WeaponName { pistol, minigun };
    private string weaponName;
    private Sprite weaponSprite;
    private float fireRate;
    private float angleOffset;
    private int weaponDamage;
    private bool singleFire;
    public enum AmmoType { bullets, shells, energy, explosive, voidAmmo };
    private int ammoType;

    private Color spriteColor;

    /// <summary>
    /// Creates a weapon object and assigns creates a GameObject with the correct physical properties.
    /// </summary>
    /// <param name="weapon">
    /// Weapon number.
    /// </param>
    public Weapon(int weapon)
    {
        // get the correct weapon based on the number given by the user
        switch (weapon)
        {
            case (int)WeaponName.pistol:
                weaponName = "Pistol";
                //weaponSprite = 
                fireRate = .05f;
                angleOffset = 10;
                weaponDamage = 1;
                singleFire = true;
                ammoType = (int)AmmoType.bullets;

                spriteColor = Color.red;

                break;
            case (int)WeaponName.minigun:
                weaponName = "Minigun";
                //weaponSprite =
                fireRate = .05f;
                angleOffset = 15;
                weaponDamage = 1;
                singleFire = false;
                ammoType = (int)AmmoType.bullets;

                spriteColor = Color.blue;

                break;
        }

        // create a gameobject with the right properties
        gun = new GameObject(weaponName);
        SpriteRenderer sr = gun.AddComponent<SpriteRenderer>();
        sr.sprite = testSprite;
        Debug.Log("working");
        sr.color = spriteColor;

        gun.AddComponent<BoxCollider2D>().isTrigger = true;
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

}