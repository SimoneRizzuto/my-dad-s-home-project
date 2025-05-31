using Godot;
using System;
using MyFathersHomeProject.Scripts.Character;
using MyFathersHomeProject.Scripts.Shared.Constants;

[Icon("res://Assets/Textures/UI/oliver-head.png")]
public partial class Oliver : CharacterBody2D, ICharacter
{
    private int Gravity => ProjectSettings.GetSetting("physics/2d/default_gravity").ToString().ToInt();
    private int JumpVelocity => -125;
    private bool JumpInput => Input.IsActionPressed(InputMapAction.Jump);
    private bool TriggeredJump => IsOnFloor() && JumpInput;
    private AnimatedSprite2D MainSprite => GetNode<AnimatedSprite2D>($"{nameof(MainSprite)}");
    private string LastDirectionString => Enum.GetName(LastDirection)?.ToLower();
    
    public Direction LastDirection { get; set; } = Direction.Left;
    public CharacterState CharacterState { get; set; } = CharacterState.Gameplay;
    
    public override void _Ready()
    {
        MainSprite.AnimationFinished += OnAnimationFinished;
    }
    
    public override void _PhysicsProcess(double delta)
    {
        switch (CharacterState)
        {
            case CharacterState.Gameplay:
                ProcessGameplay(delta);
                break;
            case CharacterState.Cutscene:
            case CharacterState.Disabled:
                break;
        }
        
        MoveAndSlide();
        
        if (Velocity == Vector2.Zero)
        {
            var positionX = (float)Math.Round(Position.X, MidpointRounding.ToEven);
            var positionY = (float)Math.Round(Position.Y, MidpointRounding.ToEven);
            Position = new(positionX, positionY);
        }
    }
    
    private void ProcessGameplay(double delta)
    {
        Move();
        
        if (IsOnFloor() && TriggeredJump)
        {
            Jump();
        }
        
        if (!IsOnFloor())
        {
            Velocity = new Vector2(Velocity.X, Velocity.Y + Gravity * (float)delta);
        }
    }
    
    private void Move()
    {
        var direction = Input.GetAxis(InputMapAction.MoveLeft, InputMapAction.MoveRight);
        Move(direction);
    }
    
    #region interface implementations
    
    public void Move(float direction)
    {
        var movementVector = new Vector2(direction * Movement.MoveSpeed, Velocity.Y);
        
        if (movementVector.X < 0) SetDirection(Direction.Left);
        else if (movementVector.X > 0) SetDirection(Direction.Right);
        
        Velocity = movementVector;
        
        if (IsOnFloor())
        {
            if (movementVector.X != 0)
            {
                PlayAnimation($"walk {LastDirectionString}");
            }
            else
            {
                PlayAnimation($"idle {LastDirectionString}");
            }
        }
    }
    
    public void PlayAnimation(string animationName)
    {
        MainSprite.Play(animationName);
    }
    
    public void SetDirection(Direction direction)
    {
        LastDirection = direction;
    }
    
    public void Jump()
    {
        if (!IsOnFloor()) return;
        
        Velocity = new Vector2(Velocity.X, JumpVelocity);
        
        MainSprite.SetFrameAndProgress(0, 0);
        PlayAnimation($"jump {LastDirectionString}");
    }
    
    #endregion
    
    #region signals
    private void OnAnimationFinished()
    {
        if (CharacterState is CharacterState.Cutscene) return;
        
        if (MainSprite.Animation.ToString().Contains("jump"))
        {
            PlayAnimation($"idle {LastDirectionString}");
        }
    }
    #endregion
}
