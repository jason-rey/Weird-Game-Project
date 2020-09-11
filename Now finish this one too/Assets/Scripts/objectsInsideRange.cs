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

    public string[] TagList = { "Ball", "Box" };

    private void Awake()
    {
        objectsInScene = FindObjectsOfType(typeof(GameObject)) as GameObject[];

        foreach (GameObject gameObject in objectsInScene)
        {
            if (gameObject.GetComponent<Rigidbody2D>() == null)
            {
                continue;
            }

            if (gameObject.tag != "theWorld" && gameObject.tag != "Player" && gameObject.tag != "playerProjectile" && gameObject.tag != "MainCamera" && gameObject.tag != "cameraComponent" && gameObject.tag != "knifeComponent" && gameObject.tag != this.tag && gameObject.name != "cm")
            {
                stoppedObjects.Add(gameObject);
                
            }

            if (gameObject.tag == "playerProjectile")
            {
                stoppedKnives.Add(gameObject);
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "playerProjectile")
        {
            Debug.Log("this");
            if (collision.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                stoppedKnives.Add(collision.gameObject);
            }
        }
    }
}
