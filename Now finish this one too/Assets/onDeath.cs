using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onDeath : MonoBehaviour
{
    private objectHealth objectHealth;
    // Start is called before the first frame update
    void Start()
    {
        objectHealth = GetComponent<objectHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (objectHealth.currentHealth <= 0)
        {
            OnDeath();
        }
    }

    private void OnDeath()
    {
        Destroy(this.gameObject);
    }
}
