using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class AIMovement : MonoBehaviour
{
    public GameObject[] gameObjects;
    public pointlist pointlist;
    public AIDestinationSetter destinationSetter;
    private const float MOVESPEED = 7.5f;
    private int i = 0;
    private void Start()
    {
        pointlist = FindObjectOfType<pointlist>();
        destinationSetter.target = pointlist.points[0].transform;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "point")
        {
            i++;
            if (i <= pointlist.points.Count)
            {
                destinationSetter.target = pointlist.points[i].transform;
            }
        }
    }
}
   
