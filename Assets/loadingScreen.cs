using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class loadingScreen : MonoBehaviour
{
    [SerializeField] public string sceneString;
    [SerializeField] public GameObject slider;
    [SerializeField] private float transtionSpeed;
    [SerializeField] private Animator animator;
    [SerializeField] private List<loadScreenMessage> loadScreenMessages = new List<loadScreenMessage>();
    private Slider slider_s;
    public bool shouldReveal;
  

    void Start()
    {
   

        
        this.GetComponent<Canvas>().worldCamera = Camera.main;
       
        slider_s = slider.GetComponent<Slider>();
        
    }
    public void startLoading()
    {
        if (GlobalControl.Instance != null)
        {
            GlobalControl.Instance.isloading = true;
        }
        animator.enabled = true;
    }
    
    // Update is called once per frame
    public IEnumerator LoadAsyncOperation()
    {
        Instantiate(loadScreenMessages[Random.Range(0, loadScreenMessages.Count-1)].gameObject, this.transform);
       
        slider.SetActive(true);
        if (GlobalControl.Instance != null)
        {
            if (GlobalControl.Instance.player.GetComponent<PlayerManagament>().inFightScene)
            {
                GlobalControl.Instance.player.transform.position = GlobalControl.Instance.previousPos;
            }
        }
        AsyncOperation scene;
       
            scene = SceneManager.LoadSceneAsync(sceneString);
      
        while (scene.progress < 1)
        {
            slider_s.value = scene.progress;
            yield return new WaitForEndOfFrame();
        }

    }
}
