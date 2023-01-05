using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System;
public class TimeClockManager : MonoBehaviour
{
    public Transform timeCircle;
    public DayManager dayManager;
    public TextMeshProUGUI textMeshProUGUI;
    public int hoursi;
    public int minutesi;
    public bool demo;
    public const float REAL_SECONDS_PER_INGAME_DAY = 1500f;
    private float time;
    
    private float day;
    private int currentDay;

    // Start is called before the first frame update
    void Start()
    {
       
        day = dataStatic.Instance.savedDay;
        currentDay = dataStatic.Instance.currentday;
        time = GlobalControl.Instance.time;
        DayManager dayManager2 = FindObjectOfType<DayManager>();
        dayManager = dayManager2;
        if (demo)
        {
            day = 0.4f;
        }
    }

    // Update is called once per frame
    void Update()
    {

        float hoursPerDay = 24f;
        float minutesPerHour = 60f;
        day += Time.deltaTime / REAL_SECONDS_PER_INGAME_DAY;
        dataStatic.Instance.savedDay = day;
      
        if (day >= 1)
        {
            day = 0;
            currentDay++;
        }
       
        if (currentDay >= 7)
        {
            currentDay = 0;
        }

        GlobalControl.Instance.currentDay = currentDay;
        if(dayManager != null)
        {
            dayManager.day = day;
        }
       
        float dayNormalised = day % 1;
        float rotationDegreesPerday = 360f;
        
        timeCircle.eulerAngles =  new Vector3(0,0, -dayNormalised * rotationDegreesPerday );
        hoursi = (int)Mathf.Floor(dayNormalised * hoursPerDay);
     
        minutesi = (int)Mathf.Floor(((dayNormalised * hoursPerDay) % 1) * minutesPerHour);
        if (GlobalControl.Instance != null)
        {
            GlobalControl.Instance.hour = hoursi;
            GlobalControl.Instance.minute = minutesi;
        }
        if (demo)
        {
            if(hoursi >= 18)
            {
                GlobalControl.Instance.ui_hotbar.GetComponentInChildren<Canvas>().enabled = false;
                GlobalControl.Instance.ui_life.GetComponent<Canvas>().enabled = false;
                GlobalControl.Instance.ui_money.GetComponentInChildren<Canvas>().enabled = false;
                GlobalControl.Instance.UI_Buttons.GetComponent<Canvas>().enabled = false;
                GlobalControl.Instance.UI_Objective.GetComponent<Canvas>().enabled = false;
                GlobalControl.Instance.UI_Exp.GetComponent<Canvas>().enabled = false;
                GlobalControl.Instance.UI_Time.GetComponent<Canvas>().enabled = false;
                Scene currentScene = SceneManager.GetActiveScene();
                if (currentScene.name != "MainMenu")
                {
                    SceneManager.LoadScene("MainMenu");
                }
            }
        }
        string hours = Mathf.Floor(dayNormalised * hoursPerDay).ToString("00");
        string minutes = Mathf.Floor(((dayNormalised * hoursPerDay) % 1) * minutesPerHour).ToString("00");
        textMeshProUGUI.text = hours + ":" + minutes;

    }
    public void SetHour(string value)
    {
        int intValue = int.Parse(value);

        float normlizedValue = Mathf.InverseLerp(0,24, intValue);
        
        day = normlizedValue;
        
    }
}
