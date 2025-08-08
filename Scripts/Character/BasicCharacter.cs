using Godot;
using System;
using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Character;
public partial class BasicCharacter : CharacterBody2D, ICharacter
{
    [Export] public string InitialAnimation = "";
    
    [Export] public bool IsHoldingPlate;
    
    // getters
    private int Gravity => ProjectSettings.GetSetting("physics/2d/default_gravity").ToString().ToInt();
    private AnimatedSprite2D MainSprite => GetNode<AnimatedSprite2D>($"{nameof(MainSprite)}");
    private string LastDirectionString => Enum.GetName(LastDirection)?.ToLower();
    
    public override void _Ready()
    {
        if (!string.IsNullOrEmpty(InitialAnimation))
        {
            MainSprite?.Play(InitialAnimation);
        }
        else
        {
            MainSprite.Play();
        }
    }
    
    public override void _PhysicsProcess(double delta)
    {
        Velocity = new Vector2(Velocity.X, Velocity.Y + Gravity * (float)delta);
        
        MoveAndSlide();
    }
    
    #region interface implementations
    public CharacterState CharacterState { get; set; }
    public Direction LastDirection { get; set; }
    public bool IsJumping => !IsOnFloor();

    public void Move(float direction)
    {
        var movementVector = new Vector2(direction * Movement.MoveSpeed, Velocity.Y);
        
        if (movementVector.X < 0) SetDirection(Direction.Left);
        else if (movementVector.X > 0) SetDirection(Direction.Right);
        
        Velocity = movementVector;
        
        if (IsOnFloor())
        {
            var animationToPlay = "";
            
            if (movementVector.X != 0)
            {
                animationToPlay = $"walk {LastDirectionString}";
            }
            else
            {
                animationToPlay = $"idle {LastDirectionString}";
            }
            
            if (IsHoldingPlate)
            {
                animationToPlay += " plate";
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
        throw new NotImplementedException();
    }
    
    public void SetVisibility(bool visible)
    {
        Visible = visible;
    }
    
    #endregion
}