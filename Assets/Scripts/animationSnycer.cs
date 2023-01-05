using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationSnycer : MonoBehaviour
{
    // Start is called before the first frame update
    public void sync()
    {

        Animator anim =   this.GetComponent<Animator>();
        anim.Play("animState", -1);
        anim.Play("animState", 0);
        anim.Play("animState", 3);
    }
}
