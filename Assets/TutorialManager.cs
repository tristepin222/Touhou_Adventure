using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] gms;
    private int i = 0;
    private void Start()
    {
        i = 0;
    }
    public void minus()
    {
        
        if (i > 0)
        {
            i--;
            gms[i + 1].SetActive(false);
            gms[i].SetActive(true);
        }
    }
    public void plus()
    {
       
        if (i < gms.GetLength(0)-1)
        {
            i++;
            gms[i - 1].SetActive(false);
            gms[i].SetActive(true);
        }

    }
}
