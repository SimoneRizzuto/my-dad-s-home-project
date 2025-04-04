using Godot;
using System;
using MyFathersHomeProject.Scripts.Shared.Helpers;
using MyFathersHomeProject.Scripts.Shared.Constants;

namespace MyFathersHomeProject.Scripts.Player;
public partial class PlayerCamera : Camera2D
{
    private const int Height = 55;

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
        if (Input.IsActionJustPressed(InputMapAction.Debug1))
        {
            switch (isMounted)
            {
                case false: Mount(new(200, GlobalPosition.Y));
                    break;
                case true: Unmount();
                    break;
            }
        }
        
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
    
    private void Mount(Vector2 position)
    {
        GlobalPosition = position;
        
        PositionSmoothingSpeed = 7;
        isMounted = true;
    }
    
    private void Unmount()
    {
        isMounted = false;
    }
}