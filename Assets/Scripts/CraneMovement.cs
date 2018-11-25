using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraneMovement : MonoBehaviour
{
    public float accForce = 5f;
    public float maxSpeed = 5f;

    private Rigidbody2D rigidbody2d;
    private bool isMoving = false;
    private string keyHolding;

    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
        Freeze();
        SpeedLimit();
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

    void Freeze()
    {
        if (!isMoving)
        {
            rigidbody2d.constraints |= RigidbodyConstraints2D.FreezePositionX;
        }
    }

    void SpeedLimit()
    {
        if (rigidbody2d.velocity.magnitude > maxSpeed)
        {
            rigidbody2d.velocity = rigidbody2d.velocity.normalized * maxSpeed;
        }
    }
}