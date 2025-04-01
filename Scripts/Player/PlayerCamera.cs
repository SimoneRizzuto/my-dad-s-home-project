using Godot;
using System;
using MyFathersHomeProject.Scripts.Shared.Helpers;

public partial class PlayerCamera : Camera2D
{
    private Oliver Oliver => GetNodeHelper.GetOliver(GetTree());
    private float OliverXPosition => (float)Math.Round(Oliver.Position.X);
    
    public override void _Ready()
    {
        PositionSmoothingEnabled = false;
        SetGlobalPosition();
    }
    
    public override void _Process(double delta)
    {
        SetGlobalPosition();
        PositionSmoothingEnabled = true;
        PositionSmoothingSpeed = 8;
    }
    
    private void SetGlobalPosition()
    {
        GlobalPosition = new(OliverXPosition, GlobalPosition.Y);
    }
}
