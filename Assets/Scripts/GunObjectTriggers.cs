using UnityEngine;
using System.Collections;

public class GunObjectTriggers : MonoBehaviour
{
    [HideInInspector]
    public SpriteRenderer outline;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && transform.parent == null)
        {
            outline.enabled = true;
            PlayerManager.instance.touchingDroppedWeapon = true;
            PlayerManager.instance.droppedWeapon = gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            // FIXME: The player can't pick up a weapon if they're colliding with two and stop colliding with one of them cus of this
                // Maybe use OnCollisionStay2D instead for marking touchingDroppedWeapon as true
            outline.enabled = false;
            PlayerManager.instance.touchingDroppedWeapon = false;
        }
    }
}