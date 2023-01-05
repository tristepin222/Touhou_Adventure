using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Steamworks;

public class GiveSucces : MonoBehaviour
{
	bool start;
	void Start()
	{
		if (SteamManager.Initialized)
		{
	

			SteamUserStats.GetAchievement("ACH_START", out start);
			if (start == false)
			{
				SteamUserStats.SetAchievement("ACH_START");
				SteamUserStats.StoreStats();
			}
		}
	}
}
