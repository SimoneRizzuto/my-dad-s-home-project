using Godot;
using System;
using DialogueManagerRuntime;
using MyFathersHomeProject.Scripts.Dialogue;
using MyFathersHomeProject.Scripts.Shared.Constants;

[Icon("res://Assets/Textures/UI/oliver-head.png")]
public partial class Oliver : CharacterBody2D, ICharacter
{
    private int Gravity => ProjectSettings.GetSetting("physics/2d/default_gravity").ToString().ToInt();
    private int JumpVelocity => -125;
    private bool MoveLeft => Input.IsActionPressed(InputMapAction.MoveLeft);
    private bool MoveRight => Input.IsActionPressed(InputMapAction.MoveRight);
    private bool Jump => Input.IsActionPressed(InputMapAction.Jump);
    private bool IsJumping => IsOnFloor() && Jump;
    private AnimatedSprite2D MainSprite => GetNode<AnimatedSprite2D>($"{nameof(MainSprite)}");
    private string LastDirectionString => Enum.GetName(LastDirection)?.ToLower();
    
    private bool IsInteracting = false;
    
    public Direction LastDirection { get; set; } = Direction.Left;
    
    public override void _Ready()
    {
        MainSprite.AnimationFinished += OnAnimationFinished;
    }
    
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed(InputMapAction.Debug1))
        {
            DialogueManager.ShowDialogueBalloon(GD.Load($"res://Assets/Dialogue/test-dialogue.dialogue"), "debug");
            //DialogueManager.DialogueEnded += SetupGameplayAfterDialogueEnded;
        }
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
        
        if (movementVector.X < 0) LastDirection = Direction.Left;
        else if (movementVector.X > 0) LastDirection = Direction.Right;

        Velocity = movementVector;
        
        if (movementVector.X != 0)
        {
            MainSprite.Play($"walk {LastDirectionString}");
        }
        else
        {
            MainSprite.Play($"idle {LastDirectionString}");
        }
    }
    
    private void OnJump()
    {
        MainSprite.SetFrameAndProgress(0, 0);
        MainSprite.Play($"jump {LastDirectionString}");
    }
    
    // signals
    private void OnAnimationFinished()
    {
        if (MainSprite.Animation.ToString().Contains("jump"))
        {
            MainSprite.Play($"idle {LastDirectionString}");
        }
    }
}
