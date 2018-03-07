using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour 
{
    public static PlayerManager instance;

    int health = 3;
    public int Health
    {
        get { return health; }
        set
        {
            if (value <= 0)
                Destroy(gameObject);
            health = value;
            StartCoroutine(Effects.Damage(this, sr));
        }
    }

    [HideInInspector]
    public bool touchingDroppedWeapon;
    [HideInInspector]
    public GameObject droppedWeapon;

    [HideInInspector]
    // if true, the player will not take damage
    public bool iFrames = false;

    SpriteRenderer sr; 

	// Use this for initialization
	void Start () 
    {
        instance = this;

        EnemySpawner.SpawnEnemiesInRoom(15);
        sr = gameObject.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () 
    {
		weaponSwitcher();
	}

    /// <summary>
    /// Swaps to the weapon on the ground if the player presses E while on it.
    /// </summary>
    void weaponSwitcher()
    {
        // if the player is touching the dropped weapon and they press E,
        if(Input.GetKeyDown(KeyCode.E) && touchingDroppedWeapon)
        {
            // drop the player's current weapon
            PlayerFire.instance.gun.gun.transform.parent = null;
            // grab the name of the gun on the ground
            string droppedWeaponName = droppedWeapon.name;
            // destroy it 
            Destroy(droppedWeapon);
            // equip the weapon on the ground
            PlayerFire.instance.gun = new Weapon(droppedWeaponName, PlayerFire.instance.gunRotater);
        }
    }

}
