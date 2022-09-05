using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class sceneloading : MonoBehaviour
{
    [SerializeField] public GameObject slider;
    private Slider slider_s;


    void Start()
    {
         slider_s = slider.GetComponent<Slider>();
        StartCoroutine(LoadAsyncOperation());
    }

    // Update is called once per frame
    IEnumerator LoadAsyncOperation()
    {
        
        AsyncOperation scene;
        if (GlobalControl.Instance != null)
        {
             scene = SceneManager.LoadSceneAsync(GlobalControl.Instance.lastScene);
        }
        else
        {
            scene = SceneManager.LoadSceneAsync("PlayerHouseWorld");
        }
        while (scene.progress < 1)
        {
            slider_s.value = scene.progress;
            yield return new WaitForEndOfFrame();
        }
       
    }
}
