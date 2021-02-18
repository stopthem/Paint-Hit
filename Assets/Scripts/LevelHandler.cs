using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public static int currentLevel;
    public static int ballCount;
    public static int totalCircles;

    public static Color currentColor;

    private void Awake()
    {
        if (PlayerPrefs.GetInt("FirstTime1", 0) == 0)
        {
            PlayerPrefs.SetInt("FirstTime1", 1);
            PlayerPrefs.SetInt("C_Level", 1);
        }

        UpgradeLevel();
    }

    private void UpgradeLevel()
    {
        currentLevel = PlayerPrefs.GetInt("C_Level", 1);

        if (currentLevel == 1)
        {
            ballCount = 2;
            totalCircles = 2;
        }
        if (currentLevel == 2)
        {
            ballCount = 3;
            totalCircles = 3;
        }
        if (currentLevel == 3)
        {
            ballCount = 6;
            totalCircles = 5;
        }
        if (currentLevel == 4)
        {
            ballCount = 9;
            totalCircles = 8;
        }
        if (currentLevel == 5)
        {
            ballCount = 10;
            totalCircles = 10;
        }
    }
}
