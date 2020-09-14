using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animationController : MonoBehaviour
{
    public Animator animator;
    public playerControllerV2 playerCont;
    public SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        playerCont = GetComponent<playerControllerV2>();
        sprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCont.horizontalInput != 0)
        {
            animator.SetBool("isWalking", true);
        }

        else
        {
            animator.SetBool("isWalking", false);
        }
    }
}
