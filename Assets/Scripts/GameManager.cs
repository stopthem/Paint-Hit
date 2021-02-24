using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float rotationSpeed = 150f;
    public static float rotationTime = 3f;
    public static int currentCircleNo;
    public static Color oneColor;

    [HideInInspector] public int circleHits;
    private int levelCount = 1;

    private Ball m_ballScript;
    private UIHandler m_uiHandler;
    private Background m_background;
    private AudioManager m_audioManager;

    [HideInInspector] public int ballsCount;
    private int m_circleNo;

    private Color[] m_changingColors;

    public Material material;

    private bool spawnedLevel;
    private bool isFailed;

    private void Awake()
    {
        m_ballScript = GameObject.Find("DummyBall").GetComponent<Ball>();
        m_uiHandler = GameObject.Find("UICanvas").GetComponent<UIHandler>();
        m_background = GameObject.Find("BackgroundCanvas").GetComponent<Background>();
        m_audioManager = GetComponent<AudioManager>();
    }

    private void Start()
    {
        HandleStart();
    }

    private void Update()
    {
        CheckLevel();
    }

    private void CheckLevel()
    {
        if (LevelHandler.totalCircles == m_circleNo && !m_uiHandler.isLevelOver)
        {
            m_uiHandler.isLevelOver = true;
            StartCoroutine(IsLevelCompletedRoutine());
        }

        if (ballsCount == 0 && !isFailed)
        {
            isFailed = true;
            m_ballScript.canShoot = false;
            StartCoroutine(IsLevelFailedRoutine());
        }

        if (circleHits == LevelHandler.hitPoint && !spawnedLevel && !m_uiHandler.isLevelOver)
        {
            SpawnLevel();
        }
    }

    private IEnumerator IsLevelCompletedRoutine()
    {
        yield return new WaitForSeconds(.5f);
        if (circleHits == LevelHandler.hitPoint)
        {
            m_ballScript.canShoot = false;
            m_uiHandler.LevelOver();
            m_audioManager.PlayCompleteSound();
        }
        else
        {
            m_uiHandler.isLevelOver = false;
        }
    }

    private IEnumerator IsLevelFailedRoutine()
    {
        yield return new WaitForSeconds(.3f);
        if (circleHits < LevelHandler.hitPoint)
        {
            KillTween(true);
            m_audioManager.PlayFailSound();

            m_ballScript.canShoot = false;
            m_uiHandler.LevelFailed();
        }
        else
        {
            isFailed = false;
            m_ballScript.canShoot = true;
        }
    }

    private void SpawnLevel()
    {
        StartCoroutine(MakeANewCircleCoroutine(.3f));
    }

    private void HandleStart()
    {
        spawnedLevel = false;
        ballsCount = LevelHandler.ballCount;
        m_circleNo = 1;
        m_changingColors = ColorScript.colorArray;

        DefineCircleColor();

        CreateCircle();
    }

    public IEnumerator MakeANewCircleCoroutine(float timeToWait)
    {
        m_ballScript.canShoot = false;
        spawnedLevel = false;
        yield return new WaitForSeconds(timeToWait);

        if (spawnedLevel == false)
        {
            MakeNewCircle();
        }
        ballsCount = LevelHandler.ballCount;
        yield return new WaitForSeconds(timeToWait);
        spawnedLevel = false;
        circleHits = 0;

        m_ballScript.canShoot = true;
    }

    private void MakeNewCircle()
    {
        spawnedLevel = true;
        CircleEnd();

        GameObject[] circleArray = GameObject.FindGameObjectsWithTag("circle");
        if (circleArray != null)
        {
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
        }
        m_circleNo++;
        CreateCircle();

        HurdlesColorChange();
    }

    private void CreateCircle()
    {
        DefineCircleColor();

        GameObject circleToSpawn = Instantiate(Resources.Load("round" + UnityEngine.Random.Range(1, 4).ToString())) as GameObject;
        if (circleToSpawn != null)
        {
            circleToSpawn.transform.position = new Vector3(0, 20, 23);
            circleToSpawn.name = "Circle" + m_circleNo;

            currentCircleNo = m_circleNo;

            LevelHandler.currentColor = oneColor;

            HurdlesColorChange();
        }
    }

    private void DefineCircleColor()
    {
        int random = UnityEngine.Random.Range(1, 8);
        oneColor = m_changingColors[random];
        material.color = oneColor;
    }

    private void HurdlesColorChange()
    {
        GameObject circle = GameObject.Find("Circle" + m_circleNo);
        if (circle != null)
        {
            for (int i = 0; i < 24; i++)
            {
                if (circle.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>())
                {
                    circle.transform.GetChild(i).gameObject.GetComponent<MeshRenderer>().material.color = oneColor;
                }
            }
        }
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

    public static void DestroyCircles()
    {
        GameObject[] circleArray = GameObject.FindGameObjectsWithTag("circle");
        if (circleArray != null)
        {
            foreach (var circle in circleArray)
            {
                Destroy(circle);
            }
        }
    }

    private void KillTween(bool isEnabled)
    {
        GameObject circle = GameObject.Find("Circle" + m_circleNo);
        if (circle != null)
        {
            if (circle.GetComponent<iTween>())
            {
                circle.GetComponent<iTween>().enabled = !isEnabled;
            }
        }
    }
    public void RestartGame()
    {
        StartCoroutine(RestartGameRoutine());
    }

    private IEnumerator RestartGameRoutine()
    {
        circleHits = 0;

        levelCount = PlayerPrefs.GetInt("C_Level");
        levelCount = 1;
        PlayerPrefs.SetInt("C_Level", levelCount);

        yield return new WaitForSeconds(.1f);
        DestroyCircles();
        StartCoroutine(m_uiHandler.RestartLevelRoutine());

        HandleStart();

        m_ballScript.canShoot = true;
    }

    public void LevelFailedG()
    {
        StartCoroutine(LevelFailedGRoutine());
    }

    private IEnumerator LevelFailedGRoutine()
    {
        circleHits = 0;

        levelCount = PlayerPrefs.GetInt("C_Level");
        levelCount = 1;
        PlayerPrefs.SetInt("C_Level", levelCount);

        yield return new WaitForSeconds(.1f);

        StartCoroutine(m_uiHandler.RestartLevelRoutine());

        HandleStart();

        m_ballScript.canShoot = true;
    }

    public void NewLevel()
    {
        StartCoroutine(NewLevelRoutine());
    }

    private IEnumerator NewLevelRoutine()
    {
        circleHits = 0;
        m_uiHandler.nextLevelButton.gameObject.SetActive(false);

        StartCoroutine(m_uiHandler.NewLevelScreen());

        yield return new WaitForSeconds(.1f);

        levelCount = PlayerPrefs.GetInt("C_Level");
        levelCount++;
        PlayerPrefs.SetInt("C_Level", levelCount);

        HandleStart();
        m_background.ChangeBackground();
        m_ballScript.canShoot = true;
    }

    public void BuyBalls()
    {
        ballsCount = ballsCount + 3;
        StartCoroutine(m_uiHandler.RestartLevelRoutine());
        m_audioManager.PlayCompleteSound();
        KillTween(false);
        m_ballScript.canShoot = true;
    }

}
