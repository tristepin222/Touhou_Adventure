using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simplebutton : MonoBehaviour
{
    public GameObject obje;
    private void Start()
    {
        obje.SetActive(false);
    }
    public void show()
    {

        if (obje.activeInHierarchy)
        {
            obje.SetActive(false);
        }
        else
        {
            obje.SetActive(true);
        }
    }
}
