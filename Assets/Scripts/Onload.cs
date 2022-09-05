using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
  using System;

public class Onload : MonoBehaviour
{
   
    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += SceneOnLoad;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void SceneOnLoad(Scene scene, LoadSceneMode mode)
    {

    }
}
