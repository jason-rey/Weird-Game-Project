using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public playerControllerV2 playerController;

    [SerializeField] private DashState dashState;
    [SerializeField] private float dashDistance;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float cooldownLength = 0.5f;

    private Vector3 targetPosition;
    private bool isDashing;
    private bool canDash;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playerDash(Rigidbody2D rgbd, float cooldownLength, float cooldownTime)
    {
        float playerDirection = Input.GetAxisRaw("Horizontal");
        Debug.DrawRay(transform.position, new Vector3(dashDistance * playerDirection, 0) * dashDistance);

        switch (dashState)
        {
            case DashState.Ready:
                bool dashInput = Input.GetKeyDown(KeyCode.LeftShift);
                RaycastHit2D hit = Physics2D.Raycast(this.transform.position, new Vector3(dashDistance * playerDirection, 0), dashDistance, 9);
                if (dashInput)
                {
                    if (!playerController.IsGrounded())
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

                if (playerController.IsGrounded())
                {
                    if (cooldownTime <= 0)
                    {
                        dashState = DashState.Ready;
                    }
                }

                if (!playerController.IsGrounded())
                {
                    if (!canDash && playerController.IsGrounded())
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
