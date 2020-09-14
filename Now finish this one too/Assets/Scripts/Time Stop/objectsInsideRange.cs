using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class objectsInsideRange : MonoBehaviour
{
    public List<GameObject> stoppedObjects = new List<GameObject>();
    public List<GameObject> GetColliders() { return stoppedObjects; }

    public List<GameObject> stoppedKnives = new List<GameObject>();

    public GameObject[] objectsInScene;

    public List<string> tagList = new List<string>();

    private void Awake()
    {
        objectsInScene = FindObjectsOfType(typeof(GameObject)) as GameObject[];

        foreach (GameObject gameObject in objectsInScene)
        {
            if (gameObject.GetComponent<Rigidbody2D>() == null)
            {
                continue;
            }
           
            if (tagList.Contains(gameObject.tag))
            {
                continue;               
            }

            stoppedObjects.Add(gameObject);

        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "playerProjectile")
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                stoppedKnives.Add(collision.gameObject);
            }
        }
    }
}
