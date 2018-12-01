using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneController : MonoBehaviour
{
    //public float hookSpeed;
    public float armSpeed;

    public float leftHookOpenDegree = 60f;
    public float leftHookCloseDegree = 20f;
    public float rightHookOpenDegree = -60f;
    public float rightHookCloseDegree = -20f;
    public float smooth = 5f;

    private Transform bone1;
    private Transform bone2;
    private Transform leftHook;
    private Transform rightHook;

    private bool isRotatingBone1 = false;
    private bool isRotatingBone2 = false;
    private string keyHoldingBone1;
    private string keyHoldingBone2;
    private JointMotor2D motor;

    private bool isHooking = false;
    private string keyHooking;

    void Start()
    {
        bone1 = transform.Find("Bone1");
        bone2 = transform.Find("Bone2");
        leftHook = transform.Find("HookLeft");
        rightHook = transform.Find("HookRight");
    }

    void FixedUpdate()
    {
        FirstArmControl();
        SecondArmControl();
        FreezeMovement(isRotatingBone1, isRotatingBone2);
        HookControl();
    }

    void FirstArmControl()
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

    void SecondArmControl()
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

    void FreezeMovement(bool isRotatingLeft, bool isRotatingRight)
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

    void HookControl()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            if (isHooking == false)
            {
                keyHooking = "J";
                isHooking = true;
            }
        }

        if (Input.GetKey(KeyCode.J))
        {
            if (isHooking && keyHooking == "J")
            {
/*                if (leftHook.GetComponent<RelativeJoint2D>().angularOffset > 20)
                    leftHook.GetComponent<RelativeJoint2D>().angularOffset -= hookSpeed * Time.deltaTime;
                if (rightHook.GetComponent<RelativeJoint2D>().angularOffset < -20)
                    rightHook.GetComponent<RelativeJoint2D>().angularOffset += hookSpeed * Time.deltaTime;*/

                leftHook.GetComponent<RelativeJoint2D>().angularOffset =
                    Mathf.Lerp(leftHook.GetComponent<RelativeJoint2D>().angularOffset, leftHookCloseDegree,
                        Time.deltaTime * smooth);
                rightHook.GetComponent<RelativeJoint2D>().angularOffset =
                    Mathf.Lerp(rightHook.GetComponent<RelativeJoint2D>().angularOffset, rightHookCloseDegree,
                        Time.deltaTime * smooth);
            }
        }

        if (Input.GetKeyUp(KeyCode.J) && keyHooking == "J")
        {
            isHooking = false;
            keyHooking = null;
        }

        if (Input.GetKeyDown(KeyCode.K))
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
/*                if (leftHook.GetComponent<RelativeJoint2D>().angularOffset < 60)
                    leftHook.GetComponent<RelativeJoint2D>().angularOffset += hookSpeed * Time.deltaTime;
                if (rightHook.GetComponent<RelativeJoint2D>().angularOffset > -60)
                    rightHook.GetComponent<RelativeJoint2D>().angularOffset -= hookSpeed * Time.deltaTime;*/
                leftHook.GetComponent<RelativeJoint2D>().angularOffset = leftHookOpenDegree;
                rightHook.GetComponent<RelativeJoint2D>().angularOffset = rightHookOpenDegree;
            }
        }

        if (Input.GetKeyUp(KeyCode.K) && keyHooking == "K")
        {
            isHooking = false;
            keyHooking = null;
        }
    }
}