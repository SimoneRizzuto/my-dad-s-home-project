using Godot;
using MyFathersHomeProject.Scripts.Player;

namespace MyFathersHomeProject.Scripts.Camera;
public partial class PlayerCamera : Camera2D
{
    public static PlayerCamera Instance { get; private set; }
    
    public const int Height = 45;

    private Oliver Oliver => Oliver.Instance;

    private bool isMounted;
    
    public override void _Ready()
    {
        Instance = this;
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
    
    public void SetPositionOnOliver()
    {
        if (!IsInstanceValid(Oliver)) return;
        
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