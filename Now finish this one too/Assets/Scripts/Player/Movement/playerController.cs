using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float runSpeed;
    public float jumpForce;
    public float dashSpeed;
    public float dashDistance; // length of dash is dashLength / 100
    public float cooldownLength = 0.5f;
    public DashState dashState;
    public float onGroundCooldown;
    public float playerDirection;
    public stopTimeController timeController;
    public float horizontalInput;

    private float currentSpeed = 0;
    private float accelerationSpeed = 0;
    private bool runStartup = false;
    private bool canDash;
    private Vector3 targetPosition;    
    private float cooldownTime;
    private float originalSpeed;
    private bool isDashing;
    private bool doubleJump = false;
    private bool jumpApex;
    private bool jumpInput;
    private bool isJumping;
    private Rigidbody2D rgbd;
    private CapsuleCollider2D playerCollider;
    //private SpriteRenderer playerSprite;
    // Start is called before the first frame update
    void Start()
    {
        playerCollider = this.GetComponent<CapsuleCollider2D>();
        rgbd = this.GetComponent<Rigidbody2D>();
        //SpriteRenderer = this.GetComponent<SpriteRenderer2D>():
        timeController = GetComponent<stopTimeController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!timeController.stoppingTime)
        {
            horizontalInput = Input.GetAxisRaw("Horizontal");

            if (dashState != DashState.Dashing)
            {
                transform.Translate((Vector2.right * horizontalInput * runSpeed) * Time.deltaTime);
            }


            if (IsGrounded())
            {
                doubleJump = true;
            }

            if (Input.GetKeyDown(KeyCode.Space) && !IsGrounded() && doubleJump)
            {
                jumpInput = true;
                doubleJump = false;
            }

            if (Input.GetKeyDown(KeyCode.Space) && IsGrounded())
            {
                jumpInput = true;
                isJumping = true;
            }

            switch (horizontalInput) // Determines which way the player is facing
            {
                case 1:
                    playerDirection = 1;
                    break;
                case -1:
                    playerDirection = -1;
                    break;
            }

            Dash();
        }

    }

    private void FixedUpdate()
    {
        Jump(jumpInput);
    }

    void Jump(bool input)
    {
        if (input)
        {
            rgbd.velocity = Vector2.up * jumpForce;
            jumpInput = false;
        }
    }

    bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(this.transform.position, Vector3.down, (playerCollider.bounds.size.y / 2) + 0.1F);
        Debug.DrawRay(this.transform.position, Vector3.down * ((playerCollider.bounds.size.y / 2) + 0.5F));
        return hit;
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

    private void Dash()
    {

        switch (dashState)
        {
            case DashState.Ready:
                bool dashInput = Input.GetKeyDown(KeyCode.LeftShift);
                RaycastHit2D hit = Physics2D.Raycast(this.transform.position, new Vector3(dashDistance * playerDirection, 0),dashDistance);
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
                if (Vector2.Distance(transform.position, targetPosition) <= 1)
                {
                    dashState = DashState.Cooldown;
                }
                break;

            case DashState.Cooldown:
                rgbd.constraints = RigidbodyConstraints2D.FreezeRotation;
                cooldownTime -= (cooldownTime / onGroundCooldown);

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
