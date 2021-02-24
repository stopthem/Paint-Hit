using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Circle m_circle;
    private GameManager m_gameManager;
    private AudioManager m_audioManager;
    private void Awake()
    {
        m_gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        m_audioManager = GameObject.Find("GameManager").GetComponent<AudioManager>();
    }
    private void OnCollisionEnter(Collision other)
    {
        m_circle = other.gameObject.GetComponent<Circle>();
        if (other.gameObject.CompareTag("hit") || other.gameObject.GetComponent<MeshRenderer>().enabled == true)
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = true;
            base.GetComponent<Rigidbody>().AddForce(Vector3.down * 50, ForceMode.Impulse);
            Destroy(base.gameObject, .5f);
        }
        else
        {
            m_audioManager.PlayHitSound();
            m_gameManager.circleHits++;
            other.gameObject.name = "hitbyball";
            other.gameObject.tag = "hit";
            ChangeColor(other.gameObject);
        }
    }

    private void ChangeColor(GameObject g)
    {
        g.gameObject.GetComponent<MeshRenderer>().enabled = true;
        g.gameObject.GetComponent<MeshRenderer>().material.color = GameManager.oneColor;
        Destroy(base.gameObject);
    }
}
