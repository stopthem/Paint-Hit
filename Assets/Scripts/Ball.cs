using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameManager m_gameManager;
    private float m_speed = 100;

    public bool canShoot = true;

    public GameObject ball;

    private void Awake()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && canShoot)
        {
            HitBall();
        }
    }

    private void HitBall()
    {
        m_gameManager.ballsCount--;

        if (ball != null)
        {
            GameObject ballToThrow = Instantiate<GameObject>(ball, new Vector3(0, 0, -8), Quaternion.identity);
            ballToThrow.GetComponent<MeshRenderer>().material.color = GameManager.oneColor;
            ballToThrow.GetComponent<Rigidbody>().AddForce(Vector3.forward * m_speed, ForceMode.Impulse);
        }
    }
}
