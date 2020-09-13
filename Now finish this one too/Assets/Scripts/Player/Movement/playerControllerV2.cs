using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControllerV2 : MonoBehaviour
{
    public float horizontalInput;
    public stopTimeController timeController;

    private CapsuleCollider2D playerCollider;
    private Rigidbody2D playerBody;

   
    [SerializeField] private float runSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float cooldownLength = 0.5f;

    private Vector3 targetPosition;
    private bool isDashing;
    private bool canDash;


    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<CapsuleCollider2D>();
        playerBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!timeController.stoppingTime)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");
            transform.Translate((Vector2.right * horizontalInput * runSpeed) * Time.deltaTime);
            Jump(IsGrounded(), playerBody);
        }
      
        if (timeController.stoppingTime)
        {
            playerBody.constraints = playerBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        playerBody.constraints = playerBody.constraints = RigidbodyConstraints2D.FreezeRotation;

    }

   public bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector3.down, (playerCollider.bounds.size.y / 2) + 0.1F);
        Debug.DrawRay(this.transform.position, Vector3.down * ((playerCollider.bounds.size.y / 2) + 0.5F));
        return hit;
    } 

    void Jump(bool isGrounded,Rigidbody2D rgbd)
    {             
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rgbd.velocity = new Vector2(rgbd.velocity.x,jumpForce);
        }

        if (Input.GetButtonUp("Jump") && rgbd.velocity.y > 0)
        {
            rgbd.velocity = new Vector2(rgbd.velocity.x, rgbd.velocity.y * 0.5f);
        }


    }

    //IEnumerator cooldownTimer()
    //{
    //    float cooldown = 0;

    //    if (IsGrounded())
    //    {
    //        Debug.Log("delaying");
    //        cooldown = 0.5f;
    //        yield return new WaitForSeconds(cooldown);
    //        canDash = true;
    //    }

    //    if (!IsGrounded())
    //    {
    //        if (isDashing)
    //        {
    //            Debug.Log("dash used");
    //            canDash = false;
    //        }
    //    }

    //}

}
