using Godot;
using System;
using MyFathersHomeProject.Scripts.Shared.Helpers;
using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Player;
public partial class PlayerCamera : Camera2D
{
    private Oliver Oliver => GetNodeHelper.GetOliver(GetTree());
    private float OliverXPosition => (float)Math.Round(Oliver.Position.X);

    private bool _isMounted;
    
    public override void _Ready()
    {
        PositionSmoothingEnabled = false;
        SetPositionOnOliver();
    }
    
    public override void _Process(double delta)
    {
        if (Input.IsActionPressed(InputMapAction.Debug1))
        {
            Mount(new(1, 1));
        }
        
        if (_isMounted)
        {
            SetPositionOnOliver();
        }
        
        PositionSmoothingEnabled = true;
        PositionSmoothingSpeed = 8;
    }
    
    private void SetPositionOnOliver()
    {
        GlobalPosition = new(OliverXPosition, GlobalPosition.Y);
    }
    
    private void Mount(Vector2 position)
    {
        GlobalPosition = position;
        _isMounted = true;
    }
    
    private void Unmount()
    {
        _isMounted = false;
    }
}