using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;
public class BossTeleport : MonoBehaviour
{
    public AudioClip sound1;
    public AudioClip sound2;
    private bool once = false;
    public GameObject NPC;
    private Vector3 source;
    public bool kill = true;
    public string scene = "";
    public AudioSource AS;
    public float sound = 0.5f;
    public Volume volume;
    public Image effect;
    public Canvas cv;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    public void Teleport()
    {
        if (cv != null)
        {
            cv.worldCamera = GlobalControl.Instance.vc.GetComponent<Camera>();
        }
        if (!once)
        {
            
            source = this.transform.position;
            this.transform.position = GlobalControl.Instance.player.transform.position;
            AS.PlayOneShot(sound1, sound);
            if (NPC != null)
            {
                NPC.transform.position = source;
            }
            once = true;
            if (kill)
            {
                StartCoroutine(GlobalControl.Instance.player.GetComponent<PlayerManagament>().killl(sound2));
            }else if(scene != ""){
                StartCoroutine(load());
            }
           
        }
    }

    public IEnumerator load()
    {
        Time.timeScale = 0;
        ColorCurves CC = (ColorCurves)volume.profile.components[1];
        CC.active = true;
        if(effect != null)
        {
            effect.enabled = true;
        }
        if (volume != null)
        {
            LensDistortion LD = (LensDistortion)volume.profile.components[0];

            StartCoroutine(fade(LD));
            
        }

       



        yield return new WaitWhile(() => AS.isPlaying  );
        Time.timeScale = 1;
        SceneManager.LoadScene(scene);

    }
   private IEnumerator fade(LensDistortion LD)
    {
        for (float i = 0; i >= -1; i -= 0.01f)
        {
            // set color with i as alpha
            LD.intensity.Override(i);
            yield return null;
        }
        for (float i = LD.intensity.value; i <= 1; i += 0.002f)
        {
            // set color with i as alpha
            LD.intensity.Override(i);
            yield return null;
        }
        for (float i = LD.intensity.value; i >= 0; i -= 0.01f)
        {
            // set color with i as alpha
            LD.intensity.Override(i);
            yield return null;
        }
    }
}
