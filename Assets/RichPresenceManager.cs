using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;
public class RichPresenceManager : MonoBehaviour
{
    bool inited = false;
    void Start()
    {

        if (SteamManager.Initialized)
        {
            inited = true;
        }
    }

    public void SetPresence(string key,string value)
    {
        if (inited)
        {

            SteamFriends.SetRichPresence("place", value);
            SteamFriends.SetRichPresence("steam_display", "#status");
        }

    }
}
