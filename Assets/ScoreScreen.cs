using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using TMPro;
using System;
public class ScoreScreen : MonoBehaviour
{
    [SerializeField] GameObject effect;
    [SerializeField] typeWriterEffectAdvanced time;
    [SerializeField] typeWriterEffectAdvanced score;
    [SerializeField] TextMeshProUGUI note;
    [SerializeField] TextMeshProUGUI timel;
    [SerializeField] TextMeshProUGUI scorel;
    [SerializeField] TextMeshProUGUI notel;
    [SerializeField] List<string> scores = new List<string>();
    [SerializeField] loadingScreen loadingScreen;
    private int scoreAmount;
    private double timef = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        GlobalControl.Instance.player.GetComponent<PlayerManagament>().inFightScene = true;
        GlobalControl.Instance.player.GetComponent<PlayerManagament>().ScoreScreen = this;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timef += Time.deltaTime;
    }
    public void show()
    {
        
        this.GetComponent<Animator>().enabled = true;
        this.GetComponent<Canvas>().enabled = true;
    }
    public void addScore(int amount)
    {
        scoreAmount += amount;
    }
    public void OnAfterAnimation()
    {
        StartCoroutine(time.PlayText(TimeSpan.FromSeconds(timef).ToString(@"m\:ss")));
        int scoreTime = 10000 / (int)timef;
        addScore(scoreTime);
        StartCoroutine(score.PlayText(scoreAmount.ToString()));
        if(scoreAmount <= 100)
        {
            note.text = scores[0];
            notel.text = scores[0];
        }else if(scoreAmount <= 500)
        {
            note.text = scores[1];
            notel.text = scores[1];
        }
        else if (scoreAmount <= 1000)
        {
            note.text = scores[2];
            notel.text = scores[2];
        }
        else if (scoreAmount <= 5000)
        {
            note.text = scores[3];
            notel.text = scores[3];
        }
        else if (scoreAmount <= 10000)
        {
            note.text = scores[4];
            notel.text = scores[4];
        }

    }
    public void OnConfirm()
    {
        loadingScreen.sceneString = GlobalControl.Instance.lastScene;
        loadingScreen.startLoading();
    }

}
