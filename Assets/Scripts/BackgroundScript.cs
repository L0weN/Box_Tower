using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BackgroundScript : MonoBehaviour
{
    [SerializeField] private RawImage _backgroundImg;
    private float _x = 0.1f , _y = 0f;
    void Update()
    {
        _backgroundImg.uvRect = new Rect(_backgroundImg.uvRect.position + new Vector2(_x, _y) * Time.deltaTime, _backgroundImg.uvRect.size);
    }
}


public abstract class Merhaba
{
    public float x;
    public abstract void MerhabaYaz();

    public virtual void SelamYaz()
    {
        x = 5;
    }
}

public class Selam : Merhaba
{
    public override void MerhabaYaz()
    {
        Debug.Log("Selam");
    }
}