using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Encounter : MonoBehaviour
{
    public GameObject encounterToSpawn;
    public Transform startingPoint;
    public PointManager pointManager;
    public GameObject obstacle;
    public bool save = true;
    public int index;
    // Start is called before the first frame update
    void Start()
    {
        
        if (!GlobalControl.Instance.encounters[index])
        {
            encounterToSpawn.transform.position = startingPoint.position;
            encounterToSpawn.GetComponent<NPCMovement>().startMovement();
            if (save)
            {
                GlobalControl.Instance.encounters[index] = true;
            }
        }
        else
        {
            obstacle.SetActive(false);
        }
    }

    
}
