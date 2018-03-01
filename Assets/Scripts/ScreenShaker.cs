using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShaker : MonoBehaviour
{
    static Camera mainCam;

    static float currentVelocity;
    static float startTime;
    static float duration = .5f;

    // point for camera to return to, should anchor to targetObject
    static Vector3 origin;

    // Object to use as the origin object
    public static GameObject targetObject;

    private void Start()
    {
        mainCam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCameraOrigin(targetObject);
        //ResetCameraPosition();
    }

    /// <summary>
    /// Shakes the camera inside of a unit circle with a radius of strength.
    /// </summary>
    /// <param name="strength">
    /// The radius of the unit circle to generate a point inside, effectively strength.
    /// </param>
    public static void ShakeCamera(float strength)
    {
        // Gets the start time so the camera can start resetting
        startTime = Time.time;
        // Generate a random new position for the camera to move to
        Vector3 newPos = Random.insideUnitCircle * strength;
        // Update the new position's z to -10 so the camera doesn't move forward and make everything invisible
        newPos.z = -10;
        // Set the camera's position to this smoothed position
        mainCam.transform.position += newPos;
    }

    /// <summary>
    /// Constantly moves the camera back to the origin.
    /// </summary>
    private void ResetCameraPosition()
    {
        // Time stuff
        float t = (Time.time - startTime) / duration;
        // Smooth movement between 0,0 and the current position the camera is in
        Vector3 pos = new Vector3(Mathf.SmoothStep(transform.position.x, origin.x, t),
                                  Mathf.SmoothStep(transform.position.y, origin.y, t),
                                  -10);
        // Slowly reset the camera
        transform.position = pos;
    }
    
    /// <summary>
    /// Update the origin point of the camera to be the position of an object.
    /// </summary>
    /// <param name="originObject">
    /// The object to use as an origin point.
    /// </param>
    private void UpdateCameraOrigin(GameObject originObject)
    {
        if(originObject != null)
            origin = originObject.transform.position;
    }

}
