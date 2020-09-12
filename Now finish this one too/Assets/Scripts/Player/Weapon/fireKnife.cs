using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireKnife : MonoBehaviour , IWeaponFireable
{
    public GameObject knifePrefab;
    public Transform knifeRotation;
    public GameObject knifeEmitter;
    public float throwForce;
    public GameObject playerObj;
    public ammoManager weaponAmmo;

    private Vector3 mousePos;

    public void FireWeapon()
    {
        Vector3 mousePosRaw = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        mousePos = Camera.main.ScreenToWorldPoint(mousePosRaw);
        mousePos.z = 0;

        Vector3 shootDirection = (mousePos - this.transform.position);
        shootDirection = shootDirection.normalized;
        //Debug.Log(shootDirection * throwForce);
        shootDirection *= throwForce;
        Debug.DrawRay(transform.position, new Vector3(shootDirection.x, shootDirection.y) * throwForce);

        Vector3 finalKnifeForce = new Vector3(shootDirection.x, shootDirection.y, shootDirection.z);

        GameObject firedKnife = GameObject.Instantiate(knifePrefab, knifeEmitter.transform.position,knifeRotation.transform.rotation);
        Rigidbody2D thrownRgbd = firedKnife.GetComponent<Rigidbody2D>();


        thrownRgbd.velocity += new Vector2(finalKnifeForce.x * throwForce, finalKnifeForce.y * throwForce) ;       
    }

}
