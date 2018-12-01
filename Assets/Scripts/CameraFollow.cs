using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerLeft;
    public Transform playerRight;

    // How many units should we keep from the players
    public float zoomFactor = 1.5f;
    public float speed = 5f;

    private Vector3 centerPosition;

    // Use this for initialization
    void Start()
    {
        transform.position = (playerLeft.position + playerRight.position) / 2f;
    }

    // Update is called once per frame
    void Update()
    {
        FixedCameraFollowSmooth(GetComponent<Camera>(), playerLeft, playerRight);
    }

    // Follow Two Transforms with a Fixed-Orientation Camera
    public void FixedCameraFollowSmooth(Camera cam, Transform t1, Transform t2)
    {
        // Midpoint we're after
        Vector3 midpoint = (t1.position + t2.position) / 2f;
        // Distance between objects
        float distance = (t1.position - t2.position).magnitude;
        // Move camera a certain distance, doesn't matter for orthographic camera
        Vector3 cameraDestination = midpoint - cam.transform.forward * distance * zoomFactor;
        if (cam.orthographic)
        {
            cam.orthographicSize = distance;
        }
        cam.transform.position = Vector3.Slerp(cam.transform.position, cameraDestination, speed * Time.deltaTime);
        // Snap when close enough to prevent annoying slerp behavior
        if ((cameraDestination - cam.transform.position).magnitude <= 0.05f)
            cam.transform.position = cameraDestination;
    }
}