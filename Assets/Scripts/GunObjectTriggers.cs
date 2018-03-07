using UnityEngine;
using System.Collections;

public class GunObjectTriggers : MonoBehaviour
{
    [HideInInspector]
    public SpriteRenderer outline;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
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
            outline.enabled = false;
            PlayerManager.instance.touchingDroppedWeapon = false;
        }
    }
}