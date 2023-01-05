using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weatherController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sunny;
    public GameObject rainy;
    public GameObject stormy;
    public GameObject cloudy;
    public GameObject foggy;
    public GameObject windy;
    public weatherController[] other;
    public bool isSame = false;
    public int rand;
    public bool over_ride = false;
    public AudioClip audio;
    void Start()
    {
  
       
   
        other = FindObjectsOfType<weatherController>();
       
                rand = Random.Range(1, 4);
           
    
        if (options.Instance.sAmount > 0.4f)
        {
            rainy.GetComponent<AudioSource>().volume = options.Instance.sAmount - 0.4f;
        }
        if (sunny != null)
        {
            sunny.SetActive(false);
        }
        if (rainy != null)
        {
            rainy.SetActive(false);
        }
        if (stormy != null)
        {
            rainy.SetActive(false);
        }
        if (cloudy != null)
        {
            rainy.SetActive(false);
        }
        switch (rand)
        {
            case 1:
                if (sunny != null)
                {
                    sunny.SetActive(true);
                }
                break;
            case 2:
                if (rainy != null)
                {
                    rainy.SetActive(true);
                }
                break;
                case 3:
                if(stormy != null)
                {
                    stormy.SetActive(true);
                }
                break;
            case 4:
                if (cloudy != null)
                {
                    cloudy.SetActive(true);
                }
                break;
            default:
                sunny.SetActive(true);
                break;
            

        }
    }



     public GameObject  getWeather()
    {
        switch (rand)
        {
            case 1:
                if (sunny != null)
                {
                    return sunny;
                }
                break;
            case 2:
                if (rainy != null)
                {
                     return rainy;
                }
                break;
            case 3:
                if (stormy != null)
                {
                    stormy.SetActive(true);
                }
                break;
            case 4:
                if (cloudy != null)
                {
                    cloudy.SetActive(true);
                }
                break;
            default:
                return sunny;
               

        }
        return sunny;
    }
}
