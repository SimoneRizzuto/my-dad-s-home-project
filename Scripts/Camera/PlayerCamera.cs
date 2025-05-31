using Godot;
using MyFathersHomeProject.Scripts.Player;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scripts.Camera;
public partial class PlayerCamera : Camera2D
{
    public const int Height = 45;

    private Oliver Oliver => GetNodeHelper.GetOliver(GetTree());

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
        GlobalPosition = new(Oliver.Position.X, Height);
        
        PositionSmoothingSpeed = 8;
        
        if (Oliver.Velocity == Vector2.Zero)
        {
            PositionSmoothingSpeed = 10;
        }
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