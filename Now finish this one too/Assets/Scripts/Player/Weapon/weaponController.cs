using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponController : MonoBehaviour
{
    public fireProjectile weapon;
    public ammoManager weaponAmmo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (weaponAmmo.currentKnifeAmmo > 0 && !weaponAmmo.currentlyReloading)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                weapon.FireWeapon();
                weaponAmmo.currentKnifeAmmo -= 1;
            }
            
        }

        if (weaponAmmo.currentKnifeAmmo < weaponAmmo.maxKnifeAmmo && !weaponAmmo.currentlyReloading)
        {            
            if (Input.GetKeyDown(KeyCode.R)) StartCoroutine(weaponAmmo.ReloadWeapon());
        }

        if (weaponAmmo.currentKnifeAmmo <= 0 && !weaponAmmo.currentlyReloading) StartCoroutine(weaponAmmo.ReloadWeapon());
    }
}
