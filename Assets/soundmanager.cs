using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundmanager : MonoBehaviour
{
    List<AudioSource> audioSourcesS = new List<AudioSource>();
    List<AudioSource> audioSourcesM = new List<AudioSource>();

    private void Start()
    {
        
        foreach(AudioSource audioSource in audioSourcesS)
        {
            audioSource.volume = options.Instance.sAmount;
        }
        foreach (AudioSource audioSource in audioSourcesM)
        {
            audioSource.volume = options.Instance.mAmount;
        }
    }
}
