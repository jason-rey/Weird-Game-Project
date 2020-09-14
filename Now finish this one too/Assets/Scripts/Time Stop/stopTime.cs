using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stopTime : MonoBehaviour
{
    public objectsInsideRange stopRange;
    public stopTimeController timeController;
    public List<Vector3> storedVelocities = new List<Vector3>();
    public List<Vector3> stoppedKnifeVelocities = new List<Vector3>();
    public bool done = false;

    [SerializeField] private float knifeStopDelay;

    private int tempListCount = 0;
    private int objectCount;
    private int objectIndex;
    private bool onlyOnce;
    private bool onlyOnce2;
    private bool hasChanged = false;
    private void Awake()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        timeController = player.GetComponent<stopTimeController>();
        objectIndex = 0;
        onlyOnce = true;
        onlyOnce2 = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        //stopRange = GetComponent<objectsInsideRange>();
    }

    
    private void Update()
    {
        DetectListChange();

        if (timeController.timeIsStopped)
        {
            //Debug.Log("stopping time");
            if (onlyOnce)
            {
                StartCoroutine("StopObjects");
                onlyOnce = false;
            }
            StartCoroutine("StopKnives");
        }

        else if (!timeController.timeIsStopped)
        {
            //Debug.Log("continuing time");

            if (onlyOnce2)
            {
                ContinueObjectMovement();
                onlyOnce2 = false;
            }
        }

        objectCount = stopRange.stoppedObjects.Count;
    }

    // Update is called once per frame
    private IEnumerator StopObjects()
    {

        yield return 0;
        foreach (GameObject stoppedObject in stopRange.stoppedObjects)
        {
            Rigidbody2D currentBody = stoppedObject.GetComponent<Rigidbody2D>();
            if (stoppedObject.tag != "playerProjectile")
            {                
                storedVelocities.Add(currentBody.velocity);                
                currentBody.constraints = RigidbodyConstraints2D.FreezeAll;
                currentBody.velocity = Vector3.zero;
            }
                
        }
        
    }

    private void DetectListChange()
    {

        if (tempListCount != stopRange.stoppedKnives.Count)
        {
            hasChanged = true;
            tempListCount = stopRange.stoppedKnives.Count;
            //yield return 1;
        }

        else if (tempListCount == stopRange.stoppedKnives.Count)
        {
            hasChanged = false;
        }
    }

    private IEnumerator StopKnives()
    {

        if (hasChanged)
        {
            Rigidbody2D currentBody = stopRange.stoppedKnives[tempListCount-1].GetComponent<Rigidbody2D>();
            //knifeCollisions knifeCol = stopRange.stoppedKnives[tempListCount-1].GetComponent<knifeCollisions>();

            if (currentBody.velocity == Vector2.zero)
            {
                Debug.Log("zeeeeeerroooo");
                currentBody.velocity = stopRange.stoppedKnives[tempListCount - 1].GetComponent<knifeCollisions>().storedVelocity;
                Debug.Log(currentBody.velocity);
            }

            stoppedKnifeVelocities.Add(currentBody.velocity);


            //if (!knifeCol.aboutToCollide)
            //{
            yield return new WaitForSeconds(knifeStopDelay);
            currentBody.velocity = Vector3.zero;
            currentBody.constraints = RigidbodyConstraints2D.FreezeAll;
            //}
        }
    }

    private void ContinueObjectMovement()
    {
        foreach (GameObject stoppedObject in stopRange.stoppedObjects)
        {
            Rigidbody2D currentBody = stoppedObject.GetComponent<Rigidbody2D>();

            if (stoppedObject.tag != "Environment")
            {
                //Debug.Log(stoppedObject.name);
                currentBody.constraints = RigidbodyConstraints2D.FreezeRotation;
            }
            currentBody.velocity = storedVelocities[objectIndex];
           

            if (objectIndex + 1 <= storedVelocities.Count - 1 && storedVelocities.Count != 0)
            {
                objectIndex += 1;
            }
                
        }

        objectIndex = 0;

        foreach (GameObject knife in stopRange.stoppedKnives)
        {
            if (knife.GetComponent<Rigidbody2D>() != null)
            {
                Rigidbody2D currentBody = knife.GetComponent<Rigidbody2D>();

                //if (stoppedKnifeVelocities[objectIndex] == Vector3.zero)
                //{
                //    Debug.Log("applying");
                //    stoppedKnifeVelocities[objectIndex] = Vector3.one * GameObject.Find("Knife").GetComponent<fireProjectile>().shootForce;
                //}

                currentBody.constraints = RigidbodyConstraints2D.FreezeRotation;
                currentBody.velocity = stoppedKnifeVelocities[objectIndex];

            }

            if (objectIndex + 1 <= stoppedKnifeVelocities.Count-1 && stoppedKnifeVelocities.Count != 0)
            {
                objectIndex += 1;
            }
        }

        done = true;

    }
            
}
