using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slide : MonoBehaviour
{
   public RectTransform rect1;
   public RectTransform rect2;
    public float rows;
    float refference;
    void Start()
    {
         refference = rect1.anchoredPosition.y;
         refference += 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
   public void valueChanged(float value)
    {
        rect1.anchoredPosition = new Vector2(rect1.anchoredPosition.x, refference * rows * value );
    }
}
