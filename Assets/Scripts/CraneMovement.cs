using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneMovement : MonoBehaviour
{
    public float accForce = 100f;
    public float maxSpeed = 1f;

    private Rigidbody2D rigidbody2d;
    private bool isMoving = false;
    private string keyHolding;
    private bool isElevating = false;
    private bool direction = false; //0 is up, 1 is down

    private void Start()
    {
        rigidbody2d = transform.Find("Base").GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        //Move();
        if (CompareTag("Player1"))
            Elevate(KeyCode.Space);
        else if(CompareTag("Player2"))
            Elevate((KeyCode.Keypad0));
        Freeze();
        SpeedLimit();
    }

    private void Move()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (isMoving == false)
            {
                isMoving = true;
                keyHolding = "Right";
                rigidbody2d.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            }
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (isMoving && keyHolding == "Right")
            {
                rigidbody2d.AddForce(transform.right * accForce);
            }
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) && keyHolding == "Right")
        {
            isMoving = false;
            keyHolding = null;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (isMoving == false)
            {
                isMoving = true;
                keyHolding = "Left";
                rigidbody2d.constraints &= ~RigidbodyConstraints2D.FreezePositionX;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if (isMoving && keyHolding == "Left")
            {
                rigidbody2d.AddForce(-transform.right * accForce);
            }
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) && keyHolding == "Left")
        {
            isMoving = false;
            keyHolding = null;
        }
    }

    private void Elevate(KeyCode elevateKey)
    {
        if (Input.GetKeyDown(elevateKey))
        {
            if (isElevating == false)
            {
                isElevating = true;
                rigidbody2d.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            }
        }

        if (Input.GetKey(elevateKey))
        {
            if (isElevating)
            {
                if (!direction)
                {
                    rigidbody2d.AddForce(transform.up * accForce);
                }
                else if (direction)
                {
                    rigidbody2d.AddForce(-transform.up * accForce);
                }
            }
        }

        if (Input.GetKeyUp(elevateKey))
        {
            if (isElevating)
            {
                isElevating = false;
                direction = !direction;
            }
        }
    }

    private void Freeze()
    {
        if (!isMoving)
        {
            rigidbody2d.constraints |= RigidbodyConstraints2D.FreezePositionX;
        }

        if (!isElevating)
        {
            rigidbody2d.constraints |= RigidbodyConstraints2D.FreezePositionY;
        }
    }

    private void SpeedLimit()
    {
        if (rigidbody2d.velocity.magnitude > maxSpeed)
        {
            rigidbody2d.velocity = rigidbody2d.velocity.normalized * maxSpeed;
        }
    }
}