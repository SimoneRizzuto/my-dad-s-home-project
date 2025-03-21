using Godot;
using System;

public partial class Oliver : CharacterBody2D
{
    public override void _PhysicsProcess(double delta)
    {
        var moveLeft = Input.IsActionPressed(InputMapAction.MoveLeft);
        var moveRight = Input.IsActionPressed(InputMapAction.MoveRight);
        
        Console.WriteLine($"Move Left: {moveLeft}");
        Console.WriteLine($"Move Right: {moveRight}");


        /*var movementVector = Input.GetVector(InputMapAction.MoveLeft, InputMapAction.Right, InputMapAction.Up, InputMapAction.Down);
        State.Player.CalculatedVelocity = MovementVectorIsAboveThreshold(movementVector) ? movementVector * CurrentMoveSpeed : Vector2.Zero;

        SetMovementAnimation(movementVector);*/
    }
}
