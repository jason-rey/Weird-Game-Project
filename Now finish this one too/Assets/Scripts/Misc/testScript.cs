using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testScript : MonoBehaviour
{
    public List<GameObject> objectsInsideRadius = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        objectsInsideRadius.Add(collision.gameObject);
    }

}
