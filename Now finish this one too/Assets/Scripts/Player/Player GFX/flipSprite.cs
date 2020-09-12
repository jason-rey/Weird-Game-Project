using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class flipSprite : MonoBehaviour
{
    public Transform knifeTransf;
    public SpriteRenderer playerSprite;

    private Vector3 mousePos;

    // Start is called before the first frame update
    void Start()
    {
        playerSprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane);


        if (mousePos.x > 950)
        {
//            knifeTransf.rotation = Quaternion.Euler(new Vector3(knifeTransf.rotation.x, knifeTransf.rotation.y, -knifeTransf.rotation.z));
            playerSprite.flipX = false;
        }

        if (mousePos.x < 950)
        {
            playerSprite.flipX = true;
        }
    }
}
