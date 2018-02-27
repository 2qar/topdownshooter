using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        transform.position = new Vector3(Mathf.SmoothDamp(transform.position.x,
                                                          objectToFollow.transform.position.x,
                                                          ref currentVelocityX,
                                                          smoothTime),
                                         Mathf.SmoothDamp(transform.position.y,
                                                          objectToFollow.transform.position.y,
                                                          ref currentVelocityY,
                                                          smoothTime),
                                         -10);
        Vector3 difference = crosshair.transform.position - player.transform.position;
        difference.z = -10;
        //transform.position = transform.position + (difference / 5);
    }
}
