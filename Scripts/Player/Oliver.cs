using Godot;
using System;
using DialogueManagerRuntime;
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
    
    public override void _Process(double delta)
    {
        if (Input.IsActionJustPressed(InputMapAction.Debug1))
        {
            DialogueManager.ShowDialogueBalloon(GD.Load($"res://Assets/Dialogue/test-dialogue.dialogue"), "debug");
            CharacterState = CharacterState.Cutscene;
            //DialogueManager.DialogueEnded += SetupGameplayAfterDialogueEnded;
        }
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
        
        if (movementVector.X < 0) LastDirection = Direction.Left;
        else if (movementVector.X > 0) LastDirection = Direction.Right;
        
        Velocity = movementVector;
        
        if (IsOnFloor())
        {
            if (movementVector.X != 0)
            {
                MainSprite.Play($"walk {LastDirectionString}");
            }
            else
            {
                MainSprite.Play($"idle {LastDirectionString}");
            }
        }
    }
    
    public void Jump()
    {
        if (!IsOnFloor()) return;
        
        Velocity = new Vector2(Velocity.X, JumpVelocity);
        
        MainSprite.SetFrameAndProgress(0, 0);
        MainSprite.Play($"jump {LastDirectionString}");
    }
    
    #endregion
    
    #region signals
    private void OnAnimationFinished()
    {
        if (MainSprite.Animation.ToString().Contains("jump"))
        {
            MainSprite.Play($"idle {LastDirectionString}");
        }
    }
    #endregion
}
