using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class loadScene : MonoBehaviour
{
    [SerializeField] public string scene;
    [SerializeField] Collider2D collider2;
    [SerializeField] loadingScreen loadingScreen;

    void Start()
    {
        if (GlobalControl.Instance != null)
        {
            GlobalControl.Instance.once[0] = false;
        }
        if(loadingScreen == null)
        {
            GameObject gameobject = GameObject.FindGameObjectWithTag("loadingScene");
            loadingScreen = gameobject.GetComponent<loadingScreen>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            
                GlobalControl.Instance.lastScene = scene;
         
            loadingScreen.sceneString = scene;
            loadingScreen.startLoading();

        }
    }
   
}
