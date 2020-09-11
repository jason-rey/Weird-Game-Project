using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControllerV2 : MonoBehaviour
{
    public float horizontalInput;
    public stopTimeController timeController;

    private CapsuleCollider2D playerCollider;
    private Rigidbody2D playerBody;

    [SerializeField] private DashState dashState;
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
            Dash(playerBody, cooldownLength, cooldownLength);
            transform.Translate((Vector2.right * horizontalInput * runSpeed) * Time.deltaTime);
            Jump(IsGrounded(), Input.GetButtonDown("Jump"), playerBody);
        }
      
        if (timeController.stoppingTime)
        {
            playerBody.constraints = playerBody.constraints = RigidbodyConstraints2D.FreezeAll;
        }

        playerBody.constraints = playerBody.constraints = RigidbodyConstraints2D.FreezeRotation;

    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector3.down, (playerCollider.bounds.size.y / 2) + 0.1F,9);
        Debug.DrawRay(this.transform.position, Vector3.down * ((playerCollider.bounds.size.y / 2) + 0.5F));
        return hit;
    } 

    void Jump(bool isGrounded,bool input,Rigidbody2D rgbd)
    {     
        
        if (input && isGrounded)
        {
            rgbd.velocity = Vector2.up * jumpForce;
        }
    }

    IEnumerator cooldownTimer()
    {
        float cooldown = 0;

        if (IsGrounded())
        {
            Debug.Log("delaying");
            cooldown = 0.5f;
            yield return new WaitForSeconds(cooldown);
            canDash = true;
        }

        if (!IsGrounded())
        {
            if (isDashing)
            {
                Debug.Log("dash used");
                canDash = false;
            }
        }

    }

    void Dash(Rigidbody2D rgbd, float cooldownLength, float cooldownTime)
    {
        float playerDirection = Input.GetAxisRaw("Horizontal");


        switch (dashState)
        {
            case DashState.Ready:
                bool dashInput = Input.GetKeyDown(KeyCode.LeftShift);
                RaycastHit2D hit = Physics2D.Raycast(this.transform.position, new Vector3(dashDistance * playerDirection, 0), dashDistance,9);
                if (dashInput)
                {
                    if (!IsGrounded())
                    {
                        canDash = false;
                    }

                    else
                    {
                        canDash = true;
                    }

                    if (!hit)
                    {
                        targetPosition = transform.position + new Vector3(dashDistance * playerDirection, 0);
                        rgbd.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                        dashState = DashState.Dashing;
                        cooldownTime = cooldownLength;
                    }

                    if (hit)
                    {
                        targetPosition = hit.point;
                        rgbd.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
                        dashState = DashState.Dashing;
                        cooldownTime = cooldownLength;
                    }
                }
                break;

            case DashState.Dashing:
                transform.position = Vector3.Lerp(transform.position, targetPosition, dashSpeed * Time.deltaTime);
                if (Vector2.Distance(transform.position, targetPosition) <= 2)
                {
                    dashState = DashState.Cooldown;
                }
                break;

            case DashState.Cooldown:
                rgbd.constraints = RigidbodyConstraints2D.FreezeRotation;
                cooldownTime -= (cooldownTime / 1);

                if (IsGrounded())
                {
                    if (cooldownTime <= 0)
                    {
                        dashState = DashState.Ready;
                    }
                }

                if (!IsGrounded())
                {
                    if (!canDash && IsGrounded())
                    {
                        dashState = DashState.Ready;
                    }

                    if (canDash)
                    {
                        dashState = DashState.Ready;
                    }
                }
                break;
        }

    }

    public enum DashState
    {
        Ready,
        Dashing,
        Cooldown
    }
}
