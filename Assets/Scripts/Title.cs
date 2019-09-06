using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public AudioSource BGM;
    
    public void stopPlaying()
    {
        if (BGM.isPlaying)
        {
            Debug.Log("Call stop methord");
            BGM.Stop();
        }
        else
        {
            BGM.Play();
        }
    }
}
