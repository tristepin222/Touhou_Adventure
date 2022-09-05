using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
 
public class healthsystem : MonoBehaviour
{
    public Image bossImage;
    public Image bar;
    public Image bar2;
    public Sprite sprite;
    public Slider slider;
    public float healthvalue = 0;
    private void Start()
    {
        if(bossImage != null)
        {
            bossImage.sprite = sprite;
        }
    }
    public void healthChange(float healthvalue, float maxHealth = 0)
    {
        if (slider != null)
        {
            slider.value = healthvalue / maxHealth;
        }
        else
        {
            float amount = (healthvalue / maxHealth);
            bar.fillAmount = amount;
        }
    }
}
