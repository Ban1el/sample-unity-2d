using System;
using UnityEngine;
using UnityEngine.Windows;

public class RunState : State
{
    public float maxXSpeed;
    public override void Enter()
    {
        animator.Play("Run");
    }

    public override void Do()
    {
        float velX = body.linearVelocityX;

        float speed = Mathf.Abs(body.linearVelocityX);

        animator.speed = Helpers.Map(
              speed,          // Current horizontal velocity
              0f, maxXSpeed,  // Input range
              0f, 1f,       // Animation speed range
              true            // Clamp the result
        );

        if (!input.grounded || Mathf.Abs(velX) < 0.1f)
        {
            isComplete = true;
        }
    }

    public override void Exit()
    {
    }
}
