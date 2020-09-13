using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopTimeController : MonoBehaviour
{
    public GameObject timeStopCircle;
    public float timeStopRange;
    public float timeStopDuration;
    public float initialSize;
    public float expansionRate = 1;
    public float retractionRate;
    public bool timeIsStopped = false;
    public bool fullSize = false;
    public bool playAudio = false;
    public bool stoppingTime;
    public bool playStoppingAudio;
    public bool startingTime;
    public float timeStopDelay;


    private Rigidbody2D rgbd;
    private bool onlyOnce;
    private float currentSize = 0;

    private void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            // cooldown time here later

            stoppingTime = true;
            playAudio = true;
            StartCoroutine(TimeStop());
        }
        
        else
        {
            playAudio = false;
        }

        if (!timeIsStopped)
        {
            startingTime = false;
        }

    }

    private IEnumerator TimeStop()
    {
        yield return new WaitForSeconds(timeStopDelay);        
        timeIsStopped = true;
        GameObject radius = GameObject.Instantiate(timeStopCircle, transform.position, transform.rotation);
   

        while (currentSize < timeStopRange)
        {
            yield return 0;
            //radiusRenderer.material = stoppingTime;
            currentSize += expansionRate;
            radius.transform.localScale = new Vector3(currentSize, currentSize, 0);
        }

        if (currentSize >= timeStopRange)
        {
            fullSize = true;
            stoppingTime = false;
        }

        yield return new WaitForSeconds(timeStopDuration);
        timeIsStopped = false;
        onlyOnce = true;
        startingTime = true;
        playStoppingAudio = true;       
        yield return 0;
        playStoppingAudio = false;
        fullSize = false;

        while (currentSize > 0)
        {
            yield return 0;
            currentSize -= retractionRate;
            radius.transform.localScale = new Vector3(currentSize, currentSize, 0);
        }

        //if (currentSize <= 0)
        //{            
        //    timeIsStopped = false;
        //}

        yield return 0;

        if (radius.GetComponent<stopTime>().done)
        {
            Destroy(radius);
        }
    }

}

