using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float rotationSpeed = 130f;
    public static float rotationTime = 3f;
    public static int currentCircleNo;
    public static Color oneColor;

    [HideInInspector] public int circleHits;
    private int levelCount;

    private Ball m_ballScript;
    private UIHandler uiHandler;

    [HideInInspector] public int ballsCount;
    private int m_circleNo;

    private Color[] m_changingColors;

    public Material material;

    private bool spawnedLevel;

    private void Awake()
    {
        m_ballScript = GameObject.Find("DummyBall").GetComponent<Ball>();
        uiHandler = GameObject.Find("UICanvas").GetComponent<UIHandler>();
    }

    private void Start()
    {
        levelCount++;
        HandleStart();
    }

    private void Update()
    {
        CheckLevel();
    }

    private void CheckLevel()
    {

        if (LevelHandler.totalCircles == m_circleNo && circleHits == LevelHandler.hitPoint && !uiHandler.isLevelOver)
        {
            uiHandler.LevelOver();
        }

        if (circleHits == LevelHandler.hitPoint && !spawnedLevel && !uiHandler.isLevelOver)
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
        ballsCount = LevelHandler.ballCount;
        m_circleNo = 1;
        m_changingColors = ColorScript.colorArray;

        DefineCircleColor();

        CreateCircle();
    }

    public IEnumerator MakeANewCircleCoroutine(float timeToWait)
    {
        m_ballScript.canShoot = false;
        yield return new WaitForSeconds(timeToWait);

        spawnedLevel = true;
        MakeNewCircle();

        yield return new WaitForSeconds(timeToWait);
        ballsCount = LevelHandler.ballCount;
        m_ballScript.canShoot = true;
    }

    private void MakeNewCircle()
    {
        spawnedLevel = false;

        CircleEnd();

        GameObject[] circleArray = GameObject.FindGameObjectsWithTag("circle");

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

        CreateCircle();

        HurdlesColorChange();
    }

    private void CreateCircle()
    {
        DefineCircleColor();

        GameObject circleToSpawn = Instantiate(Resources.Load("round" + UnityEngine.Random.Range(1, 4).ToString())) as GameObject;
        circleToSpawn.transform.position = new Vector3(0, 20, 23);
        circleToSpawn.name = "Circle" + m_circleNo;

        currentCircleNo = m_circleNo;

        LevelHandler.currentColor = oneColor;

        HurdlesColorChange();
    }

    private void HurdlesColorChange()
    {
        GameObject circle = GameObject.Find("Circle" + m_circleNo);
        for (int i = 0; i < 24; i++)
        {
            if (circle.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>())
            {
                circle.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.color = oneColor;
            }
        }
    }

    private void DefineCircleColor()
    {
        int random = UnityEngine.Random.Range(1, 8);
        oneColor = m_changingColors[random];
        material.color = oneColor;
    }

    public static void CircleEnd()
    {
        GameObject circle = GameObject.Find("Circle" + currentCircleNo);
        if (circle != null)
        {
            for (int i = 0; i < 24; i++)
            {
                circle.transform.GetChild(i).gameObject.SetActive(false);
            }
            circle.transform.GetChild(24).gameObject.GetComponent<MeshRenderer>().material.color = oneColor;

            if (circle.GetComponent<iTween>())
            {
                circle.GetComponent<iTween>().enabled = false;
            }
        }
    }

    public void NewLevel()
    {
        StartCoroutine(NewLevelRoutine());
    }

    private IEnumerator NewLevelRoutine()
    {
        circleHits = 0;
        uiHandler.nextLevelButton.gameObject.SetActive(false);

        StartCoroutine(uiHandler.NewLevelScreen());

        levelCount = PlayerPrefs.GetInt("C_Level");
        levelCount++;
        PlayerPrefs.SetInt("C_Level", levelCount);
        yield return new WaitForSeconds(.1f);

        HandleStart();
    }
}
