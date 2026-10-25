using UnityEngine;

public class AirState : State
{
    public AnimationClip anim;
    public PlayerMovement input;
    public override void Enter() 
    {
        animator.Play(anim.name);
    }
    public override void Do() 
    {
        float time = Helpers.Map(body.linearVelocityY, input.jumpSpeed, -input.jumpSpeed, 0, 1, true);
        animator.Play("Jump", 0, time);
        animator.speed = 0;

        if (core.groundSensor.grounded)
        {
            isComplete = true;
        }
    }
    public override void Exit() { 
    
    }
}
