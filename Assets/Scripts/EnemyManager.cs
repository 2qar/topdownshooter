using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour 
{
    // player components
    private GameObject player;
    private PlayerManager playerMan;

    // enemy components
    SpriteRenderer sr;
    Rigidbody2D rb;

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
                ScreenShaker.ShakeCamera(4f);
                Destroy(gameObject);
            }

            health = value;
        }
    }

	// Use this for initialization
	void Start () 
    {
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
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if the enemy collides with the player,
        if (collision.gameObject.name == player.name)
        {
            if(player != null)
                playerMan.Health--;
        }
            
    }

}
