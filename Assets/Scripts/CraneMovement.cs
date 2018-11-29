﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneMovement : MonoBehaviour
{
    public float accForce = 5f;
    public float maxSpeed = 5f;

    private Rigidbody2D rigidbody2d;
    private bool isMoving = false;
    private string keyHolding;
    private bool isElevating = false;
    private bool direction = false;//0 is up, 1 is down

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Move();
        Elevate();
        Freeze();
        SpeedLimit();
        PositionLimit();
    }

    void Move()
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

    void Elevate()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (isElevating == false)
            {
                isElevating = true;
                rigidbody2d.constraints &= ~RigidbodyConstraints2D.FreezePositionY;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (isElevating)
            {
                if (!direction)
                {
                    rigidbody2d.AddForce(transform.up * accForce);
                }
                else if(direction)
                {
                    rigidbody2d.AddForce(-transform.up * accForce);
                }
            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (isElevating)
            {
                isElevating = false;
                direction = !direction;
            }
        }
    }
    
    void Freeze()
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

    void SpeedLimit()
    {
        if (rigidbody2d.velocity.magnitude > maxSpeed)
        {
            rigidbody2d.velocity = rigidbody2d.velocity.normalized * maxSpeed;
        }
    }

    void PositionLimit()
    {
        if (transform.position.y > 5)
        {
            Vector2 temp = new Vector2(transform.position.x, 5f);
            transform.position = temp;
        }
    }
}