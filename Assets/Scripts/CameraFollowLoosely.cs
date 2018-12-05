using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraFollowLoosely : MonoBehaviour
{
    public float maxDistanceUp = 4f;
    public float maxDistanceDown = 1f;
    public float lowerBorderY = 4f;
    public float minZoom = 11f;
    public float maxZoom = 17f;
    public float smooth = 10f;
    public GameObject player1;
    public GameObject player2;

    void Update()
    {
        float cranePosition = (player1.transform.position.y > player2.transform.position.y)
            ? player1.transform.position.y
            : player2.transform.position.y;
        Vector3 cameraPosition = transform.position;
        //Vector3 groundPosition = GameObject.Find("GroundPoint").transform.position;

        CameraFollow(cranePosition, cameraPosition);
    }

    void CameraFollow(float cranePositionY, Vector3 cameraPosition)
    {
        float distanceY = GetComponent<Camera>().orthographicSize + cameraPosition.y - cranePositionY;
        if (distanceY < maxDistanceUp)
        {
            Vector3 newCameraPosition = cameraPosition
                                        + transform.up * (maxDistanceUp - distanceY);
            transform.position = Vector3.Lerp(cameraPosition, newCameraPosition, smooth * Time.deltaTime);

            float zoom = (GetComponent<Camera>().orthographicSize + maxDistanceUp - distanceY);
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            GetComponent<Camera>().orthographicSize =
                Mathf.Lerp(GetComponent<Camera>().orthographicSize, zoom, smooth * Time.deltaTime);
        }

        distanceY = cranePositionY - cameraPosition.y;
        if (distanceY < maxDistanceDown)
        {
            Vector3 newCameraPosition = cameraPosition
                                        + transform.up * (distanceY - maxDistanceDown);
            if (newCameraPosition.y < lowerBorderY)
                newCameraPosition.y = lowerBorderY;
            transform.position = Vector3.Lerp(cameraPosition, newCameraPosition, smooth * Time.deltaTime);

            float zoom = (GetComponent<Camera>().orthographicSize + distanceY - maxDistanceDown);
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            GetComponent<Camera>().orthographicSize =
                Mathf.Lerp(GetComponent<Camera>().orthographicSize, zoom, smooth * Time.deltaTime);
        }
    }
}