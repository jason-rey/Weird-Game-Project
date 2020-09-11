using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class theWorldSoundController : MonoBehaviour
{
    public AudioSource timeStopSound;
    public AudioSource timeStartSound;
    public stopTimeController timeController;

    private bool onlyOnce = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeController.playAudio)
        {            
            timeStopSound.Play();         
        }

        if (timeController.playStoppingAudio)
        {
            Debug.Log("play");
            timeStartSound.Play();
        }

    }
}
