using UnityEngine;

public class GroundSensor : MonoBehaviour
{
    public bool grounded { get; private set; }
    public Transform groundCheck;
    public float groundCheckRadius;
    [SerializeField]
    private LayerMask groundLayer;
    private void FixedUpdate()
    {
        CheckGround();
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
}
