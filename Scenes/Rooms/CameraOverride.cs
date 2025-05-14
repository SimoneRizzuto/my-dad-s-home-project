using Godot;
using MyFathersHomeProject.Scripts.Camera;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scenes.Rooms;
public partial class CameraOverride : Node2D
{
    [Export] public int LimitLeft = -10000000;
    [Export] public int LimitTop = -10000000;
    [Export] public int LimitRight = 10000000;
    [Export] public int LimitBottom = 10000000;
    
    public PlayerCamera PlayerCamera => GetNodeHelper.GetPlayerCamera(GetTree());
    
    public override void _Process(double delta)
    {
        PlayerCamera.LimitLeft = LimitLeft;
        PlayerCamera.LimitTop = LimitTop;
        PlayerCamera.LimitRight = LimitRight;
        PlayerCamera.LimitBottom = LimitBottom;
    }
}