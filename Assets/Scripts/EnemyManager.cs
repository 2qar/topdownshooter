using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Maybe do AI agent / navmesh stuff
// TODO: When enemies get guns and are in levels w/ obstacles, make them cast a ray towards the player before shooting to make sure they don't just shoot walls and look dumb
// TODO: Write basic enemy movement behavior

public class EnemyManager : MonoBehaviour 
{
    private Transform gunRotater;

    // player components
    private GameObject player;
    private PlayerManager playerMan;

    // enemy components
    SpriteRenderer sr;
    Rigidbody2D rb;

    Weapon gun;

    private int health = 3;
    public int Health
    {
        get { return health; }
        set
        {
            // make the enemy flash white, stop the effect before running so the enemy doesn't get stuck
            //StopCoroutine(Effects.Damage(sr));
            StartCoroutine(Effects.Damage(sr));

            if(value <= 0)
            {
                ScreenShaker.ShakeCamera(10f);
                dropWeaponOrAmmo();
                Destroy(gameObject);
            }

            health = value;
        }
    }

	// Use this for initialization
	void Start () 
    {
        gunRotater = gameObject.transform.GetChild(0);
        gun = new Weapon("Pistol", gunRotater);

        // player components
        player = GameObject.FindGameObjectWithTag("Player");
        playerMan = player.GetComponent<PlayerManager>();

        // enemy components
        sr = gameObject.GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        // note: doesn't like to run
        //Effects.RotateObject(gameObject, 15f, 2f);
	}
	
    private void FixedUpdate()
    {
        if (player != null)
            rb.velocity = (player.transform.position - gameObject.transform.position).normalized * 20;

        // TODO: Make collision layers for the raycasts
        /*
        RaycastHit2D ray = Physics2D.Raycast(gun.bulletSpawnPoint.transform.position, 
                                             PlayerManager.instance.transform.position, 
                                             Mathf.Infinity, 
                                             );
                                             */

        //Debug.Log(ray.transform);

        // TODO: Add dampening to this so the enemies don't perfectly follow the player
        float angle = gun.GetWeaponAngle(PlayerManager.instance.gameObject, gameObject);
        gun.UpdateGunSpritePosition(angle, gun.sr);
        gunRotater.transform.rotation = Quaternion.Euler(0, 0, angle);

        //fire on a timer based on the enemy's weapon
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if the enemy collides with the player,
        if (collision.gameObject.name == player.name)
            if(playerMan != null)
                playerMan.Health--;

        if (collision.gameObject.tag == "PlayerBullet")
            Health -= PlayerFire.instance.gun.WeaponDamage;
    }

    private void dropWeaponOrAmmo()
    {
        int chance = Random.Range(1, 5);
        if(chance > 3)
        {
            if(Random.Range(1, 4) == 3)
                if (PlayerFire.instance.gun.gun.name == gun.gun.name)
                    createAmmo();
                else
                    gun.gun.transform.parent = null;
        }
    }

    /// <summary>
    /// Create an ammo box.
    /// </summary>
    private void createAmmo()
    {
        GameObject ammoBox = new GameObject();
        ammoBox.name = "Ammo Box";
        SpriteRenderer ammoSr = ammoBox.AddComponent<SpriteRenderer>();
        ammoBox.transform.position = gameObject.transform.position;
        ammoSr.sprite = Resources.Load<Sprite>("Sprites/Square");
        ammoSr.color = Color.green;
        ammoBox.transform.localScale = new Vector3(7, 7, 7);
        ammoBox.AddComponent<BoxCollider2D>().isTrigger = true;
    }

}
