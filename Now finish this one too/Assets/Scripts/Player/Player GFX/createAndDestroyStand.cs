using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createAndDestroyStand : MonoBehaviour
{
    public GameObject theWorldPrefab;
    public stopTimeController timeController;
    public playerControllerV2 playerController;
    public SpriteRenderer playerSprite;
    public float xOffset = 0.5f;
    public float yOffset = 0.05f;
    public float deploySpeed;
    public float theWorldDestroyDelay;

    private bool onlyOnce = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (timeController.stoppingTime)
        {
            if (onlyOnce)
            {
                StartCoroutine("CreateDestroyStand");
                onlyOnce = false;
            }
        }

        if (!timeController.timeIsStopped)
        {

        }

    }

    private IEnumerator CreateDestroyStand()
    {
        GameObject theWorld = GameObject.Instantiate(theWorldPrefab, transform, false);
        theWorld.transform.localPosition = new Vector3(xOffset, yOffset);
        theWorld.transform.localScale = Vector3.one;
        yield return new WaitForSeconds(theWorldDestroyDelay);
        theWorld.GetComponent<destroyStand>().DestroyStand();
    }



}
