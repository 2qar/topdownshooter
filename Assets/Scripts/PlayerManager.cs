using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour 
{
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
    // if true, the player will not take damage
    public bool iFrames = false;

    SpriteRenderer sr; 

	// Use this for initialization
	void Start () 
    {
        EnemySpawner.SpawnEnemiesInRoom(15);
        sr = gameObject.GetComponent<SpriteRenderer>();

        Weapon gun = new Weapon((int)Weapon.WeaponName.pistol);
	}
	
	// Update is called once per frame
	void Update () 
    {
		
	}



}
