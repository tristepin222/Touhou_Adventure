using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CullerSelf : MonoBehaviour
{
    int nu = 1;
    public Animator animator;
    public List<int> sortingorderNumber = new List<int>();
    public SpriteRenderer[] renderers;
    void Start()
    {

        foreach (SpriteRenderer render in renderers)
        {
            sortingorderNumber.Add(render.sortingOrder);
            nu = (int)(render.transform.position.y * -100);
                render.sortingOrder += nu;
          
        }
    }

    // Update is called once per frame
    void Update()
    {
        int i = 0;
        foreach (SpriteRenderer render in renderers)
        {

            nu = (int)(this.transform.position.y * -100);
            nu += sortingorderNumber[i];
            i++;

        }
    }
}
