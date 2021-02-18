using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHandler : MonoBehaviour
{
    public static float rotationSpeed = 75f;
    public static float rotationTime = 3f;
    public static int currentCircleNo;
    public static Color oneColor;
    public GameObject ball;

    private float speed = 100;

    private int ballsCount;
    private int circleNo;

    private Color[] changingColors;

    public SpriteRenderer spriteRenderer;
    public Material material;

    private void Start()
    {
        ResetGame();
    }

    private void ResetGame()
    {
        changingColors = ColorScript.colorArray;
        oneColor = changingColors[0];
        spriteRenderer.color = oneColor;
        material.color = oneColor;
        
        GameObject circleToSpawn = Instantiate(Resources.Load("round" + UnityEngine.Random.Range(1, 4).ToString())) as GameObject;
        circleToSpawn.transform.position = new Vector3(0, 20, 23);
        circleToSpawn.name = "Circle" + circleNo;

        ballsCount = LevelHandler.ballCount;

        currentCircleNo = circleNo;
        LevelHandler.currentColor = oneColor;
    }

    public void HitBall()
    {
        if (ballsCount <= 1)
        {
            base.Invoke("MakeANewCircle", .4f);

        }
        ballsCount--;

        GameObject ballToThrow = Instantiate<GameObject>(ball, new Vector3(0, 0, -8), Quaternion.identity);
        ballToThrow.GetComponent<MeshRenderer>().material.color = oneColor;
        ballToThrow.GetComponent<Rigidbody>().AddForce(Vector3.forward * speed, ForceMode.Impulse);


    }

    private void MakeANewCircle()
    {
        GameObject[] circleArray = GameObject.FindGameObjectsWithTag("circle");
        GameObject circle = GameObject.Find("Circle" + circleNo);
        for (int i = 0; i < 24; i++)
        {
            circle.transform.GetChild(i).gameObject.SetActive(false);
        }
        circle.transform.GetChild(24).gameObject.GetComponent<MeshRenderer>().material.color = BallHandler.oneColor;

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

        circleNo++;
        currentCircleNo = circleNo;

        GameObject circleToSpawn = Instantiate(Resources.Load("round" + UnityEngine.Random.Range(1, 5).ToString())) as GameObject;
        circleToSpawn.transform.position = new Vector3(0, 20, 23);
        circleToSpawn.name = "Circle" + circleNo;

        ballsCount = LevelHandler.ballCount;

        
        oneColor = changingColors[circleNo];
        spriteRenderer.color = oneColor;
        material.color = oneColor;
        LevelHandler.currentColor = oneColor;
    }

}
