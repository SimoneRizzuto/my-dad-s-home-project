using Godot;
using System;
using MyFathersHomeProject.Scripts.Shared.Constants;

public partial class Oliver : CharacterBody2D
{
    private int Gravity => ProjectSettings.GetSetting("physics/2d/default_gravity").ToString().ToInt();
    private bool MoveLeft => Input.IsActionPressed(InputMapAction.MoveLeft);
    private bool MoveRight => Input.IsActionPressed(InputMapAction.MoveRight);
    
    public override void _PhysicsProcess(double delta)
    {
        if (!IsOnFloor())
        {
            Velocity += new Vector2(Velocity.X, Gravity * (float)delta);
            Console.WriteLine(Velocity);
        }
        else
        {
            OnMove(delta);
        }
        
        
        
        MoveAndSlide();
    }

    public void OnMove(double delta)
    {
        var movementVector = Vector2.Zero;
        
        if (MoveLeft)
        {
            movementVector = Vector2.Left;
        }
        else if (MoveRight)
        {
            movementVector = Vector2.Right;
        }

        var calculatedVelocity = movementVector * Movement.MoveSpeed;
        Velocity = calculatedVelocity * (float)delta;
        
    }
}
