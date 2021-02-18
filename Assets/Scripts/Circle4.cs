using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle4 : MonoBehaviour
{
    private void Start()
    {
        iTween.MoveTo(base.gameObject, iTween.Hash(new object[]{
            "y",
            0,
            "easetype",
            iTween.EaseType.easeInOutQuad,
            "time",
            0.6,
            "OnComplete",
            "RotateCircle"
        }));
    }

    private void RotateCircle()
    {
        iTween.RotateBy(base.gameObject, iTween.Hash(new object[]{
            "y",
            .75f,
            "time",
            BallHandler.rotationTime,
            "easetype",
            iTween.EaseType.easeInOutQuad,
            "looptype",
            iTween.LoopType.pingPong,
            "delay",
            .5
        }));
    }
}
