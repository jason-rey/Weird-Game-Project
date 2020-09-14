using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ignoreTrigger : MonoBehaviour
{
    private CircleCollider2D collider;
    public knifeCollisions knifeCol;
    // Start is called before the first frame update
    void Start()
    {
        collider = this.GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "playerProjectile" || other.tag == "timeStopBubble")
        {
            Physics2D.IgnoreCollision(collider, other);
        }

        if (!other.CompareTag("playerProjectile") && !other.CompareTag("timeStopBubble") && !other.CompareTag("Player"))
        {
            knifeCol.aboutToCollide = true;
        }
    }

}
