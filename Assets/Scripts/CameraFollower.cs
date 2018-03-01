using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(Camera))]
public class CameraFollower : MonoBehaviour 
{
    // The gameobject for the camera to follow
    public GameObject objectToFollow;

    // How long it should take for the camera to get from it's current position to the player
    public float smoothTime;

    float currentVelocityX;
    float currentVelocityY;

    public GameObject crosshair;
    public GameObject player;

    private void FixedUpdate()
    {
        followObject(objectToFollow);

        /*
        Vector3 difference = crosshair.transform.position - player.transform.position;
        difference.z = -10;
        transform.position = transform.position + (difference / 5);
        */
    }

    /// <summary>
    /// Makes the camera smoothly follow a given target.
    /// </summary>
    /// <param name="target">
    /// Target object to follow.
    /// </param>
    private void followObject(GameObject target)
    {
        // x and y positions
        float[] positions = smoother(target);
        if (target != null)
            transform.position = new Vector3(positions[0], positions[1], -10);
    }

    /// <summary>
    /// Gets a smoothened x and y position between the camera and the given target.
    /// </summary>
    /// <returns>
    /// If the target object exists, it returns an x and y position for the camera to use.
    /// If the target object has been destroyed or hasn't been set, it returns an empty float array.
    /// </returns>
    /// <param name="target">
    /// Target object to follow.
    /// </param>
    private float[] smoother(GameObject target)
    {
        // if the target object exists,
        try
        {
            // return x and y positions for the camera to use
            return new float[]
            {
                // x position
                Mathf.SmoothDamp(transform.position.x, target.transform.position.x, ref currentVelocityX, smoothTime),
                // y position
                Mathf.SmoothDamp(transform.position.y, target.transform.position.y, ref currentVelocityY, smoothTime)
            };
        }
        catch(Exception e)
        {
            Debug.Log(e);
            return new float[0];
        }
    }

}
