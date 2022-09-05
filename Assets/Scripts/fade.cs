using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class fade : MonoBehaviour
{
    [SerializeField] public string scene;
    [SerializeField] private Image imageEffect;
    [SerializeField] private float transtionSpeed;
    [SerializeField] private List<Sprite> imagtes = new List<Sprite>();
    public bool shouldReveal;
    public bool goingInFight = false;

    // Start is called before the first frame update
    void Awake()
    {
        imageEffect.material.SetFloat("_cutoff", 1.1f);

        this.GetComponent<Canvas>().worldCamera = Camera.main;
        imageEffect.material.mainTexture = imagtes[Random.Range(0, imagtes.Count-1)].texture;
    }


    private void Update()
    {
        if (shouldReveal)
        {
       
            imageEffect.material.SetFloat("_cutoff", Mathf.MoveTowards(imageEffect.material.GetFloat("_cutoff"), -0.1f, transtionSpeed * Time.deltaTime));
        }
        if (imageEffect.material.GetFloat("_cutoff") <= -0.1f)
        {
        if (!goingInFight)
            {
                GlobalControl.Instance.lastScene = scene;
            }
            SceneManager.LoadScene(scene);
            shouldReveal = false;
        }
    }
    
}
