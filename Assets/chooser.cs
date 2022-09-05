using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
public class chooser : MonoBehaviour
{
    public int index = 0;
    public string[] strings = { "リボン", "髪", "シャツ", "目", "眉毛", "口", "パンツ", "靴" };
    public string[] strings2 = { "Ribbon", "Hair", "shirt", "Eyes", "Eyebrows", "Month", "Pants", "Shoes" };
    public string[] Master;
    public Text Text;
    public Text Text2;
    public Dropdown dp;
    // Start is called before the first frame update
    void Start()
    {
      

    }
    private void Update()
    {
        if (dp.value == 0)
        {
            Master = strings2;
        }
        else
        {
            Master = strings;
        }
    }
    public  void Add()
    {
        if (index == strings.Length-1)
        {
            index = 0;
        }
        else
        {
            index++;
        }
        
        Text.text = Master[index];
        if (dp.value == 0)
        {
            Text2.text = "Style " + dataStatic.Instance.Indexes[index];
        }
        else
        {
            Text2.text = "スタイル " + dataStatic.Instance.Indexes[index];
        }
      
    }
    public  void Remove()
    {
        if (index == 0)
        {
            index = strings.Length-1;
        }
        else
        {
            index--;
        }
        Text.text = Master[index];
        if (dp.value == 0)
        {
            Text2.text = "Style " + dataStatic.Instance.Indexes[index];
        }
        else
        {
            Text2.text = "スタイル " + dataStatic.Instance.Indexes[index];
        }
    }


}

