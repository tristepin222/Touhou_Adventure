using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Button : MonoBehaviour
{
   public GameObject UICraft;
    public GameObject UIInv;
    public GameObject UIMap;
    public GameObject UI_Objective;
    public GameObject UI_Tutorial;
    public GameObject UI_BombCreator;
    private void Start()
    {
        UICraft = GlobalControl.Instance.ui_craftingsystem;
        UIInv = GlobalControl.Instance.ui_inventory;
        UIMap = GlobalControl.Instance.ui_map;
        UI_Objective = GlobalControl.Instance.UI_Objective;
        UI_Tutorial = GlobalControl.Instance.UI_Tutorial;
        UI_BombCreator = GlobalControl.Instance.UI_BombCreator;
    }
    public void show(int val)
    {
      
        switch (val)
        {
            case 5:
                if (UI_BombCreator.transform.GetComponent<Canvas>().enabled)
                {
                    UI_BombCreator.transform.GetComponent<Canvas>().enabled = false;
                }
                else
                {
                    UI_BombCreator.transform.GetComponent<Canvas>().enabled = true;
                }
                break;

            case 4:
                if (UI_Tutorial.transform.GetComponent<Canvas>().enabled)
                {
                    UI_Tutorial.transform.GetComponent<Canvas>().enabled = false;
                }
                else
                {
                    UI_Tutorial.transform.GetComponent<Canvas>().enabled = true;
                }
                break;
            case 3:
                if (UI_Objective.transform.GetComponent<Canvas>().enabled)
                {
                    UI_Objective.transform.GetComponent<Canvas>().enabled = false;
                }
                else
                {
                    UI_Objective.transform.GetComponent<Canvas>().enabled = true;
                }
                break;
            case 2:
                if (UICraft.transform.GetChild(0).GetComponent<Canvas>().enabled)
                {
                    UICraft.transform.GetChild(0).GetComponent<Canvas>().enabled = false;
                }
                else
                {
                    UICraft.transform.GetChild(0).GetComponent<Canvas>().enabled = true;
                }
                break;
            case 1:
                if (UIInv.GetComponent<Canvas>().enabled)
                {
                    UIInv.GetComponent<Canvas>().enabled = false;
                }
                else
                {
                    UIInv.GetComponent<Canvas>().enabled = true;
                }
                break;
            case 0:
                if (UIMap.GetComponent<Canvas>().enabled)
                {
                    UIMap.GetComponent<Canvas>().enabled = false;
                }
                else
                {
                    UIMap.GetComponent<Canvas>().enabled = true;
                }
                break;
        }
      
    }
}
