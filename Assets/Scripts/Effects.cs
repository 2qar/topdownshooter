using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A various collection of effects for players, enemies, bullets etc. to use in different situations.
/// </summary>
public class Effects : MonoBehaviour 
{
    /// <summary>
    /// Makes the player invincible for a second and plays an effect to let the player know they've been hit.
    /// </summary>
    /// <param name="playerMan">
    /// The player's management script, needed to make the player invincible.
    /// </param>

    /// <param name="playerSprite">
    /// The player's sprite.
    /// </param>
    public static IEnumerator Damage(PlayerManager playerMan, SpriteRenderer playerSprite)
    {
        // make the player invincible and transparent for a second
        playerMan.iFrames = true;
        playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, .5f);

        yield return new WaitForSeconds(1f);

        // change the player's alpha back to full and disable invincibility
        playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1);
        playerMan.iFrames = false;

        yield break;
    }

    /// <summary>
    /// Make the enemy flash white.
    /// </summary>
    /// <returns>The damage.</returns>
    /// <param name="enemySprite">Enemy sprite.</param>
    public static IEnumerator Damage(SpriteRenderer enemySprite)
    {
        // store the enemy's original color
        Color originalColor = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b);

        // make the enemy flash white
        enemySprite.color = Color.white;
        yield return new WaitForSeconds(.01f);
        enemySprite.color = originalColor;

        yield break;
    }

    /// <summary>
    /// Shakes the main camera inside of a unit circle with a radius of strength.
    /// </summary>
    /// <param name="strength">
    /// The radius of the unit circle to generate a point inside; the higher the value, the stronger the screen shake.
    /// </param>
    public static void ShakeCamera(float strength)
    {
        // Generate a random new position for the camera to move to
        Vector3 newPos = Random.insideUnitCircle * strength;
        // Update the new position's z to -10 so the camera doesn't move forward and make everything invisible
        newPos.z = -10;
        // Set the camera to the random position
        Camera.main.transform.position += newPos;
    }

    // FIXME: Explosions still get stuck, make a prefab that runs this method and destroys itself at the end to prevent hanging
        // Seems to happen mostly when the game is running at a low framerate
    /// <summary>
    /// Creates a small explosion at the given bullet's position and destroys the bullet.
    /// </summary>
    /// <param name="bullet">Bullet object to create an explosion on.</param>
    public static IEnumerator BulletHitEffect(GameObject bullet)
    {
        Explosion explosion = new Explosion(bullet);

        yield return new WaitForSeconds(.01f);
        explosion.Color = Color.black;
        yield return new WaitForSeconds(.01f);

        Destroy(explosion.ExplosionObject);
        bullet.transform.position = new Vector3(100000, 0, 0);
        Destroy(bullet, .1f);

        yield break;
    }

    // FIXME: Same issue w/ bullet hit effect, move the player off screen and wait to kill them
    /// <summary>
    /// Creates a muzzle flash at the given object when called.
    /// </summary>
    /// <param name="player">
    /// The object to create a flash at.
    /// </param>
    public static IEnumerator MuzzleFlash(GameObject player)
    {
        Explosion explosion = new Explosion(player);
        explosion.Color = new Color(1, 1, 0, .3f);

        yield return new WaitForSeconds(.1f);
        Destroy(explosion.ExplosionObject);

        yield break;
    }

    private class Explosion
    {
        // the explosion itself
        private GameObject explosionObject;
        public GameObject ExplosionObject
        {
            get { return explosionObject; }
        }

        private SpriteRenderer sprite;

        // the color of the explosion's sprite
        private Color color;
        public Color Color
        {
            get { return color; }
            set
            {
                if(sprite != null)
                    sprite.color = value;
                color = value;
            }
        }

        // size of the explosion
        private float radius;
        public float Radius
        {
            get { return radius; }
            set
            {
                if(explosionObject != null)
                    explosionObject.transform.localScale = new Vector3(radius, radius, radius);
                radius = value;
            }
        }

        /// <summary>
        /// Create a new explosion at the given GameObject's position.
        /// </summary>
        /// <param name="obj">
        /// Object to create the explosion at.
        /// </param>
        public Explosion(GameObject obj)
        {
            explosionObject = Instantiate(Resources.Load<GameObject>("Prefabs/BulletExplosion"), 
                                            obj.transform.position, Quaternion.identity);
            explosionObject.transform.position = new Vector3(explosionObject.transform.position.x, 
                                                                explosionObject.transform.position.y, -1);
            sprite = explosionObject.GetComponent<SpriteRenderer>();
            Color = sprite.color;
        }
    }

    // TODO: Write this method
    public static void WeaponDropRotation()
    {
        
    }

}
