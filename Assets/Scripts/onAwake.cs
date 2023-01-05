using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Pathfinding;
public class onAwake : NetworkBehaviour
{
    public bool create = true;
    public bool SetAIPlayer;
    public bool giveSuccess;
    public string success;
    public string richPresenceKey;
    public string richPresenceValue;
    GameObject[] musics;
    GameObject[] sounds;
    private bool hostinit = false;
    public bool init = true;
    private SuccessManager successManager;
    private RichPresenceManager RichPresenceManager;
    private ulong playerID;
    private void Awake()
    {
   
        

      
      
    }
    private void Start()
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
         playerID = NetworkManager.Singleton.LocalClientId;
        if (GlobalControl.Instance != null)
        {

            GlobalControl.Instance.player.GetComponent<culler>().culled = false;

            initAwake();


        }


    }
    public void initAwake()
    {
        successManager = FindObjectOfType<SuccessManager>();
        RichPresenceManager = FindObjectOfType<RichPresenceManager>();
       
        if (successManager != null)
        {
            if (giveSuccess)
            {
                successManager.SetAchievement(success);
            }
            if(richPresenceValue != "")
            {
                RichPresenceManager.SetPresence(richPresenceKey, richPresenceValue);
            }
        }
        if(GlobalControl.Instance != null)
        {
            successManager.setStats();
        }

        if (NetworkManager.Singleton.ConnectedClients[playerID].PlayerObject)
        {
            if (GlobalControl.Instance != null)
            {
                GlobalControl.Instance.ui_hotbar.SetActive(true);
                GlobalControl.Instance.ui_inventory.SetActive(true);
              
                GlobalControl.Instance.player.GetComponent<culler>().cull();
                if (init)
                {
                    GlobalControl.Instance.UI_Objective.GetComponent<ObjectiveManager>().set();

                }
                GlobalControl.Instance.UI_Objective.GetComponent<ObjectiveManager>().check();
            }
            pointlist pointlist = FindObjectOfType<pointlist>();
            GameObject player = GlobalControl.Instance.player;
            player.GetComponent<PlayerMovement>().onAwake = this;
            if (pointlist != null){
                FindObjectOfType<AIMovement>().pointlist = FindObjectOfType<pointlist>();

            }
            else
            {
                player.GetComponent<AIPath>().enabled = false;
                player.GetComponent<AIDestinationSetter>().enabled = false;
                player.GetComponent<AIMovement>().enabled = false;

            }
            
                if (GlobalControl.Instance.cinematic)
                {
                    GlobalControl.Instance.HideUI();
                }
                else
                {



                    if (!SetAIPlayer)
                    {
                        player.GetComponent<AIPath>().enabled = false;
                        player.GetComponent<AIDestinationSetter>().enabled = false;
                        player.GetComponent<AIMovement>().enabled = false;
                        GlobalControl.Instance.ui_hotbar.GetComponentInChildren<Canvas>().enabled = true;
                        GlobalControl.Instance.ui_life.GetComponent<Canvas>().enabled = true;
                        GlobalControl.Instance.ui_money.GetComponentInChildren<Canvas>().enabled = true;
                        GlobalControl.Instance.UI_Buttons.GetComponent<Canvas>().enabled = true;
                        GlobalControl.Instance.UI_Objective.GetComponent<Canvas>().enabled = true;
                        GlobalControl.Instance.UI_Exp.GetComponent<Canvas>().enabled = true;
                        GlobalControl.Instance.UI_Time.GetComponent<Canvas>().enabled = true;
                        if (!dataStatic.Instance.tutcomp)
                        {
                            GlobalControl.Instance.UI_Tutorial.GetComponent<Canvas>().enabled = true;
                            dataStatic.Instance.tutcomp = true;
                        }
                    }
                    else
                    {
                        player.GetComponent<AIPath>().enabled = true;
                        player.GetComponent<AIDestinationSetter>().enabled = true;
                        player.GetComponent<AIMovement>().enabled = true;
                        if (pointlist != null)
                        {
                            player.GetComponent<AIMovement>().destinationSetter.target = pointlist.points[0].transform;
                        }
                        GlobalControl.Instance.ui_hotbar.GetComponentInChildren<Canvas>().enabled = false;
                        GlobalControl.Instance.ui_life.GetComponent<Canvas>().enabled = false;
                        GlobalControl.Instance.ui_money.GetComponentInChildren<Canvas>().enabled = false;
                        GlobalControl.Instance.UI_Buttons.GetComponent<Canvas>().enabled = false;
                        GlobalControl.Instance.UI_Objective.GetComponent<Canvas>().enabled = false;
                        GlobalControl.Instance.UI_Exp.GetComponent<Canvas>().enabled = false;
                        GlobalControl.Instance.UI_Time.GetComponent<Canvas>().enabled = false;
                    }
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
        GlobalControl.Instance.isloading = false;
    }

    
}
