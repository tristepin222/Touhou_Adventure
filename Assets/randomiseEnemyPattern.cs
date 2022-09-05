using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomiseEnemyPattern : MonoBehaviour
{

    public List<GameObject> enemiesPattern = new List<GameObject>();
    public List<GameObject> AnchorPoints = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject anchorPoint in AnchorPoints)
        {
            Instantiate(enemiesPattern[Random.Range(0, enemiesPattern.Count - 1)], anchorPoint.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
