using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    public bool isHooked = false;
    public bool isPlaced = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Crane"))
        {
            isHooked = true;
        }
        if (other.transform.CompareTag("Platform"))
        {
            isPlaced = true;
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        OnTriggerEnter2D(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Crane"))
        {
            isHooked = false;
        }
        if (other.transform.CompareTag("Platform"))
        {
            isPlaced = false;
        }
    }
}