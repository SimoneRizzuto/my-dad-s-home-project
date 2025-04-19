using Godot;
using System;
using GodotPlugins.Game;
using MyFathersHomeProject.Scripts.Shared.Constants;

public partial class Oliver : CharacterBody2D
{
    private int Gravity => ProjectSettings.GetSetting("physics/2d/default_gravity").ToString().ToInt();
    private int JumpVelocity => -125;
    private bool MoveLeft => Input.IsActionPressed(InputMapAction.MoveLeft);
    private bool MoveRight => Input.IsActionPressed(InputMapAction.MoveRight);
    private bool Jump => Input.IsActionPressed(InputMapAction.Jump);
    private bool IsJumping => IsOnFloor() && Jump;
    private AnimatedSprite2D MainSprites => GetNode<AnimatedSprite2D>("MainSprites");
    private string LastDirectionString => Enum.GetName(lastDirection)?.ToLower();
    
    private bool IsInteracting = false;
    
    private Direction lastDirection = Direction.Left;

    public override void _Ready()
    {
        MainSprites.AnimationFinished += OnAnimationFinished;
    }
    
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
            OnMove();
        }
        
        MoveAndSlide();
        
        if (Velocity == Vector2.Zero)
        {
            var positionX = (float)Math.Round(Position.X, MidpointRounding.ToEven);
            var positionY = (float)Math.Round(Position.Y, MidpointRounding.ToEven);
            Position = new(positionX, positionY);
        }
    }
    
    private void OnMove()
    {
        var direction = Input.GetAxis(InputMapAction.MoveLeft, InputMapAction.MoveRight);
        
        var movementVector = new Vector2(direction * Movement.MoveSpeed, Velocity.Y);
        
        if (movementVector.X < 0) lastDirection = Direction.Left;
        else if (movementVector.X > 0) lastDirection = Direction.Right;

        Velocity = movementVector;
        
        if (movementVector.X != 0)
        {
            MainSprites.Play($"walk {LastDirectionString}");
        }
        else
        {
            MainSprites.Play($"idle {LastDirectionString}");
        }
    }
    
    private void OnJump()
    {
        MainSprites.SetFrameAndProgress(0, 0);
        MainSprites.Play($"jump {LastDirectionString}");
    }
    
    // signals
    private void OnAnimationFinished()
    {
        if (MainSprites.Animation.ToString().Contains("jump"))
        {
            MainSprites.Play($"idle {LastDirectionString}");
        }
    }
}
