using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linedrawer : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject object1;
    public bool isPlayer = true;
    public GameObject object2;
    LineRenderer liner;
   public CurvedLinePoint linepoint;
    void Start()
    {
        liner = this.gameObject.GetComponent<LineRenderer>();
        linepoint = this.GetComponentInChildren<CurvedLinePoint>();
        if (isPlayer)
        {
            object1 = GlobalControl.Instance.player;
        }
        linepoint.transform.position = object1.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        linepoint.transform.position = object1.transform.position;
    }
}
