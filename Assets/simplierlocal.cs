using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Events;
using UnityEngine.UI;
public class simplierlocal : MonoBehaviour
{
  
    public LocalizedString myString;
    public Text text;
    string localizedText;
    public TypeWriterEffect twe;
    [SerializeField]
    public  UnityEventString m_UpdateString = new UnityEventString();
    /// <summary>
    /// Register a ChangeHandler. This will be called whenever we need to update our string.
    /// </summary>
    void OnEnable()
    {
        if (twe == null)
        {
            myString.StringChanged += UpdateString;
        }
        
    }

    private void OnDisable()
    {
        myString.StringChanged -= UpdateString;
    }
    public void RefreshString()
    {
        myString.RefreshString();
        
    }
    public void UpdateString(string s)
    {
      

            localizedText = s;
        if (text != null)
        {
            if (dataStatic.Instance != null)
            {
                if (text.text == dataStatic.Instance.characterName)
                {

                }
                else
                {

                    text.text = localizedText;
                }
            }
        }
        if (twe != null)
        {
            if (!twe.started)
            {
                twe.startCoroutine(localizedText);
            }
        }
    }

    
    void OnValidate()
    {
        if (text != null)
        {
            if (dataStatic.Instance != null)
            {
                if (text.text == dataStatic.Instance.characterName)
                {

                }
                else
                {


                    RefreshString();
                }
            }
            
        }
        else
        {
            RefreshString();
        }
    }
    

}
