using Godot;
using System;
using MyFathersHomeProject.Scripts.Shared.Helpers;
using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Camera;
public partial class PlayerCamera : Camera2D
{
    public const int Height = 55;

    private Oliver Oliver => GetNodeHelper.GetOliver(GetTree());
    private float OliverXPosition => (float)Math.Round(Oliver.Position.X);

    private bool isMounted;
    
    public override void _Ready()
    {
        PositionSmoothingEnabled = false;
        SetPositionOnOliver();
    }
    
    public override void _Process(double delta)
    {
        if (!isMounted)
        {
            SetPositionOnOliver();
        }
        
        PositionSmoothingEnabled = true;
    }
    
    private void SetPositionOnOliver()
    {
        GlobalPosition = new(OliverXPosition, Height);
        
        PositionSmoothingSpeed = 8;
    }
    
    public void Mount(Vector2 position)
    {
        GlobalPosition = position;
        
        PositionSmoothingSpeed = 7;
        isMounted = true;
    }
    
    public void Dismount()
    {
        isMounted = false;
    }
}