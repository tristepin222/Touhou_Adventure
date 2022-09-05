using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weatherController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject sunny;
    public GameObject rainy;
    public GameObject stormy;
    public GameObject foggy;
    public GameObject windy;
    public weatherController[] other;
    public bool isSame = false;
    public int rand;
    public bool over_ride = false;
    void Start()
    {
        other = FindObjectsOfType<weatherController>();
        if (over_ride)
        {
            if (!isSame)
            {
                rand = Random.Range(1, 3);
            }
            else
            {
                rand = other[1].rand;
            }
        }
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
            default:
                sunny.SetActive(true);
                break;
            

        }
    }

    // Update is called once per frame
    void Update()
    {
        
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
            default:
                return sunny;
               

        }
        return sunny;
    }
}
