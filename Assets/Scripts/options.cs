using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class options : MonoBehaviour
{
    public static options Instance; 
    public float mAmount = 1f;
    public float sAmount = 1f;
    // Start is called before the first frame update
    

    // Update is called once per frame
    private void Awake()
    {
        if (Instance == null)
        {
           
            DontDestroyOnLoad(gameObject);
           
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
           
        }
    }
}
