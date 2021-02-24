using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIHandler : MonoBehaviour
{
    public TextMeshProUGUI currentCircleText;
    public TextMeshProUGUI totalCirclesText;
    public TextMeshProUGUI levelNumberText;

    public TextMeshProUGUI failLevelNumberText;

    public GameObject levelCompleteScreen;
    public GameObject gamePlayScreen;
    public GameObject failScreen;
    public GameObject dummyBall;
    public GameObject completeEffect;

    public Image[] balls;

    public Button nextLevelButton;

    [HideInInspector] public bool isLevelOver;

    public float timeToWaitAfterLevel;
    public float timeToWaitAfterRestart;

    private GameManager m_gameManager;

    private void Awake()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }


    private void Update()
    {
        currentCircleText.text = GameManager.currentCircleNo.ToString();
        totalCirclesText.text = LevelHandler.totalCircles.ToString();
        CheckBalls();
    }

    private void CheckBalls()
    {
        for (int i = 0; i < balls.Length; i++)
        {
            balls[i].enabled = false;
        }

        for (int j = 0; j < m_gameManager.ballsCount; j++)
        {
            balls[j].enabled = true;
            balls[j].color = GameManager.oneColor;
        }
    }

    public void LevelOver()
    {
        isLevelOver = true;
        StartCoroutine(LevelCompletedRoutine(timeToWaitAfterLevel));
    }

    public void LevelFailed()
    {
        StartCoroutine(LevelFailedRoutine(timeToWaitAfterLevel / 2));
    }

    private IEnumerator LevelFailedRoutine(float timeToWaitAfterFail)
    {

        yield return new WaitForSeconds(timeToWaitAfterFail);

        Hide(dummyBall);

        Show(failScreen);
        failLevelNumberText.text = LevelHandler.currentLevel.ToString();
        isLevelOver = false;
    }

    public IEnumerator RestartLevelRoutine()
    {

        yield return new WaitForSeconds(timeToWaitAfterRestart);

        Hide(failScreen);

        Show(gamePlayScreen);
        Show(dummyBall);
    }

    public IEnumerator NewLevelScreen()
    {
        yield return new WaitForSeconds(.2f);
        isLevelOver = false;

        Hide(levelCompleteScreen);
        Show(gamePlayScreen);
        Show(dummyBall);
    }

    private IEnumerator LevelCompletedRoutine(float wait)
    {
        GameManager.CircleEnd();

        completeEffect.SetActive(true);

        yield return new WaitForSeconds(wait);

        nextLevelButton.gameObject.SetActive(true);

        GameManager.DestroyCircles();

        completeEffect.SetActive(false);
        Hide(gamePlayScreen);
        Hide(dummyBall);

        Show(levelCompleteScreen);
        levelNumberText.text = LevelHandler.currentLevel.ToString();
    }

    private void Hide(GameObject g)
    {
        g.SetActive(false);
    }

    private void Show(GameObject g)
    {
        g.SetActive(true);
    }
}
