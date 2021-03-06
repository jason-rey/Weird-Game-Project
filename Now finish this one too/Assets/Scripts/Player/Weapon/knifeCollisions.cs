﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class knifeCollisions : MonoBehaviour
{
    public stopTimeController timeController;

    public float knifeRemoveDelay;

    public BoxCollider2D knifeCollider;
    private CapsuleCollider2D playerCollider;
    private GameObject player;
    private Rigidbody2D knifeBody;
    private Vector3 mousePos;
    public Vector3 shootDirection;
    private Vector3 globalScale;
    public GameObject projectilePrefab;
    public GameObject detectionSphere;
    public float knifeHitDetectionLength = 1;
    public bool aboutToCollide = false;
    public float knifeDamage;
    public Vector2 storedVelocity;

    private BoxCollider2D prefabCollider;
    private bool onlyOnce = true;
    private Rigidbody2D rgbd;
    private int layerMask;

    private void Awake()
    {
//        knifeCollider = this.GetComponent<BoxCollider2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerCollider = player.GetComponent<CapsuleCollider2D>();
        timeController = player.GetComponent<stopTimeController>();
        knifeBody = GetComponent<Rigidbody2D>();

        mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        shootDirection = (mousePos - this.transform.position);
        globalScale = new Vector3(5, 5, 2);
        prefabCollider = projectilePrefab.GetComponent<BoxCollider2D>();
        rgbd = GetComponent<Rigidbody2D>();


    }

    private void Update()
    {
        layerMask = ~(LayerMask.GetMask("Time Stop Bubble", "Player Projectile"));
        //Debug.DrawRay(this.transform.position, shootDirection.normalized * ((knifeCollider.bounds.size.y / 2) + 60));

        if (timeController.timeIsStopped)
        {
            prefabCollider.isTrigger = false;
        }   else
        {
            prefabCollider.isTrigger = true;
        }

        if (onlyOnce)
        {
            //if (hit.collider.tag != "Player" && hit.collider.tag != "timeStopBubble" && hit.collider.tag != "playerProjectile")
            //{
            //    GameObject middleMan = new GameObject("Middle Object");
            //    middleMan.layer = 2;
            //    knifeBody.velocity = Vector3.zero;
            //    knifeBody.isKinematic = true;
            //    knifeCollider.enabled = false;
            //    middleMan.transform.SetParent(hit.collider.transform, true);


            //    transform.SetParent(middleMan.transform, true);


            //    knifeBody.isKinematic = true;
            //    knifeCollider.enabled = false;
            //    StartCoroutine("DestroyKnife");
            //}

        }
        

        Debug.DrawRay(transform.position, shootDirection.normalized * ((knifeCollider.bounds.size.y)/2 + knifeHitDetectionLength), Color.red);
        RaycastHit2D detectCollision = Physics2D.Raycast(transform.position, shootDirection.normalized, ((knifeCollider.bounds.size.y / 2) + knifeHitDetectionLength),layerMask);


        if (!timeController.timeIsStopped)
        {
            Physics2D.IgnoreCollision(playerCollider, knifeCollider);
        }

        if (timeController.timeIsStopped)
        {
            //stoppedKnifeHitRadius.SetActive(true);

            if (detectCollision)
            {
                Debug.Log("ohboyhereitcomes");
                aboutToCollide = true;
                rgbd.velocity = Vector3.zero;
                rgbd.constraints = RigidbodyConstraints2D.FreezeAll;

            }

            if (!detectCollision)
            {
                //aboutToCollide = false;
            }
        }

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        //if (other.gameObject.tag == "Player")
        //{
        //    Debug.Log("player");
        //    if (!timeController.timeIsStopped)
        //    {
        //        Debug.Log("ignoring");
        //        Physics2D.IgnoreCollision(playerCollider, knifeCollider);
        //    }
        //}

        //if (!timeController.timeIsStopped)
        //{
        //    if (!other.gameObject.CompareTag("playerProjectile") && !other.gameObject.CompareTag("timeStopBubble") && !other.gameObject.CompareTag("Player"))
        //    {               
        //        aboutToCollide = true;
        //    }
        //}
        

        if (other.gameObject.tag == "playerProjectile")
        {
            Physics2D.IgnoreCollision(knifeCollider, other.collider);

        }
    }

    public IEnumerator DestroyKnife()
    {
        yield return new WaitForSeconds(knifeRemoveDelay);

        while (timeController.timeIsStopped)
        {
            yield return null;
        }

        if (!timeController.timeIsStopped)
        {
            Destroy(transform.parent.gameObject);
            Destroy(this.gameObject);
        }
    }

}
