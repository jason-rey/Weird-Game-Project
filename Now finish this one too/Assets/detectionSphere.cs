using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class detectionSphere : MonoBehaviour
{
    public GameObject knife;
    public bool hitObject;

    private knifeCollisions knifeCol;
    private Rigidbody2D knifeBody;
    private Collider2D knifeCollider;
    // Start is called before the first frame update
    void Start()
    {
        knifeBody = knife.GetComponent<Rigidbody2D>();
        knifeCollider = knife.GetComponent<BoxCollider2D>();
        knifeCol = knife.GetComponent<knifeCollisions>();
    }

    // Update is called once per frame
    void Update()
    {
        hitObject = false;
    }

    private void OnTriggerEnter2D(Collider2D hit)
    {
        hitObject = true;
        if (hit.tag != "Player" && hit.tag != "timeStopBubble" && hit.tag != "playerProjectile")
        {

            if (hit.GetComponent<objectHealth>() != null)
            {
                objectHealth colliderHealth = hit.GetComponent<objectHealth>();
                colliderHealth.currentHealth -= knifeCol.knifeDamage;
            }

            GameObject middleMan = new GameObject("Middle Object");
            middleMan.layer = 2;
            knifeBody.velocity = Vector3.zero;
            knifeBody.isKinematic = true;
            knifeCollider.enabled = false;
            middleMan.transform.SetParent(hit.transform, true);

            knife.transform.SetParent(middleMan.transform, true);


            knifeBody.isKinematic = true;
            knifeCollider.enabled = false;
            StartCoroutine(knifeCol.DestroyKnife());
        }
    }
}
