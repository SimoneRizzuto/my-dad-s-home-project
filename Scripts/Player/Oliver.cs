using System;
using Godot;
using MyFathersHomeProject.Scripts.Character;
using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Player;

[Icon("res://Assets/Textures/UI/oliver-head.png")]
public partial class Oliver : CharacterBody2D, ICharacter
{
    [Export] public Direction LastDirection { get; set; } = Direction.Left;
    [Export] public bool IsHoldingBag { get; set; }

    public static Oliver? Instance { get; private set; }

    private int Gravity => ProjectSettings.GetSetting("physics/2d/default_gravity").ToString().ToInt();
    private int JumpVelocity => -125;
    private bool JumpInputted => Input.IsActionJustPressed(InputMapAction.Jump);
    public bool IsJumping => !IsOnFloor();
    
    // private getters
    private AnimatedSprite2D MainSprite => GetNode<AnimatedSprite2D>($"{nameof(MainSprite)}");
    private string LastDirectionString => Enum.GetName(LastDirection)!.ToLower();

    private bool resolveCutsceneOnLanding;

    public CharacterState CharacterState
    {
        get => characterState;
        set
        {
            characterState = value;
            if (value == CharacterState.Cutscene)
            {
                Move(0);

                if (!IsJumping)
                {
                    PlayAnimation($"idle {LastDirectionString}");
                }
                else
                {
                    resolveCutsceneOnLanding = true;
                }
            }
        }
    }
    private CharacterState characterState = CharacterState.Gameplay;

    public override void _Ready()
    {
        MainSprite.AnimationFinished += OnAnimationFinished;
        Instance = this;
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

        ProcessGravity(delta);
        MoveAndSlide();
        
        if (resolveCutsceneOnLanding && IsOnFloor())
        {
            resolveCutsceneOnLanding = false;
            var bagSuffix = IsHoldingBag ? " bag" : "";
            PlayAnimation($"idle {LastDirectionString}{bagSuffix}");
        }


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

        if (JumpInputted)
        {
            Jump();
        }
    }

    private void ProcessGravity(double delta)
    {
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

    private void ToggleHoldingBag()
    {
        IsHoldingBag = !IsHoldingBag;
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
            string animationToPlay;
            
            if (movementVector.X != 0)
            {
                animationToPlay = $"walk {LastDirectionString}";
            }
            else
            {
                animationToPlay = $"idle {LastDirectionString}";
            }

            if (IsHoldingBag)
            {
                animationToPlay += " bag";
            }
            
            PlayAnimation(animationToPlay);
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

        var animationToPlay = $"jump {LastDirectionString}";
        if (IsHoldingBag)
        {
            animationToPlay += " bag";
        }
        PlayAnimation(animationToPlay);
    }

    public void SetVisibility(bool visible)
    {
        Visible = visible;
    }

    public void SetHoldingPlate(bool isHolding)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region signals

    private void OnAnimationFinished()
    {
        if (characterState == CharacterState.Gameplay && 
            MainSprite.Animation.ToString().Contains("jump"))
        {
            PlayAnimation($"idle {LastDirectionString}");
        }
    }

    #endregion
}