﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextFlicker : MonoBehaviour
{
    public float intervalTime = 1f;

    private float timeEllapsed = 0f;
    private Text welcomeText;
    private bool direction = false; //0 is up, 1 is down
    private float alpha;

    private void Start()
    {
        welcomeText = GetComponent<Text>();
    }

    private void Update()
    {
        if (timeEllapsed < intervalTime)
        {
            timeEllapsed += Time.deltaTime;
            if (direction)
                alpha = Mathf.Lerp(welcomeText.color.a, 1f, Time.deltaTime);
            else
                alpha = Mathf.Lerp(welcomeText.color.a, 0.3f, Time.deltaTime);
        }
        else
        {
            timeEllapsed = 0;
            direction = !direction;
        }

        welcomeText.color = new Color(0, 0, 0, alpha);
    }
}