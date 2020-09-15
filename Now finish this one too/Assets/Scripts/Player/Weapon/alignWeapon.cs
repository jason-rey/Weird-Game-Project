using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class alignWeapon : MonoBehaviour
{
    public playerControllerV2 playerCont;
    public float speed = 1;
    public Transform playerTransf;
    public Transform emitterTransf;
    public SpriteRenderer playerSprite;
    public float emitterOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update() 
    {
//        emitterTransf.position = this.transform.position;
        
        
        if (playerSprite.flipX == true)
        {
            transform.localPosition = new Vector3(-0.25f, -0.05f, playerTransf.position.z);
            emitterTransf.localPosition = transform.localPosition;
        }

        if (playerSprite.flipX == false)
        {
            transform.localPosition = new Vector3(0.25f, -0.05f, playerTransf.position.z);
            emitterTransf.localPosition = transform.localPosition;
        }

        Vector3 mousePosRaw = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(mousePosRaw);

        Vector3 vectorToTarget = mousePos - transform.position;
        vectorToTarget = vectorToTarget.normalized;
        vectorToTarget.z = 0;
        //vectorToTarget = new Vector3(vectorToTarget.x, vectorToTarget.y, 0);




        float angle = ((Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg)-90);
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

        //transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
        transform.rotation = q;
    }
}
