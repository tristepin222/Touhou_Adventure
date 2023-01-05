using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class SuccessManager : MonoBehaviour
{
    // Start is called before the first frame update
 
    bool start = false;
    bool inited = false;
    void Start()
    {

    }
    public void init()
    {
        if (SteamManager.Initialized)
        {
            inited = true;
            SteamUserStats.GetStat("STEPS", out GlobalControl.Instance.steps);
            SteamUserStats.GetStat("FIGHTS", out GlobalControl.Instance.fights);
            SteamUserStats.GetStat("MONEY", out GlobalControl.Instance.money);
        }
    }
    public void SetAchievement(string achievement)
    {
        if (inited)
        {
            SteamUserStats.GetAchievement(achievement, out start);
            if (start == false)
            {
                SteamUserStats.SetAchievement(achievement);
                SteamUserStats.StoreStats();
            }
        }
      
    }
    public void setStats()
    {
        int i = 0;
        if (inited)
        {
          
            SteamUserStats.SetStat("STEPS", GlobalControl.Instance.steps);
            SteamUserStats.SetStat("FIGHTS", GlobalControl.Instance.fights);
            SteamUserStats.SetStat("MONEY", GlobalControl.Instance.money);
           
                //Debug.Log(SteamUserStats.StoreStats());
                SteamUserStats.StoreStats();
            
        }

    }


    void OnApplicationQuit()
    {
     
    }
}
