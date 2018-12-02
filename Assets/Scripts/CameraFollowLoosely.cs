using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollowLoosely : MonoBehaviour
{
    public float maxDistanceUp = 3f;
    public float maxDistanceDown = 2f;
    public float lowerBorderY = 2f;
    public float minZoom = 7f;
    public float maxZoom = 17f;
    public float smooth = 1f;

    void Update()
    {
        Vector2 cranePosition = GameObject.Find("Center").transform.position;
        Vector2 cameraPosition = transform.position;
        Vector2 groundPosition = GameObject.Find("GroundPoint").transform.position;

        CameraFollow(cranePosition, cameraPosition);
    }
    
    void CameraFollow(Vector2 cranePosition, Vector2 cameraPosition)
    {
        float distanceY = GetComponent<Camera>().orthographicSize + cameraPosition.y - cranePosition.y;
        if (distanceY < maxDistanceUp)
        {
            Vector3 newCameraPosition = transform.position
                                        + transform.up * (maxDistanceUp - distanceY) * smooth * Time.deltaTime;
            transform.position = newCameraPosition;

            float zoom = (GetComponent<Camera>().orthographicSize + maxDistanceUp - distanceY);
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            GetComponent<Camera>().orthographicSize =
                Mathf.Lerp(GetComponent<Camera>().orthographicSize, zoom, smooth * Time.deltaTime);
        }

        distanceY = cranePosition.y - cameraPosition.y;
        if (distanceY < -maxDistanceDown)
        {
            Vector3 newCameraPosition = transform.position
                                        + transform.up * (distanceY + maxDistanceDown) * smooth * Time.deltaTime;
            if (newCameraPosition.y < lowerBorderY)
                newCameraPosition.y = lowerBorderY;
            transform.position = newCameraPosition;
            
            float zoom = (GetComponent<Camera>().orthographicSize + distanceY + maxDistanceDown);
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            GetComponent<Camera>().orthographicSize =
                Mathf.Lerp(GetComponent<Camera>().orthographicSize, zoom, smooth * Time.deltaTime);
        }
    }
}