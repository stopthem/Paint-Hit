using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public static int currentLevel;
    public static int ballCount;
    public static int totalCircles;
    public static int maxBallCount;

    public static Color currentColor;

    private void Awake()
    {
        PlayerPrefs.SetInt("C_Level", 1);

        if (PlayerPrefs.GetInt("FirstTime1", 0) == 0)
        {
            PlayerPrefs.SetInt("FirstTime1", 1);
            PlayerPrefs.SetInt("C_Level", 1);
        }

        UpgradeLevel();
    }

    private void Update()
    {
        UpgradeLevel();
        
    }

    private void UpgradeLevel()
    {
        currentLevel = PlayerPrefs.GetInt("C_Level");

        if (currentLevel == 1)
        {
            ballCount = 2;
            maxBallCount = ballCount;
            totalCircles = 2;
        }
        if (currentLevel == 2)
        {
            ballCount = 3;
            maxBallCount = ballCount;
            totalCircles = 3;
        }
        if (currentLevel == 3)
        {
            ballCount = 6;
            maxBallCount = ballCount;
            totalCircles = 5;
        }
        if (currentLevel == 4)
        {
            ballCount = 9;
            maxBallCount = ballCount;
            totalCircles = 8;
        }
        if (currentLevel == 5)
        {
            ballCount = 10;
            maxBallCount = ballCount;
            totalCircles = 10;
        }
    }
}
