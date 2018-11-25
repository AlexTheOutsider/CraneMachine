using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    public bool isHooked = false;
    public bool isPlaced = false;

    void OnTriggerEnter2D(Collider2D other)
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

    void OnTriggerExit2D(Collider2D other)
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