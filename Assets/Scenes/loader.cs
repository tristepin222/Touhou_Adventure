using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
public class loader : MonoBehaviour
{
    [SerializeField] public Transform transform;
   
    [SerializeField] public Vector3 v;
    private GameObject[] musics;
    private GameObject[] sounds;
    public DontDestroyOnLoad reff;
    public bool spe = false;
    private string scenename;
    private void Awake()
    {
        
        SceneManager.sceneLoaded += OnloadScene;
        SceneManager.sceneUnloaded += OnUnloadScene;
    }
    private void Start()
    {
       
    }
    private void OnloadScene(Scene scene, LoadSceneMode loadScene)
    {
        string lastscene;
        if (this != null)
        {
            if (GlobalControl.Instance != null)
            {
                if (GlobalControl.Instance.lastScene != "MainMenu" && GlobalControl.Instance.lastScene != "")
                {

                    lastscene = GlobalControl.Instance.lastScene;

                    GameObject b = GameObject.Find(lastscene);
                    if (lastscene != "" || b != null)
                    {
                        if (loadScene != LoadSceneMode.Additive)
                        {
                            if (b != null)
                            {
                                GlobalControl.Instance.player.transform.position = b.transform.position;
                            }
                            GlobalControl.Instance.ui_hotbar.transform.GetChild(0).GetComponent<Canvas>().enabled = true;
                            GlobalControl.Instance.UI_Buttons.transform.GetComponent<Canvas>().enabled = true;
                            GlobalControl.Instance.UI_Exp.transform.GetComponent<Canvas>().enabled = true;
                            GlobalControl.Instance.ui_life.transform.GetComponent<Canvas>().enabled = true;
                            GlobalControl.Instance.player.GetComponent<UnityEngine.Rendering.Universal.Light2D>().enabled = false;
                            GlobalControl.Instance.vc.GetComponent<Camera>().orthographicSize = 5;
                            GlobalControl.Instance.player.GetComponent<PlayerManagament>().disabled = false;
                        }
                    }
                }
                else if (GlobalControl.Instance.lastScene == "")
                {

                    GameObject b = GameObject.Find("MainMenu");
                    if (b != null)
                    {
                        if (loadScene != LoadSceneMode.Additive)
                        {
                            if (b != null)
                            {
                                GlobalControl.Instance.player.transform.position = b.transform.position;
                            }

                        }
                    }
                }

            }
            

        }

    }
    private void OnUnloadScene(Scene scene)
    {
      
        if (scene.name != "MainMenu" )
        {
            if (!scene.name.Contains("Fight"))
            {
                GlobalControl.Instance.lastScene = scene.name;
                GlobalControl.Instance.vc.GetComponent<CinemachineVirtualCamera>().m_Lens.OrthographicSize = 5;
            }
        }
        
    }
    private void Update()
    {
       
        if(GlobalControl.Instance != null) {
            if (!GlobalControl.Instance.canTP)
            {
                GameObject b = GameObject.Find("MainMenu");
                if (b != null)
                {
                    GlobalControl.Instance.player.transform.position = b.transform.position;
                }
                GlobalControl.Instance.canTP = true;
            }
        }
    }
}
