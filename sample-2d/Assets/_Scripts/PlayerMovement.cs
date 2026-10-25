using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public IdleState idleState;
    public RunState runState;
    public AirState airState;
    State state;
    protected Animator animator;
    protected Rigidbody2D body;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private float groundCheckRadius;
    [SerializeField]
    private LayerMask groundLayer;
    public float jumpSpeed;

    public bool grounded { get; private set; }
    public float xInput { get; private set; }
    public float yInput { get; private set; }
    public float acceleration;
    public float maxXSpeed;
    public float friction;
    public TextMeshProUGUI currentStateText;

    private void Awake()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        idleState.Setup(body, animator, this);
        runState.Setup(body, animator, this);
        airState.Setup(body, animator, this);
        state = idleState;
    }

    private void Update()
    {
        if (state.isComplete)
        {
            SelectState();
        }

        currentStateText.text = $"State: {state.GetType().Name}";

        state.Do();
    }

    private void FixedUpdate()
    {
        CheckGround();
        HandleMovementX();
        ApplyFriction();
    }

    private void SelectState()
    {
        if (grounded)
        {
            if (xInput == 0)
            {
                state = idleState;
            }
            else if (Mathf.Abs(xInput) > 0)
            {
                state = runState;
            }
        }
        else
        {
            state = airState;
        }

        state.Enter();
    }

    public void HandleJumpInput() 
    {
        if (grounded)
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

    private void CheckGround()
    {
        Collider2D hit = Physics2D.OverlapCircle(
            groundCheck.position,
            groundCheckRadius,
            groundLayer
        );

        grounded = hit != null ? true : false;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
}
