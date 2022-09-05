using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    private Vector3 startingpos;
    private Vector3 roampos;
    // Start is called before the first frame update
    void Start()
    {
        startingpos = transform.position;
        roampos = GetRoamingPosition();
    }
    private Vector3 GetRoamingPosition()
    {
        Vector3 v3 = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        return startingpos + v3 * Random.Range(10f, 70f);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
