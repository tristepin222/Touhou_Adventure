using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class screenRes : MonoBehaviour
{
    public GameObject confirm;
    public int[] width;
    public int[] height;
    private int screenmode;
    private int screensize;
    private FullScreenMode fullscreenmode;
    private bool confirmed;
    private int currentwidth;
    private int currentheight;
    private FullScreenMode currentfullscreenmode;
    public void ChangeScreenMode(int value)
    {

        switch (value)
        {
            case 0:
                fullscreenmode = FullScreenMode.MaximizedWindow;
                break;
            case 1:
                fullscreenmode = FullScreenMode.FullScreenWindow;
                break;
            case 2:
                fullscreenmode = FullScreenMode.Windowed;
                break;
        }
        StartCoroutine(changeScreen());
    }
    public void ChangeScreenSize(int value)
    {
        screensize = value;
        StartCoroutine(changeScreen());
    }
    public void Confirm()
    {
        confirmed = true;
        confirm.SetActive(false);
    }

    private IEnumerator changeScreen()
    {
     
        confirmed = false;
        currentfullscreenmode = Screen.fullScreenMode;
        currentwidth = Screen.currentResolution.width;
        currentheight = Screen.currentResolution.height;
        Screen.SetResolution(width[screensize], height[screensize], fullscreenmode);
        confirm.SetActive(true);
        yield return new WaitForSeconds(15f);
        if (!confirmed)
        {
            Screen.SetResolution(currentwidth, currentheight, currentfullscreenmode);
        }
        
    }

}
