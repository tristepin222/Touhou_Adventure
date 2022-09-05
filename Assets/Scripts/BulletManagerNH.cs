using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManagerNH : MonoBehaviour
{
    [SerializeField] Rigidbody2D rb;

    [SerializeField] float speed = 5f;
    [SerializeField] CapsuleCollider2D cd;
    [SerializeField] CapsuleCollider2D cd2;
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    
        
    }
    private void OnBecameInvisible()
    {

        Destroy(this.gameObject);

    }
}
