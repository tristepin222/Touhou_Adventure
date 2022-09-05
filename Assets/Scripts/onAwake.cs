using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
public class onAwake : NetworkBehaviour
{
    public bool create = true;
    GameObject[] musics;
    GameObject[] sounds;
    private bool hostinit = false;
    public bool init = true;
    private void Awake()
    {
   
        

        if (GlobalControl.Instance == null)
        {
           



            
        }
      
    }
    void Start()
    {
        
      
        if (dataStatic.Instance.client)
        {


            NetworkManager.Singleton.StartClient();

        }
        else
        {
            hostinit = true;
            NetworkManager.Singleton.StartHost();
        }
        ulong playerID = NetworkManager.Singleton.LocalClientId;
        if (NetworkManager.Singleton.ConnectedClients[playerID].PlayerObject)
        {
            if (GlobalControl.Instance != null)
            {
                GlobalControl.Instance.ui_hotbar.SetActive(true);
                GlobalControl.Instance.ui_inventory.SetActive(true);
                GlobalControl.Instance.player.GetComponent<culler>().culled = false;
                GlobalControl.Instance.player.GetComponent<culler>().cull();
                if (init)
                {
                    GlobalControl.Instance.UI_Objective.GetComponent<ObjectiveManager>().set();

                }
                GlobalControl.Instance.UI_Objective.GetComponent<ObjectiveManager>().check();
            }
            
        }

       

        musics = GameObject.FindGameObjectsWithTag("music");
        sounds = GameObject.FindGameObjectsWithTag("sound");
        foreach (GameObject music in musics)
        {
            AudioSource audio = music.GetComponent<AudioSource>();
            if (audio.volume != options.Instance.mAmount)
            {
                audio.volume = options.Instance.mAmount;
            }
        }
        foreach (GameObject sound in sounds)
        {
            AudioSource audio = sound.GetComponent<AudioSource>();
            if (audio != null)
            {
                if (audio.volume != options.Instance.sAmount)
                {
                    audio.volume = options.Instance.sAmount;
                }
            }
        }
    }

    
}
