using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]


public class FullObjectException : System.Exception{}
public class InventoryFullException : FullObjectException
{
    public InventoryFullException() { }
    
}
public class InventoryEmpty : FullObjectException { }