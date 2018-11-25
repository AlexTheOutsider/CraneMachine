using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneController : MonoBehaviour
{
    public float hookSpeed;
    public float armSpeed;

    private Transform bone1;
    private Transform bone2;
    private Transform hook1;
    private Transform hook2;

    private bool isRotating = false;
    private string keyHolding;
    private JointMotor2D motor;

    private bool isHooking = false;
    private string keyHooking;

    void Start()
    {
        bone1 = transform.Find("Bone1");
        bone2 = transform.Find("Bone2");
        hook1 = transform.Find("Hook1");
        hook2 = transform.Find("Hook2");
    }

    void FixedUpdate()
    {
        FirstArmControl();
        SecondArmControl();
        FreezeMovement(!isRotating);
        HookControl();
    }

    void FirstArmControl()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (isRotating == false)
            {
                keyHolding = "A";
                isRotating = true;
                bone1.GetComponent<Rigidbody2D>().freezeRotation = false;
                motor = bone1.GetComponent<HingeJoint2D>().motor;
                motor.motorSpeed = -armSpeed;
                bone1.GetComponent<HingeJoint2D>().motor = motor;
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (isRotating && keyHolding == "A")
            {
                bone1.GetComponent<HingeJoint2D>().useMotor = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.A) && keyHolding == "A")
        {
            bone1.GetComponent<HingeJoint2D>().useMotor = false;
            isRotating = false;
            keyHolding = null;
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (isRotating == false)
            {
                keyHolding = "D";
                isRotating = true;
                bone1.GetComponent<Rigidbody2D>().freezeRotation = false;
                motor = bone1.GetComponent<HingeJoint2D>().motor;
                motor.motorSpeed = armSpeed;
                bone1.GetComponent<HingeJoint2D>().motor = motor;
            }
        }

        if (Input.GetKey(KeyCode.D))
        {
            if (isRotating && keyHolding == "D")
            {
                bone1.GetComponent<HingeJoint2D>().useMotor = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.D) && keyHolding == "D")
        {
            bone1.GetComponent<HingeJoint2D>().useMotor = false;
            isRotating = false;
            keyHolding = null;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (isRotating == false)
            {
                keyHolding = "W";
                isRotating = true;
                bone2.GetComponent<Rigidbody2D>().freezeRotation = false;
                motor = bone2.GetComponent<HingeJoint2D>().motor;
                motor.motorSpeed = -armSpeed;
                bone2.GetComponent<HingeJoint2D>().motor = motor;
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            if (isRotating && keyHolding == "W")
            {
                bone2.GetComponent<HingeJoint2D>().useMotor = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.W) && keyHolding == "W")
        {
            bone2.GetComponent<HingeJoint2D>().useMotor = false;
            isRotating = false;
            keyHolding = null;
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            if (isRotating == false)
            {
                keyHolding = "S";
                isRotating = true;
                bone2.GetComponent<Rigidbody2D>().freezeRotation = false;
                motor = bone2.GetComponent<HingeJoint2D>().motor;
                motor.motorSpeed = armSpeed;
                bone2.GetComponent<HingeJoint2D>().motor = motor;
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (isRotating && keyHolding == "S")
            {
                bone2.GetComponent<HingeJoint2D>().useMotor = true;
            }
        }

        if (Input.GetKeyUp(KeyCode.S) && keyHolding == "S")
        {
            bone2.GetComponent<HingeJoint2D>().useMotor = false;
            isRotating = false;
            keyHolding = null;
        }
    }

    void SecondArmControl()
    {
    }

    void FreezeMovement(bool freeze)
    {
        if (freeze)
        {
            bone1.GetComponent<Rigidbody2D>().freezeRotation = true;
            bone2.GetComponent<Rigidbody2D>().freezeRotation = true;
        }
    }

    void HookControl()
    {
        if (Input.GetKey(KeyCode.J))
        {
            if (isHooking == false)
            {
                keyHooking = "J";
                isHooking = true;
            }
            else if (isHooking && keyHooking == "J")
            {
                if (hook1.GetComponent<RelativeJoint2D>().angularOffset > 20)
                    hook1.GetComponent<RelativeJoint2D>().angularOffset -= hookSpeed * Time.deltaTime;
                if (hook2.GetComponent<RelativeJoint2D>().angularOffset < -20)
                    hook2.GetComponent<RelativeJoint2D>().angularOffset += hookSpeed * Time.deltaTime;
            }
        }

        if (Input.GetKeyUp(KeyCode.J) && keyHooking == "J")
        {
            isHooking = false;
        }

        if (Input.GetKey(KeyCode.K))
        {
            if (isHooking == false)
            {
                keyHooking = "K";
                isHooking = true;
            }
            else if (isHooking && keyHooking == "K")
            {
/*                if (hook1.GetComponent<RelativeJoint2D>().angularOffset < 60)
                    hook1.GetComponent<RelativeJoint2D>().angularOffset += hookSpeed * Time.deltaTime;
                if (hook2.GetComponent<RelativeJoint2D>().angularOffset > -60)
                    hook2.GetComponent<RelativeJoint2D>().angularOffset -= hookSpeed * Time.deltaTime;*/
                hook1.GetComponent<RelativeJoint2D>().angularOffset = 60;
                hook2.GetComponent<RelativeJoint2D>().angularOffset = -60;
            }
        }

        if (Input.GetKeyUp(KeyCode.K) && keyHooking == "K")
        {
            isHooking = false;
        }
    }
}