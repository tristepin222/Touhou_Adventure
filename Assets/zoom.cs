using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoom : MonoBehaviour
{
    private void Start()
    {
        tr.localScale = new Vector2(1, 1);
    }
    public RectTransform tr;
    // Start is called before the first frame update
    public void scrollChanged(float value)
    {
        tr.localScale = new Vector2(1, 1);
        value += 1;
        
        tr.localScale = tr.localScale * value*2;
    }
}
