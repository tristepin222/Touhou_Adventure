using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleport : MonoBehaviour
{
    public Transform destination;
    public PlayerManagament player;
    void Start()
    {
        player = GlobalControl.Instance.player.GetComponent<PlayerManagament>();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if(player == null)
            {
                player = GlobalControl.Instance.player.GetComponent<PlayerManagament>();
            }
            player.gameObject.transform.position = destination.position;
        }
    }
}
