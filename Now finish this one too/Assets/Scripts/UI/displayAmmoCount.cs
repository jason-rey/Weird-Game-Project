using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class displayAmmoCount : MonoBehaviour
{
    public TextMeshProUGUI ammoCounter;
    public ammoManager ammoManager;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!ammoManager.currentlyReloading)
        {
            ammoCounter.text = ammoManager.currentKnifeAmmo.ToString();
        }
        else if (ammoManager.currentlyReloading)
        {
            ammoCounter.text = "Reloading";
        }
    }
}
