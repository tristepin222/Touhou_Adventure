using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
public class sceneManager : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] PlayerManagament player_managament;

    [SerializeField] GameObject vc;
    [SerializeField] Vector3 v;






    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneUnloaded += OnUnloadScene;
        SceneManager.sceneLoaded += OnloadScene;


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnloadScene(Scene scene, LoadSceneMode loadSceneMode)
    {
        
        if (scene.name == "Game" && GlobalControl.Instance.lastScene == "MainMenu")
        {
            
            GlobalControl.Instance.player.transform.position = GlobalControl.Instance.v3;
           
        }
        
    }
  private void  OnUnloadScene(Scene scene)
    {


        if (scene.name != "MainMenu")
        {
            if (!scene.name.Contains("Fight")){
                GlobalControl.Instance.lastScene = scene.name;
            }
        }
        
        
       

    }
    
    
}
