using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RopeRenderer : MonoBehaviour
{
    private DistanceJoint2D distanceJoint2D;
    private LineRenderer lineRenderer;
    private Transform bone1;
    
    private void Start()
    {
        distanceJoint2D = GetComponent<DistanceJoint2D>();
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        bone1 = transform.parent.Find("Bone1");
    }

    private void Update()
    {
        lineRenderer.SetPosition(0, transform.TransformPoint(distanceJoint2D.anchor));
        lineRenderer.SetPosition(1, bone1.TransformPoint(distanceJoint2D.connectedAnchor));
    }
}