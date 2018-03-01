using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour 
{

	// Use this for initialization
	void Start () 
    {
		
	}

    private void FixedUpdate()
    {
        transform.position += transform.right * 8f;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if the bullet is a player bullet and it hits the enemy, subtract health
        if (gameObject.tag == "PlayerBullet")
            if (collision.gameObject.tag == "Enemy")
                collision.gameObject.GetComponent<EnemyManager>().Health--;
        
        // create an explosion and destroy the bullet
        StartCoroutine(Effects.BulletHitEffect(gameObject));
    }

}
