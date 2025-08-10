using System;
using Godot;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Player;
using MyFathersHomeProject.Scripts.Shared.Modules;

namespace MyFathersHomeProject.Scripts.Misc;
public partial class FoodPlate : CharacterBody2D, IAction
{
    // variables
    private bool _sheIsFlying;
    private bool _comingBackDown;
    
    public override void _PhysicsProcess(double delta)
    {
        if (!_sheIsFlying) return;
        
        if (!_comingBackDown)
        {
            Velocity = new Vector2(Velocity.X, (Velocity.Y - 6750) * (float)delta);
            
            if (Position.Y < -80)
            {
                _comingBackDown = true;
            }
        }
        else
        {
            Velocity = new Vector2(Velocity.X, (Velocity.Y + 6750) * (float)delta);
        }
        
        MoveAndSlide();
    }
    
    public void Action()
    {
        _sheIsFlying = true;
    }
    
    private void OnBodyShapeEntered(Rid bodyrid, Node2D body, long bodyshapeindex, long localshapeindex)
    {
        if (body is Oliver && _comingBackDown)
        {
            // play eat sound effect
            QueueFree();
        }
    }
}