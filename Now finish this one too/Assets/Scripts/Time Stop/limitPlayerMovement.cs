using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class limitPlayerMovement : MonoBehaviour
{
    public stopTimeController timeController;
    public PolygonCollider2D polyCol1;
    public PolygonCollider2D polyCol2;
    public CapsuleCollider2D playerCollider;
    public GameObject player;
    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        timeController = player.GetComponent<stopTimeController>();
        playerCollider = player.GetComponent<CapsuleCollider2D>();
    }

    private void Update()
    {
        //if (timeController.stoppingTime || timeController.startingTime)
        //{
        polyCol1.enabled = false;
        polyCol2.enabled = false;
        //}

        if (timeController.fullSize)
        {
            polyCol1.enabled = true;
            polyCol2.enabled = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (!other.CompareTag("Player"))
        {
            Physics2D.IgnoreCollision(other, polyCol1);
            Physics2D.IgnoreCollision(other, polyCol2);
        }
       
    }

}
