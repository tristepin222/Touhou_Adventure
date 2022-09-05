using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crops 
{
    public cropScriptableObject cropS;
    public enum crop
    {
        daikon,
    }
    public string GetString()
    {
        switch (cropS.cropT)
        {
            default:
            case crop.daikon: return "Daikon";
        }
    }
}
