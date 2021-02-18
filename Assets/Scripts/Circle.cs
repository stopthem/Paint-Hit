using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    public int hitCount;
    private void Start()
    {
        iTween.MoveTo(base.gameObject, iTween.Hash(new object[]{
            "y",
            0,
            "easetype",
            iTween.EaseType.easeInCirc,
            "time",
            0.2,
            "OnComplete",
            "RotateCircle"
        }));
    }

    private void RotateCircle()
    {
        iTween.RotateBy(base.gameObject, iTween.Hash(new object[]{
            "y",
            .8,
            "time",
            BallHandler.rotationTime,
            "easetype",
            iTween.EaseType.easeInOutQuad,
            "looptype",
            iTween.LoopType.pingPong,
            "delay",
            .3
        }));
    }
}
