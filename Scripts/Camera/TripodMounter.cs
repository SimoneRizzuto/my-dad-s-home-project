using Godot;
using MyFathersHomeProject.Scripts.Shared.Helpers;

namespace MyFathersHomeProject.Scripts.Camera;
public partial class TripodMounter : Area2D
{
    [Export] public Marker2D MountPosition;
    private PlayerCamera PlayerCamera => GetNodeHelper.GetPlayerCamera(GetTree());
    
    public override void _Ready()
    {
        BodyShapeEntered += OnBodyShapeEntered;
    }
    
    private void OnBodyShapeEntered(Rid bodyrid, Node2D body, long bodyshapeindex, long localshapeindex)
    {
        if (body is Oliver)
        {
            PlayerCamera.Mount(new(MountPosition.GlobalPosition.X, PlayerCamera.Height));
        }
    }
}