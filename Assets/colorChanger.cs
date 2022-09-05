using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class colorChanger : MonoBehaviour
{
    // Start is called before the first frame update
   public FlexibleColorPicker color;
    public Image color2;
    public int i = 0;
    public chooser ch;
    public Text text;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        color2 = GameObject.Find(ch.strings2[ch.index]).GetComponent<Image>();
      color2.color  =color.color;
    }
}
