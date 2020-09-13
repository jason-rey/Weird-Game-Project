using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objectHealth : MonoBehaviour
{
    public float maxHealth;

    public float currentHealth;

    private Collider2D objectCollider;
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        objectCollider = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        
    }
}
