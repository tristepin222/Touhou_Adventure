using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization;
using UnityEngine.Localization.SmartFormat.Extensions;
using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
public class TypeWriterEffect : MonoBehaviour
{
    public float delay = 0.05f;
    public string text;
    private string currentText;
    public Text textt;
    public GameObject panel;
    public bool bypass;
    public bool leave;
    public string name;
    public reactionManager rm;
    public Canvas canva;
    public dialoguebubbles db;
   public bool started = false;
    public Coroutine texts;
     bool finish = false;
    public AudioClip AC;
    public AudioSource AS;
    private void Start()
    {
       
        panel.SetActive(false);
        AS.volume = options.Instance.sAmount;
    }
    public void startCoroutine(string s)
    {
        panel.SetActive(false);
        if (s.Contains("-"))
        {
            if (bypass)
            {
                s = s.Replace("-", name);
                
            }
            else
            {
                s = s.Replace("-", dataStatic.Instance.characterName);
            }
        }
        text = s;


        started = true;
        if (this.gameObject.activeInHierarchy)
        {
            texts = StartCoroutine(ShowText());
         }



    }

    
    
    IEnumerator ShowText()
    {

        yield return new WaitUntil(() => this.gameObject.activeSelf == true);
        this.gameObject.SetActive(true);
        textt.text = "";
        finish = false;
        int i = 0;
        
        foreach (char c in text) {
           
            textt.text += c;
            
            AS.Play();
            
            yield return new WaitForSeconds(delay);
            AS.Stop();
            i++;
        }

        panel.SetActive(true);
        bypass = false;
        started = false;
        finish = true;
        
        if(i >= text.Length)
        {
            onFinsh.Invoke(true);
        }
        StartCoroutine(doleave());
    }

     IEnumerator doleave()
    {
        if (rm != null)
        {
          
            
        }
        else
        {
            yield return new WaitForSeconds(delay*4);
         
            this.gameObject.SetActive(false);
            if (db != null)
            {
                db.next();
            }
        }
    }
    public IEnumerator exit()
    {
        yield return new WaitUntil(() => finish);

        this.gameObject.SetActive(false);
        if (rm != null)
        {
            rm.leave = false;
        }
    }
    private void OnDisable()
    {
        StopAllCoroutines();
        bypass = false;
        started = false;
        finish = false;
        text = "";
        if (rm != null)
        {
            rm.leave = false;
        }
    }
    [Serializable]
    public class ValueChangedEvent : UnityEvent<bool> { }
    [FormerlySerializedAs("onValueChange")]
    [SerializeField]
    private ValueChangedEvent OnFinsh = new ValueChangedEvent();

    public ValueChangedEvent onFinsh
    {
        get { return OnFinsh; }
        set { OnFinsh = value; }
    }
}
