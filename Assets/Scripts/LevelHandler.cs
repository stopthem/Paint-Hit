using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelHandler : MonoBehaviour
{
    public static int currentLevel;
    public static int ballCount;
    public static int totalCircles;
    public static int hitPoint;

    public static Color currentColor;

    private void Awake()
    {
        PlayerPrefs.SetInt("C_Level", 1);
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
            ballCount = 3;
            hitPoint = 2;
            totalCircles = 2;
        }
        if (currentLevel == 2)
        {
            ballCount = 5;
            hitPoint = 3;
            totalCircles = 4;
        }
        if (currentLevel == 3)
        {
            ballCount = 8;
            hitPoint = 5;
            totalCircles = 6;
        }
        if (currentLevel == 4)
        {
            ballCount = 10;
            hitPoint = 7;
            totalCircles = 8;
        }
        if (currentLevel == 5)
        {
            ballCount = 10;
            hitPoint = 8;
            totalCircles = 10;
        }
    }
}
