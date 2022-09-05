using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderBrige : MonoBehaviour
{
    ObjectiveManager _listener;
    public void Initialize(ObjectiveManager l)
    {
        _listener = l;
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        _listener.OnCollisionEnter2D(collision);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        _listener.OnTriggerEnter2D(other);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _listener.OnTriggerExit2D(collision);
    }
   
}
