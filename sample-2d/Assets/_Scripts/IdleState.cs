using UnityEngine;

public class IdleState : State
{
    public override void Enter()
    {
        animator.Play("Idle");
    }

    public override void Do()
    {
        if(!input.grounded || Mathf.Abs(input.xInput) !> 0)
        {
            isComplete = true;
        }
    }
    public override void Exit()
    {
    }
}
