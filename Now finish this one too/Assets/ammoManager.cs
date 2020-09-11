using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammoManager : MonoBehaviour
{
    public int maxKnifeAmmo;
    public float reloadTime;

    public bool currentlyReloading;

    public int currentKnifeAmmo;
    // Start is called before the first frame update

    public IEnumerator ReloadWeapon()
    {
        currentlyReloading = true;
        yield return new WaitForSeconds(reloadTime);
        currentKnifeAmmo = maxKnifeAmmo;
        currentlyReloading = false;
    }


}
