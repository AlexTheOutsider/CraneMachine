using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;

public class CraneController : MonoBehaviour
{
    public float armSpeed = 20f;
    //public float hookSpeed = 5f;
    public float leftHookOpenDegree = 60f;
    public float leftHookCloseDegree = 20f;
    public float rightHookOpenDegree = -60f;
    public float rightHookCloseDegree = -20f;
    public float hookSmooth = 5f;
    public float dropSmooth = 0.5f;
    public float maxLength = 10f;
    public float minLength = 1f;

    private Transform bone1;
    private Transform bone2;
    private Transform bone3;
    private Transform leftHook;
    private Transform rightHook;

    private bool isRotatingBone1 = false;
    private bool isRotatingBone2 = false;
    private string keyHoldingBone1;
    private string keyHoldingBone2;
    private JointMotor2D motor;

    private bool isHooking = false;
    private bool hookDirection = false; //0 is close, 1 is open
    private string keyHooking;
    private bool isExtending = false;
    private bool extendDirection = false; //0 is extend, 1 is contract
    private DistanceJoint2D distanceJoint2D;

    private void Start()
    {
        bone1 = transform.Find("Bone1");
        bone2 = transform.Find("Bone2");
        bone3 = transform.Find("Bone3");
        leftHook = transform.Find("HookLeft");
        rightHook = transform.Find("HookRight");
    }

    private void FixedUpdate()
    {
        FirstArmControl();
        //SecondArmControl();
        FreezeMovement(isRotatingBone1, isRotatingBone2);
        HookControl();
    }

    private void FirstArmControl()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (isRotatingBone1 == false)
            {
                keyHoldingBone1 = "A";
                isRotatingBone1 = true;
                bone1.GetComponent<Rigidbody2D>().freezeRotation = false;
                motor = bone1.GetComponent<HingeJoint2D>().motor;
                motor.motorSpeed = -armSpeed;
                bone1.GetComponent<HingeJoint2D>().motor = motor;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (isRotatingBone1 && keyHoldingBone1 == "A")
            {
                bone1.GetComponent<HingeJoint2D>().useMotor = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.A))
        {
            if (isRotatingBone1 && keyHoldingBone1 == "A")
            {
                bone1.GetComponent<HingeJoint2D>().useMotor = false;
                isRotatingBone1 = false;
                keyHoldingBone1 = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (isRotatingBone1 == false)
            {
                keyHoldingBone1 = "D";
                isRotatingBone1 = true;
                bone1.GetComponent<Rigidbody2D>().freezeRotation = false;
                motor = bone1.GetComponent<HingeJoint2D>().motor;
                motor.motorSpeed = armSpeed;
                bone1.GetComponent<HingeJoint2D>().motor = motor;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (isRotatingBone1 && keyHoldingBone1 == "D")
            {
                bone1.GetComponent<HingeJoint2D>().useMotor = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.D))
        {
            if (isRotatingBone1 && keyHoldingBone1 == "D")
            {
                bone1.GetComponent<HingeJoint2D>().useMotor = false;
                isRotatingBone1 = false;
                keyHoldingBone1 = null;
            }
        }
    }

    private void SecondArmControl()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isRotatingBone2 == false)
            {
                keyHoldingBone2 = "W";
                isRotatingBone2 = true;
                bone2.GetComponent<Rigidbody2D>().freezeRotation = false;
                motor = bone2.GetComponent<HingeJoint2D>().motor;
                motor.motorSpeed = -armSpeed;
                bone2.GetComponent<HingeJoint2D>().motor = motor;
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (isRotatingBone2 && keyHoldingBone2 == "W")
            {
                bone2.GetComponent<HingeJoint2D>().useMotor = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            if (isRotatingBone2 && keyHoldingBone2 == "W")
            {
                bone2.GetComponent<HingeJoint2D>().useMotor = false;
                isRotatingBone2 = false;
                keyHoldingBone2 = null;
            }
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (isRotatingBone2 == false)
            {
                keyHoldingBone2 = "S";
                isRotatingBone2 = true;
                bone2.GetComponent<Rigidbody2D>().freezeRotation = false;
                motor = bone2.GetComponent<HingeJoint2D>().motor;
                motor.motorSpeed = armSpeed;
                bone2.GetComponent<HingeJoint2D>().motor = motor;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (isRotatingBone2 && keyHoldingBone2 == "S")
            {
                bone2.GetComponent<HingeJoint2D>().useMotor = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.S))
        {
            if (isRotatingBone2 && keyHoldingBone2 == "S")
            {
                bone2.GetComponent<HingeJoint2D>().useMotor = false;
                isRotatingBone2 = false;
                keyHoldingBone2 = null;
            }
        }
    }

    private void FreezeMovement(bool isRotatingLeft, bool isRotatingRight)
    {
        if (!isRotatingLeft)
        {
            bone1.GetComponent<Rigidbody2D>().freezeRotation = true;
        }

        if (!isRotatingRight)
        {
            bone2.GetComponent<Rigidbody2D>().freezeRotation = true;
        }
    }

    private void HookControl()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (isHooking == false)
            {
                isHooking = true;
            }
        }

        if (Input.GetKey(KeyCode.J))
        {
            if (isHooking)
            {
                if (!hookDirection)
                {
/*                if (leftHook.GetComponent<RelativeJoint2D>().angularOffset > 20)
                    leftHook.GetComponent<RelativeJoint2D>().angularOffset -= hookSpeed * Time.deltaTime;
                if (rightHook.GetComponent<RelativeJoint2D>().angularOffset < -20)
                    rightHook.GetComponent<RelativeJoint2D>().angularOffset += hookSpeed * Time.deltaTime;*/

                    leftHook.GetComponent<RelativeJoint2D>().angularOffset =
                        Mathf.Lerp(leftHook.GetComponent<RelativeJoint2D>().angularOffset, leftHookCloseDegree,
                            Time.deltaTime * hookSmooth);
                    rightHook.GetComponent<RelativeJoint2D>().angularOffset =
                        Mathf.Lerp(rightHook.GetComponent<RelativeJoint2D>().angularOffset, rightHookCloseDegree,
                            Time.deltaTime * hookSmooth);
                }
                else if (hookDirection)
                {
                    /*                if (leftHook.GetComponent<RelativeJoint2D>().angularOffset < 60)
                    leftHook.GetComponent<RelativeJoint2D>().angularOffset += hookSpeed * Time.deltaTime;
                if (rightHook.GetComponent<RelativeJoint2D>().angularOffset > -60)
                    rightHook.GetComponent<RelativeJoint2D>().angularOffset -= hookSpeed * Time.deltaTime;*/
                    leftHook.GetComponent<RelativeJoint2D>().angularOffset = leftHookOpenDegree;
                    rightHook.GetComponent<RelativeJoint2D>().angularOffset = rightHookOpenDegree;
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.J))
        {
            if (isHooking)
            {
                isHooking = false;
                hookDirection = !hookDirection;
            }
        }

/*        if (Input.GetKeyDown(KeyCode.K))
        {
            if (isHooking == false)
            {
                keyHooking = "K";
                isHooking = true;
            }
        }

        if (Input.GetKey(KeyCode.K))
        {
            if (isHooking && keyHooking == "K")
            {
                /*if (leftHook.GetComponent<RelativeJoint2D>().angularOffset < 60)
                    leftHook.GetComponent<RelativeJoint2D>().angularOffset += hookSpeed * Time.deltaTime;
                if (rightHook.GetComponent<RelativeJoint2D>().angularOffset > -60)
                    rightHook.GetComponent<RelativeJoint2D>().angularOffset -= hookSpeed * Time.deltaTime;#1#
                leftHook.GetComponent<RelativeJoint2D>().angularOffset = leftHookOpenDegree;
                rightHook.GetComponent<RelativeJoint2D>().angularOffset = rightHookOpenDegree;
            }
        }

        if (Input.GetKeyUp(KeyCode.K) && keyHooking == "K")
        {
            isHooking = false;
            keyHooking = null;
        }*/

        if (Input.GetKeyDown(KeyCode.L))
        {
            if (isExtending == false)
            {
                isExtending = true;
            }
        }

        if (Input.GetKey(KeyCode.L))
        {
            if (isExtending)
            {
                distanceJoint2D = bone3.GetComponent<DistanceJoint2D>();
                if (extendDirection)
                {
                    if (distanceJoint2D.distance < maxLength)
                        distanceJoint2D.distance = Mathf.Lerp(distanceJoint2D.distance, maxLength,
                            dropSmooth * Time.deltaTime);
                }
                else if (!extendDirection)
                {
                    if (distanceJoint2D.distance > minLength)
                        distanceJoint2D.distance = Mathf.Lerp(distanceJoint2D.distance, minLength,
                            dropSmooth * Time.deltaTime);
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.L))
        {
            if (isExtending)
            {
                isExtending = false;
                extendDirection = !extendDirection;
            }
        }
    }
}