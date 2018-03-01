using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public enum WeaponName { pistol, minigun };
    private string weaponName;
    private Sprite weaponSprite;
    private float fireRate;
    private int weaponDamage;
    private bool singleFire;
    public enum AmmoType { bullets, shells, energy, explosive, voidAmmo };
    private int ammoType;

    /// <summary>
    /// Creates a weapon without assigning it to a GameObject.
    /// </summary>
    /// <param name="weapon">
    /// Weapon number.
    /// </param>
    public Weapon(int weapon)
    {
        InitializeWeapon(weapon);
    }

    public Weapon(int weapon, GameObject gun)
    {
        InitializeWeapon(weapon);
    }

    /// <summary>
    /// Initializes a new weapon as an instance of the <see cref="T:Weapon"/> class.
    /// </summary>
    /// <param name="weapon">
    /// Weapon number. 
    /// Use <see cref="T:Weapon"/>.WeaponName to get the correct int values.
    /// </param>
    private void InitializeWeapon(int weapon)
    {
        switch (weapon)
        {
            case (int)WeaponName.pistol:
                weaponName = name;
                //weaponSprite = 
                fireRate = .05f;
                weaponDamage = 1;
                singleFire = true;
                ammoType = (int)AmmoType.bullets;
                break;
            case (int)WeaponName.minigun:
                weaponName = name;
                //weaponSprite =
                fireRate = .05f;
                weaponDamage = 1;
                singleFire = false;
                ammoType = (int)AmmoType.bullets;
                break;
            }
    }

}

