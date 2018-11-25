using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    public GameController GameController;

    public bool isHooked = false;
    public bool isActivated = false;

    void Update()
    {
/*		if (!isHooked)
		{
			GameController.ScoreUpdate(Mathf.RoundToInt(transform.position.y * 10f));
		}*/
        //print(isHooked);
        //print(isActivated);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Crane"))
        {
            isHooked = true;
        }

        if (other.transform.CompareTag("Platform"))
        {
            isActivated = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.transform.CompareTag("Crane"))
        {
            isHooked = false;
        }

        if (other.transform.CompareTag("Platform"))
        {
            isActivated = false;
        }
    }
}