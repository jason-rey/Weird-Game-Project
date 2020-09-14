using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fireProjectile : MonoBehaviour , IWeaponFireable
{
    public GameObject projectilePrefab;
    public Transform projectileRotation;
    public GameObject weaponEmitter;
    public float shootForce;
    public ammoManager weaponAmmo;

    private Vector3 mousePos;

    public void FireWeapon()
    {
        Vector3 mousePosRaw = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        mousePos = Camera.main.ScreenToWorldPoint(mousePosRaw);
        mousePos.z = 0;

        Vector3 shootDirection = (mousePos - this.transform.position);
        shootDirection = shootDirection.normalized;
        //Debug.Log(shootDirection * shootForce);
        shootDirection *= shootForce;
        Debug.DrawRay(transform.position, new Vector3(shootDirection.x, shootDirection.y) * shootForce);

        Vector3 finalKnifeForce = new Vector3(shootDirection.x, shootDirection.y, shootDirection.z);

        GameObject firedKnife = GameObject.Instantiate(projectilePrefab, weaponEmitter.transform.position,projectileRotation.transform.rotation);
        Rigidbody2D thrownRgbd = firedKnife.GetComponent<Rigidbody2D>();


        thrownRgbd.velocity += new Vector2(finalKnifeForce.x * shootForce, finalKnifeForce.y * shootForce);

        firedKnife.GetComponent<knifeCollisions>().storedVelocity = new Vector2(finalKnifeForce.x * shootForce, finalKnifeForce.y * shootForce);
    }

}
