using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnToPoint : MonoBehaviour
{
    [SerializeField] public GameObject gameObject;
    // Start is called before the first frame update
    void Start()
    {
        GlobalControl.Instance.player.transform.position = gameObject.transform.position; 
    }

   
}
