using Godot;
using System;
using MyFathersHomeProject.Scripts.Shared.Constants;

public partial class Oliver : CharacterBody2D
{
    private int Gravity => ProjectSettings.GetSetting("physics/2d/default_gravity").ToString().ToInt();
    private int JumpVelocity => -200;
    private bool MoveLeft => Input.IsActionPressed(InputMapAction.MoveLeft);
    private bool MoveRight => Input.IsActionPressed(InputMapAction.MoveRight);
    private bool Jump => Input.IsActionPressed(InputMapAction.Jump);
    
    public override void _PhysicsProcess(double delta)
    {
        if (!IsOnFloor())
        {
            Velocity = new Vector2(Velocity.X, Velocity.Y + Gravity * (float)delta);
        }
        else if (IsOnFloor() && Jump)
        {
            Velocity = new Vector2(Velocity.X, JumpVelocity);
        }
        else
        {
            OnMove(delta);
        }
        
        MoveAndSlide();
    }

    private void OnMove(double delta)
    {
        var direction = Input.GetAxis(InputMapAction.MoveLeft, InputMapAction.MoveRight);
        var movementVector = new Vector2(direction * Movement.MoveSpeed, Velocity.Y);
        Velocity = movementVector * (float)delta;
    }
}
