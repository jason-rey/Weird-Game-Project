using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireKnife : MonoBehaviour
{
    public GameObject knifePrefab;
    public Transform knifeRotation;
    public GameObject knifeEmitter;
    public float throwForce;
    public GameObject playerObj;
    public List<GameObject> objectsInsideRadius;
    public playerController playerControllerScript;

    private Vector3 mousePos;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosRaw = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        mousePos = Camera.main.ScreenToWorldPoint(mousePosRaw);
        mousePos.z = 0;

        Vector3 shootDirection = (mousePos - this.transform.position);
        shootDirection = shootDirection.normalized;
        //Debug.Log(shootDirection * throwForce);
        shootDirection *= throwForce;
        Debug.DrawRay(transform.position, new Vector3(shootDirection.x,shootDirection.y) * throwForce);

        Vector3 finalKnifeForce = new Vector3(shootDirection.x, shootDirection.y, shootDirection.z);
        //Debug.Log(shootDirection);

        if (Input.GetButtonDown("Fire1"))
        {
            ShootKnife(finalKnifeForce, knifeRotation);
        }
    }

    void ShootKnife(Vector3 directionForce,Transform knifeRotation)
    {
        GameObject firedKnife = GameObject.Instantiate(knifePrefab, knifeEmitter.transform.position,knifeRotation.transform.rotation);
        Rigidbody2D thrownRgbd = firedKnife.GetComponent<Rigidbody2D>();


        thrownRgbd.velocity += new Vector2(directionForce.x * throwForce, directionForce.y * throwForce);       
    }
}
