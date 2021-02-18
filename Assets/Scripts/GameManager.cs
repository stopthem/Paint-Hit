using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float rotationSpeed = 75f;
    public static float rotationTime = 3f;
    public static int currentCircleNo;
    public static Color oneColor;

    public int circleHits;
    private int levelCount;

    private Ball m_ballScript;

    [HideInInspector] public int ballsCount;
    private int m_circleNo;

    private Color[] m_changingColors;

    public Material material;

    private bool spawnedLevel;

    private void Awake()
    {
        m_ballScript = GameObject.Find("DummyBall").GetComponent<Ball>();
    }

    private void Start()
    {
        HandleStart();
    }

    private void Update()
    {
        if (circleHits == LevelHandler.maxBallCount && !spawnedLevel)
        {
            SpawnLevel();
        }
    }

    private void SpawnLevel()
    {
        circleHits = 0;
        StartCoroutine(MakeANewCircleCoroutine(.4f));
    }

    private void HandleStart()
    {
        m_changingColors = ColorScript.colorArray;
        oneColor = m_changingColors[0];
        material.color = oneColor;

        GameObject circleToSpawn = Instantiate(Resources.Load("round" + UnityEngine.Random.Range(1, 4).ToString())) as GameObject;
        circleToSpawn.transform.position = new Vector3(0, 20, 23);
        circleToSpawn.name = "Circle" + m_circleNo;

        ballsCount = LevelHandler.ballCount;

        currentCircleNo = m_circleNo;

        LevelHandler.currentColor = oneColor;
    }

    public IEnumerator MakeANewCircleCoroutine(float timeToWait)
    {
        m_ballScript.canShoot = false;
        yield return new WaitForSeconds(timeToWait);

        spawnedLevel = true;
        MakeANewCircle();

        yield return new WaitForSeconds(timeToWait);
        m_ballScript.canShoot = true;
    }

    private void MakeANewCircle()
    {
        spawnedLevel = false;
        levelCount = PlayerPrefs.GetInt("C_Level");
        levelCount++;

        PlayerPrefs.SetInt("C_Level", levelCount);
        GameObject[] circleArray = GameObject.FindGameObjectsWithTag("circle");
        GameObject circle = GameObject.Find("Circle" + m_circleNo);

        for (int i = 0; i < 24; i++)
        {
            circle.transform.GetChild(i).gameObject.SetActive(false);
        }
        circle.transform.GetChild(24).gameObject.GetComponent<MeshRenderer>().material.color = oneColor;

        if (circle.GetComponent<iTween>())
        {
            circle.GetComponent<iTween>().enabled = false;
        }

        foreach (var target in circleArray)
        {
            iTween.MoveBy(target, iTween.Hash(new object[]{
                "y",
                -2.98f,
                "easetype",
                iTween.EaseType.spring,
                "time",
                .5
            }));
        }

        m_circleNo++;
        currentCircleNo = m_circleNo;

        GameObject circleToSpawn = Instantiate(Resources.Load("round" + UnityEngine.Random.Range(1, 5).ToString())) as GameObject;
        circleToSpawn.transform.position = new Vector3(0, 20, 23);
        circleToSpawn.name = "Circle" + m_circleNo;

        ballsCount = LevelHandler.ballCount;


        oneColor = m_changingColors[m_circleNo];
        material.color = oneColor;
        LevelHandler.currentColor = oneColor;
    }

}
