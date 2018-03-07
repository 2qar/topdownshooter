using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Don't know where else to write this, so here's the game plan:
 * -Bunch o levels
 * -Each level is wave based, bunch of dudes n stuff
 * -Between waves there's a shop
 * -Basically this is top down killing floor
 * -Enemies explode into bits that go towards a meter for the player
 * -Once it fills up, the player moves super fast and shoots super fast
 * -Encourage aggressive and risky gameplay
 */

/**
 * another idea
 * - walls kill you when you touch them
 * - rooms get smaller 
 * - rush between rooms
 * - amount of enemies gets higher and higher
 * - player weapon, ammo, health etc persists
 * 
 * - make a way for the player to increase the size of the room
 * - make a way for the enemies to decrease the size of the room
 */

/**
 * ANOTHER idea
 * - doom-like levels w/ secrets
 * - similar structure to the game u were gonna make at thomas' house
 */

public class PlayerMovement : MonoBehaviour 
{
    public float speed;

    private Rigidbody2D rb;

	// Use this for initialization
	void Start () 
    {
        // Get the player's rigidbody
        rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () 
    {
		getMovementInput();
	}

    void getMovementInput()
    {
        // Get input on the x axis from WASD or arrow keys
        float x = Input.GetAxis("Horizontal") * speed;
        // Get input on the y axis from WASD or arrow keys
        float y = Input.GetAxis("Vertical") * speed;
        // Apply this movement
        rb.velocity = new Vector2(x, y);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

    }

}
