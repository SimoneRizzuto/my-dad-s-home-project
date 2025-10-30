using Godot;
using MyFathersHomeProject.Scripts.Player;

namespace MyFathersHomeProject.Scripts.Camera;
public partial class PlayerCamera : Camera2D
{
    public static PlayerCamera Instance { get; private set; }
    
    public const int Height = 45;

    private Oliver Oliver => Oliver.Instance;

    public bool IsMounted { get; private set; }
    
    public override void _Ready()
    {
        Instance = this;
        PositionSmoothingEnabled = true;
        SetPositionOnOliver();
    }
    
    public override void _Process(double delta)
    {
        if (!IsMounted)
        {
            SetPositionOnOliver();
        }
    }
    
    public void SetPositionOnOliver()
    {
        if (!IsInstanceValid(Oliver)) return;
        
        GlobalPosition = new(Oliver.Position.X, Height);
        
        PositionSmoothingSpeed = 4;
        
        if (Oliver.Velocity == Vector2.Zero)
        {
            PositionSmoothingSpeed = 6;
        }
    }
    
    public void Mount(Vector2 position, int smoothingSpeed = 6)
    {
        GlobalPosition = position;
        
        PositionSmoothingSpeed = smoothingSpeed;
        IsMounted = true;
    }
    
    public void Dismount()
    {
        IsMounted = false;
    }
    
    public void ToggleSmoothing(bool? enabled = null)
    {
        switch (enabled)
        {
            case true:
            case false:
                PositionSmoothingEnabled = enabled.Value;
                break;
            case null:
                PositionSmoothingEnabled = !PositionSmoothingEnabled;
                break;
        }
    }
}