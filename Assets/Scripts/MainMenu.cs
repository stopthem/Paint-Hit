using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour
{
    public Sprite[] spriteArray;

    public Image image;

    private void Start()
    {
        image.sprite = spriteArray[Random.Range(0,4)];
    }
}
