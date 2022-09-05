using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Type
{
    public string typeName;
    public TypeScriptable type;
    public enum TypeT
    {
        Vampire,
        Fairy,

    }

    public string GetString()
    {
        switch (type.Type)
        {
            default:
            case TypeT.Vampire: return "Vampire";
            case TypeT.Fairy: return "Fairy";
        }

        
    }
    public Item GetItem()
    {
        switch (type.Type)
        {
            default:
            case TypeT.Vampire: return new Item { };
            case TypeT.Fairy: return new Item { };
        }
    }
}  