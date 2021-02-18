using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    private Circle circle;
    private void OnCollisionEnter(Collision other)
    {
        circle = other.gameObject.GetComponent<Circle>();
        if (other.gameObject.CompareTag("red"))
        {
            other.gameObject.GetComponent<MeshRenderer>().enabled = true;
            other.gameObject.GetComponent<MeshRenderer>().material.color = Color.green;
            base.GetComponent<Rigidbody>().AddForce(Vector3.down * 50, ForceMode.Impulse);
            Destroy(base.gameObject, .5f);
        }
        else
        {
            GameObject splash = Instantiate(Resources.Load("splash1")) as GameObject;
            splash.transform.parent = other.gameObject.transform;
            Destroy(splash, .1f);
            other.gameObject.name = "color";
            other.gameObject.tag = "red";
            StartCoroutine(ChangeColorRoutine(other.gameObject));
        }
    }

    private IEnumerator ChangeColorRoutine(GameObject g)
    {
        yield return new WaitForSeconds(.1f);
        g.gameObject.GetComponent<MeshRenderer>().enabled = true;
        g.gameObject.GetComponent<MeshRenderer>().material.color = BallHandler.oneColor;
        Destroy(base.gameObject);
    }
}
