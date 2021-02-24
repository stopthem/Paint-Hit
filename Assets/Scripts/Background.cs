using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Background : MonoBehaviour
{
    public Sprite[] spriteArray;

    public Image image;

    private void Start()
    {
        ChangeBackground();
    }

    public void ChangeBackground()
    {
        image.sprite = spriteArray[Random.Range(0, 4)];
    }
}
