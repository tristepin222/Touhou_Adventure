using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class chooserStyle : MonoBehaviour
{
    public int index = 0;
    public int index2 = 0;
    public chooser ch;
    [SerializeField] public Sprite[] spritesHair;
    [SerializeField] public Sprite[] spritesMouth;
    [SerializeField] public Sprite[] spritesEyesBrows;
    [SerializeField] public Sprite[] spritesTops;
    [SerializeField] public Sprite[,] spritesMaster = new Sprite[8,20];
    public Text Text4;
    public Text Text3;
    public Text Text2;
    public Text Text;
    public Menu menu;
    Image sp;


    public GameObject[] Gos;
    // Start is called before the first frame update
    void Start()
    {
        int i;
        i = 0;
        foreach (Sprite sprite in spritesHair)
        {
            spritesMaster[1, i] = sprite;
            i++;
        }
        i = 0;
        //foreach (Sprite sprite in spritesMouth)
        //{
        //    spritesMaster[5, i] = sprite;
        //    i++;
        //}
        i = 0;
        foreach (Sprite sprite in spritesEyesBrows)
        {
            spritesMaster[4, i] = sprite;
            i++;
        }
        i = 0;
        foreach (Sprite sprite in spritesTops)
        {
            spritesMaster[2, i] = sprite;
            i++;
        }
        dataStatic.Instance.Indexes[ch.index] = index;
    }
   public void PreStart()
    {
        int i;
        i = 0;
        foreach (Sprite sprite in spritesHair)
        {
            spritesMaster[1, i] = sprite;
            i++;
        }
    }
   
    public void Add()
    {
        
        if ( spritesMaster[ch.index, index + 1] == null)
        {
            index = 0;
           
        }
        else
        {
            index2++;
            index++;
        }
        if (spritesMaster[ch.index, index] != null)
        {
            dataStatic.Instance.Names[ch.index] = spritesMaster[ch.index, index].name;
            dataStatic.Instance.Indexes[ch.index] = index;
            if (ch.dp.value == 0)
            {
                Text2.text = "Style " + index;
            }
            else
            {
                Text2.text = "スタイル " + index;
            }
            sp = GameObject.Find(ch.strings2[ch.index]).GetComponent<Image>();
            sp.sprite = spritesMaster[ch.index, index];
            
        }
        else
        {
            index = 0;
        }
    }
    public void Remove()
    {
        if (index == 0)
        {
            
            index = 0;
        }
        else
        {
            index--;
        }
        if (spritesMaster[ch.index, index] != null)
        {
            if (ch.dp.value == 0)
            {
                Text2.text = "Style " + index;
            }
            else
            {
                Text2.text = "スタイル " + index;
            }

            sp = GameObject.Find(ch.strings2[ch.index]).GetComponent<Image>();
            dataStatic.Instance.Names[ch.index] = spritesMaster[ch.index, index].name;
            dataStatic.Instance.Indexes[ch.index] = index;
            sp.sprite = spritesMaster[ch.index, index];
            sp = GameObject.Find(ch.strings2[ch.index]).GetComponent<Image>();
            sp.sprite = spritesMaster[ch.index, index];
        }
        else
        {
            index = 0;
            if (ch.dp.value == 0)
            {
                Text2.text = "Style " + index;
            }
            else
            {
                Text2.text = "スタイル " + index;
            }
        }
    }
    public void Comfirm()
    {
        if (Text3.text == "")
        {
            Text4.color = new Color(255, 0, 0, 255);
        }
        else
        {
            dataStatic.Instance.characterName = Text3.text;
            dataStatic.Instance.once = true;
            menu.Play();
            foreach (GameObject b in Gos)
            {

                dataStatic.Instance.colors.Add(b.GetComponent<Image>().color);
            }
        }
    }
}

