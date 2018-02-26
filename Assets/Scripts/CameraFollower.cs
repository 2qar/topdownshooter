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

    // Update is called once per frame
    /*
    void Update () 
    {
        transform.position = cameraMovement(transform.position.x, objectToFollow.transform.position.x,
                                            transform.position.y, objectToFollow.transform.position.y);
        transform.position = new Vector3(transform.position.x, transform.position.y, -10);
	}

    Vector3 cameraMovement(float x, float targetX, float y, float targetY)
    {
        return new Vector3(smoothCameraFollow(x, targetX, smoothTime),
                           smoothCameraFollow(y, targetY, smoothTime));
    }

    float smoothCameraFollow(float currentPosition, float targetPosition, float duration)
    {
        return Mathf.SmoothDamp(currentPosition, targetPosition, ref currentVelocity, duration);
    }
    */

    private void Update()
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
    }
}
