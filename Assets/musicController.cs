using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class musicController : MonoBehaviour
{
    public AudioClip audioClipSlow;
    float time1 = 0;
    public AudioClip audioClipFast;
    float time2 = 0;
    bool cr_running = false;
   public AudioSource[] music;
    bool playing1 = false;
    bool playing2 = false;
    public bool manual = false;
    // Start is called before the first frame update
    GameObject game;
    void Start()
    {
        if (!manual)
        {
            game = GameObject.FindGameObjectWithTag("music");

            music = game.GetComponents<AudioSource>();
        }
        music[0].clip = audioClipSlow;
        music[0].Play();
        music[1].clip = audioClipFast;
        music[1].Play();
        music[1].volume = 0f;



    }

    // Update is called once per frame
    void Update()
    {
        if (!manual)
        {
            GameObject enemiProj = GameObject.FindGameObjectWithTag("EnemiProjectile");
            GameObject game = GameObject.FindGameObjectWithTag("music");
            music = game.GetComponents<AudioSource>();


            if (enemiProj != null)
            {

                StartCoroutine(FadeOut(music[0], 1));
                if (!playing2)
                {



                    StartCoroutine(FadeIn(music[1], 1, false, true));



                }
                time2 = music[1].time;
            }
            else
            {
                StartCoroutine(FadeOut(music[1], 1));
                if (!playing1)
                {




                    StartCoroutine(FadeIn(music[0], 1, true, true));



                }
                time1 = music[0].time;
            }
        }
    }
    public void fadein()
    {
      StartCoroutine(FadeOut(music[0], 1));
      StartCoroutine(FadeIn(music[1], 1, false, true));
   }
    public void fadeout()
    {
       StartCoroutine(FadeOut(music[1], 1));
       StartCoroutine(FadeIn(music[0], 1, true, true));
    }
    private IEnumerator FadeOut(AudioSource audioSource, float FadeTime)
    {
       
        float startVolume = audioSource.volume;

        while (audioSource.volume > 0.001f)
        {
            audioSource.volume -= startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }


       
    }
    private IEnumerator FadeIn(AudioSource audioSource, float FadeTime, bool isslow, bool playing = false)
    {
        if (playing)
        {
            if (isslow)
            {
                playing1 = true;
                playing2 = false;
            }
            else
            {
                playing1 = false;
                playing2 = true;
            }
        }
        float startVolume = 0.1f;

        while (audioSource.volume <= options.Instance.mAmount)
        {
            audioSource.volume += startVolume * Time.deltaTime / FadeTime;

            yield return null;
        }

    }
}
