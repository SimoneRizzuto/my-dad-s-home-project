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
    private bool IsJumping => IsOnFloor() && Jump;
    private AnimatedSprite2D MainSprites => GetNode<AnimatedSprite2D>("MainSprites");
    
    private Direction lastDirection = Direction.Left;
    
    public override void _PhysicsProcess(double delta)
    {
        if (!IsOnFloor())
        {
            Velocity = new Vector2(Velocity.X, Velocity.Y + Gravity * (float)delta);
        }
        else if (IsJumping)
        {
            Velocity = new Vector2(Velocity.X, JumpVelocity);
            OnJump();
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
        
        if (movementVector.X < 0) lastDirection = Direction.Left;
        else if (movementVector.X > 0) lastDirection = Direction.Left;
        
        Velocity = movementVector * (float)delta;
        
        if (movementVector.X != 0)
        {
            //MainSprites.Play($"walk left");
            
            MainSprites.Play($"idle left");
        }
        else
        {
            MainSprites.Play($"idle left");
        }
    }
    
    private void OnJump()
    {
        var lastDirectionString = Enum.GetName(lastDirection)?.ToLower();
        MainSprites.Play($"jump {lastDirectionString}");
    }
}
