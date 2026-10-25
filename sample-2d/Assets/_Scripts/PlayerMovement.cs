using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : Core
{
    public IdleState idleState;
    public RunState runState;
    public AirState airState;
    public float jumpSpeed;
    public float xInput { get; private set; }
    public float yInput { get; private set; }
    public float acceleration;
    public float maxXSpeed;
    public float friction;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        SetupInstances();
        machine.Set(idleState);
    }

    private void Update()
    {
        SelectState();
        machine.state.Do();
    }

    private void FixedUpdate()
    {
        HandleMovementX();
        ApplyFriction();
    }

    private void SelectState()
    {
        if (groundSensor.grounded)
        {
            if (xInput == 0)
            {
                machine.Set(idleState);
            }
            else if (Mathf.Abs(xInput) > 0)
            {
                machine.Set(runState);
            }
        }
        else
        {
            machine.Set(airState);
        }
    }

    public void HandleJumpInput() 
    {
        if (groundSensor.grounded)
        {
            body.linearVelocity = new Vector2(body.linearVelocityX, jumpSpeed);
        }
    }

    public void HandleMovementX()
    {
        if (Mathf.Abs(xInput) > 0)
        {
            float increment = xInput * acceleration;
      
            float newSpeed = Mathf.Clamp(body.linearVelocityX + increment, -maxXSpeed, maxXSpeed);
 
            body.linearVelocity = new Vector2(newSpeed, body.linearVelocityY);

            FaceInput();
        }
    }

    private void FaceInput()
    {
        float direction = Mathf.Sign(xInput);

        transform.localScale = new Vector3(
            Mathf.Abs(transform.localScale.x) * direction,
            transform.localScale.y,
            transform.localScale.z
        );
    }

  

    // Study later
    private void ApplyFriction()
    {
        if (Mathf.Abs(xInput) < 0.01f)
        {
            body.linearVelocityX = Mathf.MoveTowards(
                body.linearVelocityX,
                0f,
                friction * Time.fixedDeltaTime
            );
        }
    }

    public void Move(InputAction.CallbackContext context)
    {
        xInput = context.ReadValue<float>();
    }

//    private void OnDrawGizmos()
//    {
//#if UNITY_EDITOR
//        Gizmos.color = Color.red;
//        Gizmos.DrawWireSphere(groundSensor.groundCheck.position, groundSensor.groundCheckRadius);
//#endif
//    }
}
