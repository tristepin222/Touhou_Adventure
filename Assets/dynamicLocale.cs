using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Localization;
using UnityEngine.Localization.Events;
using UnityEngine.Localization.Settings;
using UnityEngine.Localization.Tables;
public class dynamicLocale : MonoBehaviour
{
   
    [SerializeField] public Text text;
    [SerializeField] public bool useString;
    [SerializeField] public string textt;
    [SerializeField]
    LocalizedString m_StringReference = new LocalizedString();

    [SerializeField]
    List<Object> m_FormatArguments = new List<Object>();

    [SerializeField]
    UnityEventString m_UpdateString = new UnityEventString();

    /// <summary>
    /// References the <see cref="StringTable"/> and <see cref="StringTableEntry"/> of the localized string.
    /// </summary>
    /// 
    private void Start()
    {
        RefreshString();
    }
    public LocalizedString StringReference
    {
        get => m_StringReference;
        set
        {
            // Unsubscribe from the old string reference.
            ClearChangeHandler();

            m_StringReference = value;

            if (isActiveAndEnabled)
                RegisterChangeHandler();
        }
    }

    /// <summary>
    /// Event that will be sent when the localized string is available.
    /// </summary>
    public UnityEventString OnUpdateString
    {
        get => m_UpdateString;
        set => m_UpdateString = value;
    }

    /// <summary>
    /// Forces the string to be regenerated, such as when the string formatting argument values have changed.
    /// </summary>
    public void RefreshString()
    {
        if (useString)
        {


            m_StringReference.TableEntryReference = text.text;
        }
        else
        {
            m_StringReference.TableEntryReference = textt;
        }
        StringReference.RefreshString();
    }

    /// <summary>
    /// Starts listening for changes to <see cref="StringReference"/>.
    /// </summary>
    protected virtual void OnEnable() {
        
        RegisterChangeHandler();
        RefreshString();
    }

    /// <summary>
    /// Stops listening for changes to <see cref="StringReference"/>.
    /// </summary>
    protected virtual void OnDisable() => ClearChangeHandler();

    /// <summary>
    /// Invokes the <see cref="OnUpdateString"/> event.
    /// </summary>
    /// <param name="value"></param>
    protected virtual void UpdateString(string value)
    {

            OnUpdateString.Invoke(value);
       
    }

    void OnValidate()
    {
        RefreshString();
    }

    internal virtual void RegisterChangeHandler()
    {
        
        if (m_FormatArguments.Count > 0)
            StringReference.Arguments = m_FormatArguments.ToArray();
        StringReference.StringChanged += UpdateString;
    }

    internal virtual void ClearChangeHandler()
    {
        StringReference.StringChanged -= UpdateString;
    }

    private void uptstring()
    {
        
    }
}

