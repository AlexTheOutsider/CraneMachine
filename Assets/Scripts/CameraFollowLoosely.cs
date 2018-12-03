using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollowLoosely : MonoBehaviour
{
    public float maxDistanceUp = 3f;
    public float maxDistanceDown = 2f;
    public float lowerBorderY = 4f;
    public float minZoom = 7f;
    public float maxZoom = 17f;
    public float smooth = 1f;

    void Update()
    {
        Vector3 cranePosition = GameObject.Find("Center").transform.position;
        Vector3 cameraPosition = transform.position;
        //Vector3 groundPosition = GameObject.Find("GroundPoint").transform.position;

        CameraFollow(cranePosition, cameraPosition);
    }

    void CameraFollow(Vector3 cranePosition, Vector3 cameraPosition)
    {
        float distanceY = GetComponent<Camera>().orthographicSize + cameraPosition.y - cranePosition.y;
        if (distanceY < maxDistanceUp)
        {
            Vector3 newCameraPosition = cameraPosition
                                        + transform.up * (maxDistanceUp - distanceY);
            transform.position = Vector3.Lerp(cameraPosition, newCameraPosition, smooth * Time.deltaTime);
            //transform.position = newCameraPosition;

            float zoom = (GetComponent<Camera>().orthographicSize + maxDistanceUp - distanceY);
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            GetComponent<Camera>().orthographicSize =
                Mathf.Lerp(GetComponent<Camera>().orthographicSize, zoom, smooth * Time.deltaTime);
        }

        distanceY = cranePosition.y - cameraPosition.y;
        if (distanceY < -maxDistanceDown)
        {
            Vector3 newCameraPosition = cameraPosition
                                        + transform.up * (distanceY + maxDistanceDown);
            if (newCameraPosition.y < lowerBorderY)
                newCameraPosition.y = lowerBorderY;
            transform.position = Vector3.Lerp(cameraPosition, newCameraPosition, smooth * Time.deltaTime);

            float zoom = (GetComponent<Camera>().orthographicSize + distanceY + maxDistanceDown);
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            GetComponent<Camera>().orthographicSize =
                Mathf.Lerp(GetComponent<Camera>().orthographicSize, zoom, smooth * Time.deltaTime);
        }
    }
}